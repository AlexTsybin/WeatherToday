using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherToday.Core.ViewModels.Base;
using WeatherToday.Core.ViewModels.Base.Commands;

namespace WeatherToday.Core.ViewModels.Location
{
    public class LocationVM : BaseVM
    {
        #region Properties

        private string _latitude;
        public string Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }

        private string _longtitude;
        public string Longtitude
        {
            get => _longtitude;
            set => SetProperty(ref _longtitude, value);
        }

        private string _locationCity;
        public string LocationCity
        {
            get => _locationCity;
            set => SetProperty(ref _locationCity, value);
        }

        #endregion

        #region Commands

        private IMvxAsyncCommand _getLocationCommand;
        public IMvxAsyncCommand GetLocationCommand
        {
            get => _getLocationCommand ?? (_getLocationCommand = new TorAsyncCommand(GetLocationExecute, null, true));
        }

        #endregion

        #region Private

        private async Task GetLocationExecute()
        {
            await Task.CompletedTask;
        }

        #endregion

        #region Public

        public async override Task Initialize()
        {
            await base.Initialize();

            Latitude = Convert.ToString(59.9431705);
            Longtitude = Convert.ToString(30.3454007);
            LocationCity = "St.Petersburg";
        }

        public override void Prepare()
        {
            base.Prepare();
        }

        #endregion
    }
}
