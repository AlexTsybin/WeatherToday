using System;
using System.Collections.Generic;
using System.Text;
using WeatherToday.Core.ViewModels.Collection;

namespace WeatherToday.Core.ViewModels.CityList
{
    public class CityListItemVM : CollectionItemVM
    {
        #region Properties

        private string _cityName;
        public string CityName
        {
            get => _cityName;
            set => SetProperty(ref _cityName, value);
        }

        #endregion

        #region Constructor

        public CityListItemVM(string city)
        {
            CityName = city;
        }

        #endregion
    }
}
