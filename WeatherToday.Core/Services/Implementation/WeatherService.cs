using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using WeatherToday.API.REST;
using WeatherToday.Core.Models.Weather;

namespace WeatherToday.Core.Services.Implementation
{
    public class CalculationService : IWeatherService
    {
        public async Task<WeatherListModel> GetWeatherAsync(string cityName)
        {
            WeatherListModel model = null;

            var resultObject = await WeatherClient.GetWeather(cityName);

            if (resultObject != null)
            {
                model = new WeatherListModel();

                model.Temperature = resultObject["main"]["temp"].ToString();
                model.City = resultObject["name"].ToString();
                model.WeatherDescription = resultObject["weather"][0]["description"].ToString();
                model.Date = DateTime.Now;
            }

            return model;
        }
    }
}
