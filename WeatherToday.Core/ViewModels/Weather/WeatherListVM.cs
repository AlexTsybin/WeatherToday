using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WeatherToday.Core.Messages;
using WeatherToday.Core.Models.City;
using WeatherToday.Core.Models.Parameters;
using WeatherToday.Core.Models.Weather;
using WeatherToday.Core.Services;
using WeatherToday.Core.Services.Platform;
using WeatherToday.Core.ViewModels.Base.Commands;
using WeatherToday.Core.ViewModels.CityList;
using WeatherToday.Core.ViewModels.Collection;
using WeatherToday.Core.ViewModels.EditCity;
using WeatherToday.Core.ViewModels.Forecast;
using WeatherToday.Core.ViewModels.Location;
using WeatherToday.Localization;

namespace WeatherToday.Core.ViewModels.Weather
{
    public class WeatherListVM : BaseCollectionVM<WeatherListItemVM>
    {
        #region Fields

        private MvxSubscriptionToken _citiesUpdatedToken;

        #endregion

        #region Commands

        private IMvxAsyncCommand _cityListCommand;
        public IMvxAsyncCommand CityListCommand
        {
            get => _cityListCommand ?? (_cityListCommand = new TorAsyncCommand(CityListExecute, null, true));
        }

        private IMvxAsyncCommand _locationCommand;
        public IMvxAsyncCommand LocationCommand
        {
            get => _locationCommand ?? (_locationCommand = new TorAsyncCommand(LocationExecute, null, true));
        }

        private IMvxAsyncCommand _addNewCityCommand;
        public IMvxAsyncCommand AddNewCityCommand
        {
            get => _addNewCityCommand ?? (_addNewCityCommand = new TorAsyncCommand(AddNewCity, null, true));
        }

        #endregion

        #region Services

        protected readonly IWeatherService _weatherService;

        #endregion

        #region Constructor

        public WeatherListVM(IMvxLogProvider logProvider, IMvxNavigationService navigationService,
            IUserInteraction userInteraction, IMvxMessenger messenger, IWeatherService weatherService)
            : base(logProvider, navigationService, userInteraction, messenger)
        {
            _weatherService = weatherService;
        }

        #endregion

        #region Private

        private async Task CityListExecute()
        {
            await NavigationService.Navigate<CityListVM>();
        }

        private async Task LocationExecute()
        {
            await NavigationService.Navigate<LocationVM>();
        }

        private async Task AddNewCity()
        {
            await NavigationService.Navigate<EditCityVM>();
        }

        private async void CitiesUpdatedExecute(CitiesUpdatedMessage message)
        {
            Items.Clear();

            await SetupItems();
        }

        #endregion

        #region Protected

        protected override Task ReloadExecute()
        {
            IsRefreshing = false;

            return Task.CompletedTask;
        }

        protected async override Task ItemSelectedExecute(WeatherListItemVM item)
        {
            await NavigationService.Navigate<ForecastVM, ForecastParameter>(new ForecastParameter
            {
                CityName = item.CityName
            });
        }

        protected override async Task SetupItems()
        {
            List<CityBO> cityList = await App.Database.GetCitiesAsync();

            foreach (CityBO city in cityList)
            {
                WeatherListModel resultModel = await _weatherService.GetWeatherAsync(city.CityName);

                if (resultModel != null)
                {
                    WeatherListItemParameter param = new WeatherListItemParameter
                    {
                        Temperature = resultModel.Temperature,
                        City = resultModel.City,
                        Description = resultModel.WeatherDescription,
                        Date = resultModel.Date,
                        Time = resultModel.Date
                    };

                    Items.Add(new WeatherListItemVM(param));
                } 
            }
        }

        #endregion

        #region Public

        public override async Task Initialize()
        {
            await base.Initialize();
        }

        public override void Prepare()
        {
            base.Prepare();

            PageTitle = Strings.weather_today;
            Items = new ObservableCollection<WeatherListItemVM>();

            _citiesUpdatedToken = Messenger.Subscribe<CitiesUpdatedMessage>(CitiesUpdatedExecute);
        }

        public override void Unbind()
        {
            base.Unbind();

            DisposeMvxMessageToken<CitiesUpdatedMessage>(_citiesUpdatedToken);
        }

        #endregion
    }
}
