using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherToday.Core.Services.Platform;
using WeatherToday.Core.ViewModels.Base;

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

        #region Constructor

        public EditCityVM(IMvxLogProvider logProvider, IMvxNavigationService navigationService,
            IUserInteraction userInteraction, IMvxMessenger messenger)
            : base(logProvider, navigationService, userInteraction, messenger)
        {
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
