using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherToday.Core.Models.Weather
{
    public class DailyForecastModel
    {
        public DateTime WeekDay { get; set; }

        public DateTime Date { get; set; }

        public string MaxTemp { get; set; }

        public string MinTemp { get; set; }

        public string Description { get; set; }

        public string Humidity { get; set; }

        public string Pressure { get; set; }
        
        public string WindSpeed { get; set; }

        public string WindDirection { get; set; }

        public string IconValue { get; set; }
    }
}
