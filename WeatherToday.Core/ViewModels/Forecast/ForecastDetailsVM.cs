using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherToday.Core.Models.Parameters;
using WeatherToday.Core.ViewModels.Base;
using WeatherToday.Localization;

namespace WeatherToday.Core.ViewModels.Forecast
{
    public class ForecastDetailsVM : BaseVM<ForecastDetailsParameter>
    {
        #region Fields

        private string _windSpeed;
        private int _windDegree;
        private string _humidityProportion;

        #endregion

        #region Properties

        private DateTime _forecastDate;
        public DateTime ForecastDate
        {
            get => _forecastDate;
            set => SetProperty(ref _forecastDate, value);
        }

        private string _iconValue;
        public string IconValue
        {
            get => _iconValue;
            set => SetProperty(ref _iconValue, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
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

        private string _humidity;
        public string Humidity
        {
            get => _humidity;
            set => SetProperty(ref _humidity, value);
        }

        private string _pressure;
        public string Pressure
        {
            get => _pressure;
            set => SetProperty(ref _pressure, value);
        }

        private string _wind;
        public string Wind
        {
            get => _wind;
            set => SetProperty(ref _wind, value);
        }

        #endregion

        #region Private

        private async Task SetupInfo()
        {
            // Wind
            string[] directions = new string[] { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW", "N" };

            var directionIndex = (int)(Math.Floor((_windDegree % 360) / 22.5) + 1);

            Wind = _windSpeed + " m/s  " + directions[directionIndex];

            //Humidity
            Humidity = Double.Parse(_humidityProportion) * 100 + " %";
        }

        #endregion

        #region Public

        public override async Task Initialize()
        {
            await Task.WhenAll(base.Initialize(), SetupInfo());
        }

        public override void Prepare(ForecastDetailsParameter parameter)
        {
            base.Prepare(parameter);

            ForecastDate = parameter.ForecastDate;
            IconValue = parameter.IconValue;
            Description = parameter.Description;
            MaxTemp = parameter.MaxTemp;
            MinTemp = parameter.MinTemp;
            Humidity = parameter.Humidity;
            Pressure = parameter.Pressure + " " + Strings.hectopascal;

            _humidityProportion = parameter.Humidity;
            _windSpeed = parameter.WindSpeed;
            _windDegree = Int16.Parse(parameter.WindDirection);
        }

        #endregion
    }
}
