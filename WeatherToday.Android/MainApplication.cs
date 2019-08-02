using Android.App;
using Android.Runtime;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Views;
using System;
using WeatherToday.Core;

namespace WeatherToday.Android
{
    [Application]
    public class MainApplication : MvxAppCompatApplication<Setup, App>
    {
        public MainApplication()
        {
        }

        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }
    }
}