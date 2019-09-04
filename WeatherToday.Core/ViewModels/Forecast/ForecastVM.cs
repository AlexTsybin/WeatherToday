using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherToday.Core.Services.Platform;
using WeatherToday.Core.ViewModels.Base;
using WeatherToday.Core.ViewModels.Collection;

namespace WeatherToday.Core.ViewModels.Forecast
{
    public class ForecastVM : BaseCollectionVM<ForecastListItemVM>
    {
        #region Properties

        private string _cityName;
        public string CityName
        {
            get => _cityName;
            set => SetProperty(ref _cityName, value);
        }

        private string _countryName;
        public string CountryName
        {
            get => _countryName;
            set => SetProperty(ref _countryName, value);
        }

        private string _currentTemperature;
        public string CurrentTemperature
        {
            get => _currentTemperature;
            set => SetProperty(ref _currentTemperature, value);
        }

        private string _weatherDescription;
        public string WeatherDescription
        {
            get => _weatherDescription;
            set => SetProperty(ref _weatherDescription, value);
        }

        #endregion

        #region Constructor

        public ForecastVM(IMvxLogProvider logProvider, IMvxNavigationService navigationService,
            IUserInteraction userInteraction, IMvxMessenger messenger)
            : base(logProvider, navigationService, userInteraction, messenger)
        {
        }

        #endregion

        #region Protected

        protected override Task ReloadExecute()
        {
            return Task.CompletedTask;
        }

        protected override Task ItemSelectedExecute(ForecastListItemVM item)
        {
            return Task.CompletedTask;
        }

        protected override Task SetupItems()
        {
            return Task.CompletedTask;
        }

        #endregion

        #region Public

        public override Task Initialize()
        {
            return base.Initialize();
        }

        public override void Prepare()
        {
            base.Prepare();

            CityName = "Petropavlovsk-Kamchatskiy";
            CountryName = "Russia";
            CurrentTemperature = "-7";
            WeatherDescription = "Sunny";
        }

        #endregion
    }
}
