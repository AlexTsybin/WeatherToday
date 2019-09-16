using System;
using System.Collections.Generic;
using System.Text;
using WeatherToday.Core.Models.Parameters;
using WeatherToday.Core.ViewModels.Base;

namespace WeatherToday.Core.ViewModels.Forecast
{
    public class ForecastDetailsVM : BaseVM<ForecastDetailsParameter>
    {
        #region Properties

        private string _cityName;
        public string CityName
        {
            get => _cityName;
            set => SetProperty(ref _cityName, value);
        }

        #endregion

        #region Public

        public override void Prepare(ForecastDetailsParameter parameter)
        {
            base.Prepare(parameter);

            CityName = "SPb";
        }

        #endregion
    }
}
