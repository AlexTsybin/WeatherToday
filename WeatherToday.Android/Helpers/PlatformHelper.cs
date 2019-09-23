using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Gms.Common;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using WeatherToday.Localization;

namespace WeatherToday.Android.Helpers
{
    public static class PlatformHelper
    {
        public static bool IsGooglePlayServicesInstalled(Context context)
        {
            var queryResult = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(context);
            if (queryResult == ConnectionResult.Success)
            {
                Log.Info("LocationActivity", "Google Play Services is installed on this device.");
                return true;
            }

            if (GoogleApiAvailability.Instance.IsUserResolvableError(queryResult))
            {
                // Check if there is a way the user can resolve the issue
                var errorString = GoogleApiAvailability.Instance.GetErrorString(queryResult);
                Log.Error("LocationActivity", "There is a problem with Google Play Services on this device: {0} - {1}",
                          queryResult, errorString);

                // Alternately, display the error to the user.
            }

            return false;
        }
    }
}