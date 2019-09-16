using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WeatherToday.Core.Models.Parameters;
using WeatherToday.Core.Services.Platform;
using WeatherToday.Core.ViewModels.Base;

namespace WeatherToday.Core.ViewModels.Forecast
{
    public class ForecastVM : BaseVM<ForecastParameter>
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

        private ObservableCollection<ForecastListItemVM> _daysList;
        public ObservableCollection<ForecastListItemVM> DaysList
        {
            get => _daysList;
            set => SetProperty(ref _daysList, value);
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

        public async override Task Initialize()
        {
            await base.Initialize();

            DaysList = new ObservableCollection<ForecastListItemVM>();

            DayWeatherParameter param = new DayWeatherParameter
            {
                Temperature = 5
            };

            DaysList.Add(new ForecastListItemVM(param));
        }

        public override void Prepare(ForecastParameter parameter)
        {
            CityName = parameter.CityName;
            CountryName = "Russia";
            CurrentTemperature = "-7";
            WeatherDescription = "Sunny";
        }

        #endregion
    }
}
