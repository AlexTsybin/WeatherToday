using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherToday.Core.Models.Parameters
{
    public class HourForecastParameter
    {
        public DateTime Time { get; set; }

        public string Temp { get; set; }

        public string IconValue { get; set; }
    }
}
