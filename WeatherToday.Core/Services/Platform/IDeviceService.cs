using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherToday.Core.Services.Platform
{
    public interface IDeviceService
    {
        string OSVersion { get; }
    }
}
