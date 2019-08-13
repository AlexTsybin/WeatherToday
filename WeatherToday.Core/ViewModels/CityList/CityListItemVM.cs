using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherToday.Core.ViewModels.Base.Commands;
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

        #region Commands

        private IMvxAsyncCommand<CityListItemVM> _delCommand;
        public IMvxAsyncCommand<CityListItemVM> DelCommand { get => _delCommand; set => _delCommand = value; }

        private IMvxAsyncCommand _deleteCityCommand;
        public IMvxAsyncCommand DeleteCityCommand
        {
            get => _deleteCityCommand ?? (_deleteCityCommand = new TorAsyncCommand(DeleteCityExecute));
        }

        #endregion

        #region Constructor

        public CityListItemVM(string city, IMvxAsyncCommand<CityListItemVM> deleteCommand)
        {
            CityName = city;

            DelCommand = deleteCommand;
        }

        #endregion

        #region Public

        public async Task DeleteCityExecute()
        {
            await DelCommand.ExecuteAsync(this);
        }

        #endregion
    }
}
