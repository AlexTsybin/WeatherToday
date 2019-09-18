using MvvmCross;
using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherToday.Core.Services;
using WeatherToday.Core.ViewModels.Base;
using WeatherToday.Core.ViewModels.Base.Commands;

namespace WeatherToday.Core.ViewModels.Location
{
    public class LocationVM : BaseVM
    {
        #region Properties

        private double _lat;
        public double Lat
        {
            get => _lat;
            set
            {
                _lat = value;
                Latitude = value.ToString();
            }
        }

        private double _lon;
        public double Lon
        {
            get => _lon;
            set
            {
                _lon = value;
                Longtitude = value.ToString();
            }
        }

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

        #region Public

        public async void UpdateCity(double lat, double lon)
        {
            LocationCity = await Mvx.IoCProvider.Resolve<IWeatherService>().GetCityFromCoordinates(lat, lon);
        }

        public async override Task Initialize()
        {
            await base.Initialize();
        }

        public override void Prepare()
        {
            base.Prepare();
        }

        #endregion
    }
}
