using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherToday.Core.Models.Weather;

namespace WeatherToday.Core.Services
{
    public interface IWeatherService
    {
        Task<WeatherListModel> GetWeatherAsync(string cityName);

        Task<List<DailyForecastModel>> GetForecastAsync(string cityName);

        Task<List<HourlyForecastModel>> GetHourlyForecastAsync(string cityName);

        Task<string> GetCityFromCoordinates(double lat, double lon);
    }
}
