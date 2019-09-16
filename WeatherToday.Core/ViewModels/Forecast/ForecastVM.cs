using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WeatherToday.Core.Models.Parameters;
using WeatherToday.Core.Services.Platform;
using WeatherToday.Core.ViewModels.Base;
using WeatherToday.Core.ViewModels.Collection;

namespace WeatherToday.Core.ViewModels.Forecast
{
    public class ForecastVM : BaseCollectionVM<ForecastListItemVM, ForecastParameter>
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

        #region Public

        public override void Prepare(ForecastParameter parameter)
        {
            CityName = parameter.CityName;
            CountryName = "Russia";
            CurrentTemperature = "-7";
            WeatherDescription = "Sunny";
        }

        protected override async Task ReloadExecute()
        {
            await SetupItems();
        }

        protected override async Task SetupItems()
        {
            Items = new ObservableCollection<ForecastListItemVM>();

            DayWeatherParameter param = new DayWeatherParameter
            {
                WeekDay = DateTime.Now,
                ForecastDate = DateTime.Now,
                MaxTemp = 5,
                MinTemp = 1
            };

            Items.Add(new ForecastListItemVM(param));
            Items.Add(new ForecastListItemVM(param));
            Items.Add(new ForecastListItemVM(param));
        }

        protected override async Task ItemSelectedExecute(ForecastListItemVM item)
        {
            await NavigationService.Navigate<ForecastDetailsVM, ForecastDetailsParameter>(new ForecastDetailsParameter
            {
                ForecastDate = item.ForecastDate
            });
        }

        #endregion
    }
}
