using Android.OS;
using WeatherToday.Core.Services.Platform;

namespace WeatherToday.Android.Services
{
    public class DeviceService : IDeviceService
    {
        public string OSVersion
        {
            get => string.Format("model:{0} version:{1} versionRelease:{2}", Build.Model, Build.VERSION.Sdk, Build.VERSION.Release);
        }
    }
}