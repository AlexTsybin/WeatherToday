using MvvmCross;
using MvvmCross.Base;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using WeatherToday.Core.Messages;
using WeatherToday.Core.Models.City;
using WeatherToday.Core.Services.Platform;
using WeatherToday.Core.ViewModels.Base.Commands;
using WeatherToday.Core.ViewModels.Collection;
using WeatherToday.Core.ViewModels.EditCity;

namespace WeatherToday.Core.ViewModels.CityList
{
    public class CityListVM : BaseCollectionVM<CityListItemVM>
    {
        #region Fields

        private MvxSubscriptionToken _citiesUpdatedToken;

        #endregion

        #region Properties

        private bool _citiesChanged;
        public bool CitiesChanged
        {
            get => _citiesChanged;
            set => SetProperty(ref _citiesChanged, value);
        }

        #endregion

        #region Commands

        private IMvxAsyncCommand<CityListItemVM> _deleteCurrentItemCommand;
        public IMvxAsyncCommand<CityListItemVM> DeleteCurrentItemCommand
        {
            get => _deleteCurrentItemCommand ?? (_deleteCurrentItemCommand = new TorAsyncCommand<CityListItemVM>(DeleteCityExecute));
        }

        private IMvxAsyncCommand _addNewCityCommand;
        public IMvxAsyncCommand AddNewCityCommand
        {
            get => _addNewCityCommand ?? (_addNewCityCommand = new TorAsyncCommand(AddNewCity, null, true));
        }

        private IMvxAsyncCommand _saveChangeCommand;
        public IMvxAsyncCommand SaveChangeCommand
        {
            get => _saveChangeCommand ?? (_saveChangeCommand = new TorAsyncCommand(SaveChangeExecute));
        }

        #endregion

        #region Constructor

        public CityListVM(IMvxLogProvider logProvider, IMvxNavigationService navigationService,
            IUserInteraction userInteraction, IMvxMessenger messenger)
            : base(logProvider, navigationService, userInteraction, messenger)
        {
        }

        #endregion

        #region Private

        private async Task DeleteCityExecute(CityListItemVM item)
        {
            CityBO city = await App.Database.GetCityByNameAsync(item.CityName);

            await App.Database.DeleteCityAsync(city);

            Items.Remove(item);

            CitiesChanged = true;
        }

        private async Task AddNewCity()
        {
            await NavigationService.Navigate<EditCityVM>();
        }

        private async void CitiesUpdatedExecute(CitiesUpdatedMessage message)
        {
            if (message.Sender is EditCityVM chatItemVM)
            {
                Items.Clear();

                CitiesChanged = true;

                await SetupItems();
            }
        }

        private async Task SaveChangeExecute()
        {
            if (!CitiesChanged)
                return;

            SendUpdateMessage();

            await NavigationService.Close(this);
        }

        private void SendUpdateMessage()
        {
            Messenger.Publish(new CitiesUpdatedMessage(this));
        }

        #endregion

        #region Protected

        protected override Task ItemSelectedExecute(CityListItemVM item)
        {
            return Task.CompletedTask;
        }

        protected override Task ReloadExecute()
        {
            IsRefreshing = false;

            return Task.CompletedTask;
        }

        protected async override Task SetupItems()
        {
            List<CityBO> cityList = await App.Database.GetCitiesAsync();

            await Mvx.IoCProvider.Resolve<IMvxMainThreadAsyncDispatcher>().ExecuteOnMainThreadAsync(() =>
            {
                foreach (var city in cityList)
                {
                    Items.Add(new CityListItemVM(city.CityName, DeleteCurrentItemCommand));
                }
            });
        }

        #endregion

        #region Public

        public override async Task Initialize()
        {
            await base.Initialize();
        }

        public override void Prepare()
        {
            base.Prepare();

            Items = new ObservableCollection<CityListItemVM>();

            _citiesUpdatedToken = Messenger.Subscribe<CitiesUpdatedMessage>(CitiesUpdatedExecute);
        }

        #endregion
    }
}
