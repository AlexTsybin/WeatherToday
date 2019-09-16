using System;

namespace WeatherToday.Core.Models.Parameters
{
    public class DayWeatherParameter
    {
        public DateTime WeekDay { get; set; }

        public DateTime ForecastDate { get; set; }

        public int MaxTemp { get; set; }

        public int MinTemp { get; set; }
    }
}
