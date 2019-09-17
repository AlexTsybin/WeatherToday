using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherToday.Core.Models.Parameters
{
    public class WeatherListItemParameter
    {
        public string Temperature { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public string IconValue { get; set; }
    }
}
