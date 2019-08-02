using MvvmCross;
using MvvmCross.Base;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WeatherToday.Core.Services.Platform;
using WeatherToday.Core.ViewModels.Base.Commands;
using WeatherToday.Core.ViewModels.Collection;

namespace WeatherToday.Core.ViewModels.Weather
{
    public class WeatherListVM : BaseCollectionVM<WeatherListItemVM>
    {
        #region Commands

        private IMvxAsyncCommand<WeatherListItemVM> _deleteCurrentItemCommand;
        public IMvxAsyncCommand<WeatherListItemVM> DeleteCurrentItemCommand
        {
            get => _deleteCurrentItemCommand ?? (_deleteCurrentItemCommand = new TorAsyncCommand<WeatherListItemVM>(DeleteCityExecute));
        }

        #endregion

        #region Constructor

        public WeatherListVM(IMvxNavigationService navigationService,
            IUserInteraction userInteraction, IMvxMessenger messenger)
            : base(navigationService, userInteraction, messenger)
        {
        }

        #endregion

        #region Private

        private async Task DeleteCityExecute(WeatherListItemVM city)
        {

        }

        #endregion

        #region Protected

        protected override Task ReloadExecute()
        {
            return null;
        }

        protected override Task ItemSelectedExecute(WeatherListItemVM item)
        {
            return null;
        }

        protected override async Task SetupItems()
        {
            var cityList = new List<WeatherListItemVM>();

            await Mvx.IoCProvider.Resolve<IMvxMainThreadAsyncDispatcher>().ExecuteOnMainThreadAsync(() =>
            {
                Items.Add(new WeatherListItemVM(10, "Saint Petersburg", "Sunny", DateTime.Now, DateTime.Now));
                Items.Add(new WeatherListItemVM(15, "Helsinki", "Sunny", DateTime.Now, DateTime.Now));
                Items.Add(new WeatherListItemVM(21, "Amsterdam", "Cloudy", DateTime.Now, DateTime.Now));
            });
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

            Items = new ObservableCollection<WeatherListItemVM>();
        }

        #endregion
    }
}
