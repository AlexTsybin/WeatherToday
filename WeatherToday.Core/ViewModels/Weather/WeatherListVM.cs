using MvvmCross;
using MvvmCross.Base;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WeatherToday.Core.Messages;
using WeatherToday.Core.Models.City;
using WeatherToday.Core.Models.Parameters;
using WeatherToday.Core.Services.Platform;
using WeatherToday.Core.ViewModels.Base.Commands;
using WeatherToday.Core.ViewModels.CityList;
using WeatherToday.Core.ViewModels.Collection;
using WeatherToday.Core.ViewModels.EditCity;
using WeatherToday.Core.ViewModels.Forecast;
using WeatherToday.Core.ViewModels.Location;

namespace WeatherToday.Core.ViewModels.Weather
{
    public class WeatherListVM : BaseCollectionVM<WeatherListItemVM>
    {
        #region Fields

        private MvxSubscriptionToken _citiesUpdatedToken;

        #endregion

        #region Commands

        private IMvxAsyncCommand _cityListCommand;
        public IMvxAsyncCommand CityListCommand
        {
            get => _cityListCommand ?? (_cityListCommand = new TorAsyncCommand(CityListExecute, null, true));
        }

        private IMvxAsyncCommand _locationCommand;
        public IMvxAsyncCommand LocationCommand
        {
            get => _locationCommand ?? (_locationCommand = new TorAsyncCommand(LocationExecute, null, true));
        }

        private IMvxAsyncCommand _addNewCityCommand;
        public IMvxAsyncCommand AddNewCityCommand
        {
            get => _addNewCityCommand ?? (_addNewCityCommand = new TorAsyncCommand(AddNewCity, null, true));
        }

        #endregion

        #region Constructor

        public WeatherListVM(IMvxLogProvider logProvider, IMvxNavigationService navigationService,
            IUserInteraction userInteraction, IMvxMessenger messenger)
            : base(logProvider, navigationService, userInteraction, messenger)
        {
        }

        #endregion

        #region Private

        private async Task CityListExecute()
        {
            await NavigationService.Navigate<CityListVM>();
        }

        private async Task LocationExecute()
        {
            await NavigationService.Navigate<LocationVM>();
        }

        private async Task AddNewCity()
        {
            await NavigationService.Navigate<EditCityVM>();
        }

        private async void CitiesUpdatedExecute(CitiesUpdatedMessage message)
        {
            Items.Clear();

            await SetupItems();
        }

        #endregion

        #region Protected

        protected override Task ReloadExecute()
        {
            IsRefreshing = false;

            return Task.CompletedTask;
        }

        protected async override Task ItemSelectedExecute(WeatherListItemVM item)
        {
            await NavigationService.Navigate<ForecastVM, ForecastParameter>(new ForecastParameter
            {
                CityName = item.CityName
            });
        }

        protected override async Task SetupItems()
        {
            //var cityList = new List<WeatherListItemVM>();

            List<CityBO> cityList = await App.Database.GetCitiesAsync();

            await Mvx.IoCProvider.Resolve<IMvxMainThreadAsyncDispatcher>().ExecuteOnMainThreadAsync(() =>
            {
                foreach (CityBO city in cityList)
                {
                    Items.Add(new WeatherListItemVM(10, city.CityName, "Sunny", DateTime.Now, DateTime.Now));
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

            PageTitle = "WeatherToday";
            Items = new ObservableCollection<WeatherListItemVM>();

            _citiesUpdatedToken = Messenger.Subscribe<CitiesUpdatedMessage>(CitiesUpdatedExecute);
        }

        public override void Unbind()
        {
            base.Unbind();

            DisposeMvxMessageToken<CitiesUpdatedMessage>(_citiesUpdatedToken);
        }

        #endregion
    }
}
