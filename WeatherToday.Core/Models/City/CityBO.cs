using SQLite;
using System;

namespace WeatherToday.Core.Models.City
{
    public class CityBO
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string CityName { get; set; }
    }
}
