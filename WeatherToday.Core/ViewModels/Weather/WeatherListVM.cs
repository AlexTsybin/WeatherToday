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
using WeatherToday.Core.Services.Platform;
using WeatherToday.Core.ViewModels.Base.Commands;
using WeatherToday.Core.ViewModels.CityList;
using WeatherToday.Core.ViewModels.Collection;
using WeatherToday.Core.ViewModels.EditCity;

namespace WeatherToday.Core.ViewModels.Weather
{
    public class WeatherListVM : BaseCollectionVM<WeatherListItemVM>
    {
        #region Commands

        private IMvxAsyncCommand _cityListCommand;
        public IMvxAsyncCommand CityListCommand
        {
            get => _cityListCommand ?? (_cityListCommand = new TorAsyncCommand(CityListExecute, null, true));
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

        private async Task AddNewCity()
        {
            await NavigationService.Navigate<EditCityVM>();
        }

        #endregion

        #region Protected

        protected override Task ReloadExecute()
        {
            return null;
        }

        protected override Task ItemSelectedExecute(WeatherListItemVM item)
        {
            return null;
        }

        protected override async Task SetupItems()
        {
            var cityList = new List<WeatherListItemVM>();

            await Mvx.IoCProvider.Resolve<IMvxMainThreadAsyncDispatcher>().ExecuteOnMainThreadAsync(() =>
            {
                Items.Add(new WeatherListItemVM(10, "Saint Petersburg", "Sunny", DateTime.Now, DateTime.Now));
                Items.Add(new WeatherListItemVM(15, "Helsinki", "Sunny", DateTime.Now, DateTime.Now));
                Items.Add(new WeatherListItemVM(21, "Amsterdam", "Cloudy", DateTime.Now, DateTime.Now));
                Items.Add(new WeatherListItemVM(24, "Palma de Mallorca", "Sunny", DateTime.Now, DateTime.Now));
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
        }

        #endregion
    }
}
