using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherToday.API.REST;
using WeatherToday.Core.Models.Weather;
using Xamarin.Essentials;

namespace WeatherToday.Core.Services.Implementation
{
    public class CalculationService : IWeatherService
    {
        private async Task<string> GetLocationFromCity(string cityName)
        {
            string coordinates = string.Empty;

            try
            {
                var locations = await Geocoding.GetLocationsAsync(cityName);

                var location = locations?.FirstOrDefault();
                if (location != null)
                {
                    coordinates = location.Latitude.ToString().Replace(',', '.') + "," + location.Longitude.ToString().Replace(',', '.');
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Console.WriteLine(fnsEx.ToString());
                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return string.Empty;
            }

            return coordinates;
        }

        private async Task<string> GetCountryFromCoord(string coordinates)
        {
            string countryName = string.Empty;

            try
            {
                char[] spearator = {','};

                string[] strList = coordinates.Split(spearator);

                var latitude = Double.Parse(strList[0]);
                var longtitude = Double.Parse(strList[1]);

                var placemarks = await Geocoding.GetPlacemarksAsync(latitude, longtitude);

                var placemark = placemarks?.FirstOrDefault();

                if (placemark != null)
                {
                    countryName = placemark.CountryName;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Console.WriteLine(fnsEx.ToString());
                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return string.Empty;
            }

            return countryName;
        }

        public async Task<WeatherListModel> GetWeatherAsync(string cityName)
        {
            WeatherListModel model = null;

            string coord = await GetLocationFromCity(cityName);

            var resultObject = await WeatherClient.GetWeather(coord);

            if (resultObject != null)
            {
                model = new WeatherListModel();

                model.Temperature = resultObject["currently"]["temperature"].ToString();
                model.City = cityName;
                model.Country = await GetCountryFromCoord(coord);
                model.WeatherDescription = resultObject["currently"]["summary"].ToString();
                model.Date = (new DateTime(1970, 1, 1)).AddSeconds(Double.Parse(resultObject["currently"]["time"].ToString())).AddHours(Double.Parse(resultObject["offset"].ToString()));
                model.IconValue = resultObject["currently"]["icon"].ToString();
            }

            return model;
        }

        public async Task<List<DailyForecastModel>> GetForecastAsync(string cityName)
        {
            List<DailyForecastModel> modelList = null;

            string coord = await GetLocationFromCity(cityName);

            var resultObject = await WeatherClient.GetForecast(coord);

            if (resultObject != null)
            {
                modelList = new List<DailyForecastModel>();

                foreach (var day in resultObject["daily"]["data"])
                {
                    modelList.Add(new DailyForecastModel
                    {
                        WeekDay = (new DateTime(1970, 1, 1)).AddSeconds(Double.Parse(day["time"].ToString())),
                        Date = (new DateTime(1970, 1, 1)).AddSeconds(Double.Parse(day["time"].ToString())),
                        MaxTemp = day["temperatureMax"].ToString(),
                        MinTemp = day["temperatureMin"].ToString(),
                        Description = day["summary"].ToString(),
                        Humidity = day["humidity"].ToString(),
                        Pressure = day["pressure"].ToString(),
                        WindSpeed = day["windSpeed"].ToString(),
                        WindDirection = day["windBearing"].ToString(),
                        IconValue = day["icon"].ToString()
                    });
                }
            }

            return modelList;
        }
    }
}
