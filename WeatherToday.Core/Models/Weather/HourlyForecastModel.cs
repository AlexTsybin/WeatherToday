using System;

namespace WeatherToday.Core.Models.Weather
{
    public class HourlyForecastModel
    {
        public DateTime Time { get; set; }

        public string Temp { get; set; }

        public string IconValue { get; set; }
    }
}
