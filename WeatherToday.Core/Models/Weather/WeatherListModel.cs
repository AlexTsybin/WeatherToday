using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherToday.Core.Models.Weather
{
    public class WeatherListModel
    {
        public string Temperature { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string WeatherDescription { get; set; }

        public DateTime Date { get; set; }

        public string IconValue { get; set; }
    }
}
