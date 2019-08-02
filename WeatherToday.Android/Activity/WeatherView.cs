using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views.InputMethods;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using WeatherToday.Core.ViewModels.Base;
using WeatherToday.Core.ViewModels.Weather;

namespace WeatherToday.Android.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "WeatherApp", LaunchMode = LaunchMode.SingleTop, MainLauncher = true)]
    public class WeatherView : MvxAppCompatActivity<WeatherViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.weather_activity);
        }

        public void HideSoftKeyboard()
        {
            if (CurrentFocus == null)
                return;

            InputMethodManager inputMethodManager = (InputMethodManager)GetSystemService(InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);

            CurrentFocus.ClearFocus();
        }
    }
}