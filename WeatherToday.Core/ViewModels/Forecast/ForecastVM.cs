﻿using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WeatherToday.Core.Models.Parameters;
using WeatherToday.Core.Models.Weather;
using WeatherToday.Core.Services;
using WeatherToday.Core.Services.Platform;
using WeatherToday.Core.ViewModels.Base;
using WeatherToday.Core.ViewModels.Collection;

namespace WeatherToday.Core.ViewModels.Forecast
{
    public class ForecastVM : BaseCollectionVM<ForecastListItemVM, ForecastParameter>
    {
        #region Properties

        private string _cityName;
        public string CityName
        {
            get => _cityName;
            set => SetProperty(ref _cityName, value);
        }

        private string _countryName;
        public string CountryName
        {
            get => _countryName;
            set => SetProperty(ref _countryName, value);
        }

        private string _currentTemperature;
        public string CurrentTemperature
        {
            get => _currentTemperature;
            set => SetProperty(ref _currentTemperature, value);
        }

        private string _weatherDescription;
        public string WeatherDescription
        {
            get => _weatherDescription;
            set => SetProperty(ref _weatherDescription, value);
        }

        #endregion

        #region Services

        protected readonly IWeatherService _weatherService;

        #endregion

        #region Constructor

        public ForecastVM(IMvxLogProvider logProvider, IMvxNavigationService navigationService,
            IUserInteraction userInteraction, IMvxMessenger messenger, IWeatherService weatherService)
            : base(logProvider, navigationService, userInteraction, messenger)
        {
            _weatherService = weatherService;
        }

        #endregion

        #region Protected

        protected override async Task ReloadExecute()
        {
            await SetupItems();
        }

        protected override async Task SetupItems()
        {
            Items = new ObservableCollection<ForecastListItemVM>();

            List<DailyForecastModel> resultModel = await _weatherService.GetForecastAsync(CityName);

            if (resultModel == null)
                return;

            foreach (var model in resultModel)
            {
                Items.Add(new ForecastListItemVM(new DayWeatherParameter
                {
                    WeekDay = model.WeekDay,
                    ForecastDate = model.Date,
                    MaxTemp = Math.Round(Double.Parse(model.MaxTemp)).ToString(),
                    MinTemp = Math.Round(Double.Parse(model.MinTemp)).ToString(),
                    IconValue = model.IconValue,
                    Description = model.Description,
                    Humidity = model.Humidity,
                    Pressure = model.Pressure,
                    WindSpeed = model.WindSpeed,
                    WindDirection = model.WindDirection
                }));
            }
        }

        protected override async Task ItemSelectedExecute(ForecastListItemVM item)
        {
            await NavigationService.Navigate<ForecastDetailsVM, ForecastDetailsParameter>(new ForecastDetailsParameter
            {
                ForecastDate = item.ForecastDate,
                Description = item.Description,
                MaxTemp = item.MaxTemp,
                MinTemp = item.MinTemp,
                IconValue = item.IconValue,
                Humidity = item.Humidity,
                Pressure = item.Pressure,
                WindSpeed = item.WindSpeed,
                WindDirection = item.WindDirection
            });
        }

        #endregion

        #region Public

        public override void Prepare(ForecastParameter parameter)
        {
            CityName = parameter.CityName;
            CountryName = parameter.Country;
            CurrentTemperature = parameter.Temperature;
            WeatherDescription = parameter.Description;
        }

        #endregion
    }
}
