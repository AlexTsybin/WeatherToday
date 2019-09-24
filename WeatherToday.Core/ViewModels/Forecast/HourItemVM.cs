using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using WeatherToday.Core.Models.Parameters;
using WeatherToday.Core.ViewModels.Base;

namespace WeatherToday.Core.ViewModels.Forecast
{
    public class HourItemVM : MvxViewModel
    {
        #region Properties

        private DateTime _time;
        public DateTime Time
        {
            get => _time;
            set => SetProperty(ref _time, value);
        }

        private string _temp;
        public string Temp
        {
            get => _temp;
            set => SetProperty(ref _temp, value);
        }

        private string _iconValue;
        public string IconValue
        {
            get => _iconValue;
            set => SetProperty(ref _iconValue, value);
        }

        #endregion

        #region Constructor

        public HourItemVM(HourForecastParameter param)
        {
            Time = param.Time;
            Temp = param.Temp;
            IconValue = param.IconValue;
        }

        #endregion
    }
}
