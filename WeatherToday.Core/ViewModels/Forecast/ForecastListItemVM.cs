using WeatherToday.Core.Models.Parameters;
using WeatherToday.Core.ViewModels.Collection;

namespace WeatherToday.Core.ViewModels.Forecast
{
    public class ForecastListItemVM : CollectionItemVM
    {
        private int _temperature;
        public int Temperature
        {
            get => _temperature;
            set => SetProperty(ref _temperature, value);
        }

        #region Constructor

        public ForecastListItemVM(DayWeatherParameter param)
        {
            Temperature = param.Temperature;
        }

        #endregion
    }
}
