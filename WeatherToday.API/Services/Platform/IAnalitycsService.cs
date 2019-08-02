using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherToday.API.Services.Platform
{
    public interface IAnalitycsService
    {
        void TrackCustomEvent(string actionName, string description = "", string screen = "");

        void TrackError(Exception ex, string screen = "");
    }
}
