using System;
using System.Collections.Generic;
using System.Text;
using WeatherToday.Core.Models.Parameters;
using WeatherToday.Core.ViewModels.Collection;

namespace WeatherToday.Core.ViewModels.Weather
{
    public class WeatherListItemVM : CollectionItemVM
    {
        #region Properties

        public string Country { get; set; }

        private int _cityId;
        public int CityId
        {
            get => _cityId;
            set => SetProperty(ref _cityId, value);
        }

        private string _temperature;
        public string Temperature
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

        private string _iconValue;
        public string IconValue
        {
            get => _iconValue;
            set => SetProperty(ref _iconValue, value);
        }

        #endregion

        #region Constructor

        public WeatherListItemVM(WeatherListItemParameter param)
        {
            Temperature = param.Temperature;
            CityName = param.City;
            WeatherDescription = param.Description;
            WeatherDate = param.Date;
            WeatherTime = param.Time;
            Country = param.Country;
            IconValue = param.IconValue;
        }

        #endregion
    }
}
