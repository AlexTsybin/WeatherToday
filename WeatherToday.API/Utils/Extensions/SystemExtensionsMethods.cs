using MvvmCross;
using MvvmCross.Logging;
using System;
using System.Text;
using WeatherToday.API.Services.Platform;

namespace WeatherToday.API.Utils.Extensions
{
    public static class SystemExtensionsMethods
    {
        public static void HandleEx(this Exception exception)
        {
            if (!Mvx.IoCProvider.TryResolve(out IAnalitycsService analitycsService))
                return;

#if DEBUG
            if (Mvx.IoCProvider.TryResolve(out IMvxLog logService))
                logService.ErrorException("\nUNHANDLED:\n", exception);
#else
            analitycsService.TrackError(exception?.InnerException ?? exception);
#endif
        }

        public static string BuildAllStackTrace(this Exception exception)
        {
            var innerException = exception;

            var messageBuilder = new StringBuilder();
            messageBuilder.AppendLine(string.Format("{0} \n {1} \n ", exception.Message, exception.StackTrace));

            while (innerException.InnerException != null)
            {
                innerException = innerException.InnerException;
                messageBuilder.AppendLine(string.Format("{0} \n {1} \n ", innerException.Message, innerException.StackTrace));
            }

            return messageBuilder.ToString();
        }
    }
}
