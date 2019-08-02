using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MvvmCross;
using WeatherToday.API.Services.Platform;
using WeatherToday.Core.Services.Platform;

namespace WeatherToday.Android.Services
{
    public class AnalitycsService : IAnalitycsService
    {
        const string ScreenTag = "Screen";
        const string DescriptionTag = "Description";
        const string AppVersionTag = "App version";
        const string OSVersionTag = "OS version";
        const string ErrorTag = "UNHANDLED";

        IDeviceService _deviceService
        {
            get => Mvx.IoCProvider.Resolve<IDeviceService>();
        }

        public void TrackCustomEvent(string actionName, string description = "", string screen = "")
        {
            Task.Run(() =>
            {
                Dictionary<string, string> eventDictionary = new Dictionary<string, string>();

                if (!string.IsNullOrEmpty(description))
                    eventDictionary.Add(DescriptionTag, description);

                if (!string.IsNullOrEmpty(screen))
                    eventDictionary.Add(ScreenTag, screen);

                Analytics.TrackEvent(actionName, eventDictionary);
            });
        }

        public void TrackError(Exception ex, string screen = "")
        {
            Task.Run(() =>
            {
                Dictionary<string, string> eventDictionary = new Dictionary<string, string>
                {
                    //{ AppVersionTag, _deviceService.GetAppVersion().ToString() },
                    { OSVersionTag, _deviceService.OSVersion }
                };

                if (!string.IsNullOrEmpty(screen))
                    eventDictionary.Add(ScreenTag, screen);

                Crashes.TrackError(ex, eventDictionary);
            });
        }
    }
}