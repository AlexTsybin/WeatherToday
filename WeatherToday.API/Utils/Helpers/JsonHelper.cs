using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WeatherToday.API.Utils.Extensions;

namespace WeatherToday.API.Utils.Helpers
{
    public static class JsonHelper
    {
        private static JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, settings);
        }

        public static T Deserialize<T>(string model)
        {

            try
            {
                return JsonConvert.DeserializeObject<T>(model, settings);
            }
            catch (Exception ex)
            {
                ex.HandleEx();
                return default(T);
            }
        }

        public async static Task<T> DeserializeAsync<T>(string model)
        {

            try
            {
                var result = await Task.Run(() => JsonConvert.DeserializeObject<T>(model, settings));
                return result;
            }
            catch (Exception ex)
            {
                ex.HandleEx();
                return default(T);
            }
        }

        public static object Deserialize(string model, Type type)
        {
            try
            {
                var result = JsonConvert.DeserializeObject(model, type, settings);
                return result;
            }
            catch (Exception ex)
            {
                ex.HandleEx();
                return null;
            }
        }

        public async static Task<object> DeserializeAsync(string model, Type type)
        {
            try
            {
                var result = await Task.Run(() => JsonConvert.DeserializeObject(model, type, settings));
                return result;
            }
            catch (Exception ex)
            {
                ex.HandleEx();
                return null;
            }
        }
    }
}
