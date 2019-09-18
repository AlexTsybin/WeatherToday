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
        private static readonly string apiBase = "https://api.darksky.net/forecast/";
        private static readonly string apiKey = "6bf5d18a3bb41e148f232e3838870ab4";
        private static readonly string unit = Strings.metric;

        public static async Task<JObject> GetWeather(double[] coordinates)
        {
            if (coordinates == null || coordinates.Length == 0)
                return null;

            string lat = coordinates[0].ToString().Replace(",", ".");
            string lon = coordinates[1].ToString().Replace(",", ".");

            string url = apiBase + apiKey + "/" + lat + "," + lon + "?exclude=minutely,hourly,daily,alerts,flags" + "&units=" + unit;

            var handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            string result = await client.GetStringAsync(url);

            return JObject.Parse(result);
        }

        public static async Task<JObject> GetForecast(double[] coordinates)
        {
            if (coordinates == null || coordinates.Length == 0)
                return null;

            string lat = coordinates[0].ToString().Replace(",", ".");
            string lon = coordinates[1].ToString().Replace(",", ".");

            string url = apiBase + apiKey + "/" + lat + "," + lon + "?exclude=currently,minutely,hourly,alerts,flags" + "&units=" + unit;

            var handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            string result = await client.GetStringAsync(url);

            return JObject.Parse(result);
        }
    }
}
