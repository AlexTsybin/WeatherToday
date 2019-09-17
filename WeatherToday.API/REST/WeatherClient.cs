using MvvmCross;
using MvvmCross.Plugin.Network.Rest;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherToday.Localization;

namespace WeatherToday.API.REST
{
    public static class WeatherClient
    {
        public static async Task<JObject> GetWeather(string cityName)
        {
            string apiKey = "d9d8b84aa13c9c56d2f32d4845d77fb2";
            string apiBase = "https://api.openweathermap.org/data/2.5/weather?q=";
            string unit = Strings.metric;

            if (string.IsNullOrWhiteSpace(cityName))
                return null;

            string url = apiBase + cityName + "&appid=" + apiKey + "&units=" + unit;

            var handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            string result = await client.GetStringAsync(url);

            return JObject.Parse(result);
        }
    }
}
