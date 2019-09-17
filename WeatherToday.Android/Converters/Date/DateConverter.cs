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

namespace WeatherToday.Android.Converters.Date
{
    #region Forecast

    public  class WeekDayValueConverter : MvxValueConverter<DateTime, string>
    {
        protected override string Convert(DateTime value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format(CultureInfo.InvariantCulture, Strings.week_day_date, value);
        }
    }

    public class ShortDateValueConverter : MvxValueConverter<DateTime, string>
    {
        protected override string Convert(DateTime value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format(CultureInfo.InvariantCulture, Strings.short_date, value);
        }
    }

    public class LongDateValueConverter : MvxValueConverter<DateTime, string>
    {
        protected override string Convert(DateTime value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format(CultureInfo.InvariantCulture, Strings.long_date, value);
        }
    }

    public class TimeValueConverter : MvxValueConverter<DateTime, string>
    {
        protected override string Convert(DateTime value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format(CultureInfo.InvariantCulture, Strings.short_time, value);
        }
    }

    #endregion


}