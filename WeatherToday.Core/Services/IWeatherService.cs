using System.Threading.Tasks;
using WeatherToday.Core.Models.Weather;

namespace WeatherToday.Core.Services
{
    public interface IWeatherService
    {
        Task<WeatherListModel> GetWeatherAsync(string cityName);
    }
}
