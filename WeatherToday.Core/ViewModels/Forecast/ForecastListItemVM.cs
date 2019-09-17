using System;
using WeatherToday.Core.Models.Parameters;
using WeatherToday.Core.ViewModels.Collection;

namespace WeatherToday.Core.ViewModels.Forecast
{
    public class ForecastListItemVM : CollectionItemVM
    {
        public string Description { get; set; }

        public string Humidity { get; set; }

        public string Pressure { get; set; }

        public string WindSpeed { get; set; }

        public string WindDirection { get; set; }

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

        private string _maxTemp;
        public string MaxTemp
        {
            get => _maxTemp;
            set => SetProperty(ref _maxTemp, value);
        }

        private string _minTemp;
        public string MinTemp
        {
            get => _minTemp;
            set => SetProperty(ref _minTemp, value);
        }

        private string _iconValue;
        public string IconValue
        {
            get => _iconValue;
            set => SetProperty(ref _iconValue, value);
        }

        #region Constructor

        public ForecastListItemVM(DayWeatherParameter param)
        {
            WeekDay = param.WeekDay;
            ForecastDate = param.ForecastDate;
            MaxTemp = param.MaxTemp;
            MinTemp = param.MinTemp;
            IconValue = param.IconValue;
            Description = param.Description;
            Humidity = param.Humidity;
            Pressure = param.Pressure;
            WindSpeed = param.WindSpeed;
            WindDirection = param.WindDirection;
        }

        #endregion
    }
}
