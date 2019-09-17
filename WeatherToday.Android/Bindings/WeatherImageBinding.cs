using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding;
using MvvmCross.Platforms.Android.Binding.Target;

namespace WeatherToday.Android.Bindings
{
    public class WeatherImageBinding : MvxAndroidTargetBinding
    {
        private readonly ImageView _imageView;

        public WeatherImageBinding(ImageView view) : base(view)
        {
            _imageView = view;
        }

        #region Private

        private int GetImageResource(string name)
        {
            int resourceId = 0;

            switch (name)
            {
                case "clear-day":
                    resourceId = Resource.Drawable.ic_weather_clear_day;
                    break;
                case "clear-night":
                    resourceId = Resource.Drawable.ic_weather_clear_night;
                    break;
                case "rain":
                    resourceId = Resource.Drawable.ic_weather_rain;
                    break;
                case "snow":
                    resourceId = Resource.Drawable.ic_weather_snow;
                    break;
                case "sleet":
                    resourceId = Resource.Drawable.ic_weather_sleet;
                    break;
                case "wind":
                    resourceId = Resource.Drawable.ic_weather_wind;
                    break;
                case "fog":
                    resourceId = Resource.Drawable.ic_weather_fog;
                    break;
                case "cloudy":
                    resourceId = Resource.Drawable.ic_weather_cloudy;
                    break;
                case "partly-cloudy-day":
                    resourceId = Resource.Drawable.ic_weather_partly_cloudy_day;
                    break;
                case "partly-cloudy-night":
                    resourceId = Resource.Drawable.ic_weather_partly_cloudy_night;
                    break;
                default:
                    resourceId = Resource.Drawable.ic_weather_undefined;
                    break;
            }

            return resourceId;
        }

        #endregion

        protected override void SetValueImpl(object target, object value)
        {
            // to do logic
        }

        public override void SetValue(object value)
        {
            string name = (string)value;

            if (name == null)
                return;

            int resourceId = GetImageResource(name);

            _imageView.SetImageResource(resourceId);
        }

        public override Type TargetType
        {
            get => typeof(string);
        }

        public override MvxBindingMode DefaultMode
        {
            get => MvxBindingMode.TwoWay;
        }
    }
}