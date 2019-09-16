using System;
using WeatherToday.Core.Models.Parameters;
using WeatherToday.Core.ViewModels.Collection;

namespace WeatherToday.Core.ViewModels.Forecast
{
    public class ForecastListItemVM : CollectionItemVM
    {
        private DateTime _weekDay;
        public DateTime WeekDay
        {
            get => _weekDay;
            set => SetProperty(ref _weekDay, value);
        }

        private DateTime _forecastDate;
        public DateTime ForecastDate
        {
            get => _forecastDate;
            set => SetProperty(ref _forecastDate, value);
        }

        private int _maxTemp;
        public int MaxTemp
        {
            get => _maxTemp;
            set => SetProperty(ref _maxTemp, value);
        }

        private int _minTemp;
        public int MinTemp
        {
            get => _minTemp;
            set => SetProperty(ref _minTemp, value);
        }

        #region Constructor

        public ForecastListItemVM(DayWeatherParameter param)
        {
            WeekDay = param.WeekDay;
            ForecastDate = param.ForecastDate;
            MaxTemp = param.MaxTemp;
            MinTemp = param.MinTemp;
        }

        #endregion
    }
}
