namespace WeatherToday.Core.Models.Parameters
{
    public class ForecastParameter
    {
        public int Id { get; set; }

        public string CityName { get; set; }

        public string Country { get; set; }

        public string Temperature { get; set; }

        public string Description { get; set; }
    }
}
