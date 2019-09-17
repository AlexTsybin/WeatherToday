using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherToday.Core.Models.Parameters
{
    public class ForecastDetailsParameter
    {
        public DateTime ForecastDate { get; set; }

        public string Description { get; set; }

        public string MaxTemp { get; set; }

        public string MinTemp { get; set; }

        public string Humidity { get; set; }

        public string Pressure { get; set; }

        public string WindSpeed { get; set; }

        public string WindDirection { get; set; }

        public string IconValue { get; set; }
    }
}
