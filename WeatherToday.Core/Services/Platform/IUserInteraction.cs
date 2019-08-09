using System;
using System.Threading.Tasks;

namespace WeatherToday.Core.Services.Platform
{
    public interface IUserInteraction
    {
        Task AlertAsync(string message, string title = null, string okButton = "OK");

        void Alert(string message, Action done = null, string title = null, string okButton = "OK");

        void ShowCustomNotification(string message, int duration = 3000, string actionName = null, Action action = null);

        void CloseApp();
    }
}
