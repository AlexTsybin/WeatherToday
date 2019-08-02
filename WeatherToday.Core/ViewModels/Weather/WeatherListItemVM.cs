using System;
using System.Collections.Generic;
using System.Text;
using WeatherToday.Core.ViewModels.Collection;

namespace WeatherToday.Core.ViewModels.Weather
{
    public class WeatherListItemVM : CollectionItemVM
    {
        #region Properties

        private int _temperature;
        public int Temperature
        {
            get => _temperature;
            set => SetProperty(ref _temperature, value);
        }

        private string _cityName;
        public string CityName
        {
            get => _cityName;
            set => SetProperty(ref _cityName, value);
        }

        private string _weatherDescription;
        public string WeatherDescription
        {
            get => _weatherDescription;
            set => SetProperty(ref _weatherDescription, value);
        }

        private DateTime _weatherDate;
        public DateTime WeatherDate
        {
            get => _weatherDate;
            set => SetProperty(ref _weatherDate, value);
        }

        private DateTime _weatherTime;
        public DateTime WeatherTime
        {
            get => _weatherTime;
            set => SetProperty(ref _weatherTime, value);
        }

        #endregion

        #region Constructor

        public WeatherListItemVM(int temp, string city, string weather, DateTime date, DateTime time)
        {
            Temperature = temp;
            CityName = city;
            WeatherDescription = weather;
            WeatherDate = date;
            WeatherTime = time;
        }

        #endregion
    }
}
