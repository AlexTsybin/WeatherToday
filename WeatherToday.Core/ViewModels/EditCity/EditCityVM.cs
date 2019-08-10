using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using WeatherToday.Core.Messages;
using WeatherToday.Core.Models.City;
using WeatherToday.Core.Services.Platform;
using WeatherToday.Core.ViewModels.Base;
using WeatherToday.Core.ViewModels.Base.Commands;

namespace WeatherToday.Core.ViewModels.EditCity
{
    public class EditCityVM : BaseVM
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

        private IMvxAsyncCommand _saveCityCommand;
        public IMvxAsyncCommand SaveCityCommand
        {
            get => _saveCityCommand ?? (_saveCityCommand = new TorAsyncCommand(SaveCityExecute, null, true));
        }

        #endregion

        #region Constructor

        public EditCityVM(IMvxLogProvider logProvider, IMvxNavigationService navigationService,
            IUserInteraction userInteraction, IMvxMessenger messenger)
            : base(logProvider, navigationService, userInteraction, messenger)
        {
        }

        #endregion

        #region Private

        private async Task SaveCityExecute()
        {
            var city = new CityBO();
            city.CityName = CityName;
            await App.Database.SaveCityAsync(city);

            SendUpdateMessage();

            await NavigationService.Close(this);
        }

        private void SendUpdateMessage()
        {
            Messenger.Publish(new CitiesUpdatedMessage(this));
        }

        #endregion

        #region Public

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
