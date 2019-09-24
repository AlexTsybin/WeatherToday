using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Converters;
using WeatherToday.Localization;

namespace WeatherToday.Android.Converters.String
{
    public class CelsiusValueConverter : MvxValueConverter<string, string>
    {
        protected override string Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}\u00B0", value);
        }
    }
}