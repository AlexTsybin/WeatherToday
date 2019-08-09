using MvvmCross;
using MvvmCross.Base;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using WeatherToday.Core.Services.Platform;
using WeatherToday.Core.ViewModels.Base.Commands;
using WeatherToday.Core.ViewModels.Collection;
using WeatherToday.Core.ViewModels.EditCity;

namespace WeatherToday.Core.ViewModels.CityList
{
    public class CityListVM : BaseCollectionVM<CityListItemVM>
    {
        #region Commands

        private IMvxAsyncCommand<CityListItemVM> _deleteCurrentItemCommand;
        public IMvxAsyncCommand<CityListItemVM> DeleteCurrentItemCommand
        {
            get => _deleteCurrentItemCommand ?? (_deleteCurrentItemCommand = new TorAsyncCommand<CityListItemVM>(DeleteCityExecute));
        }

        private IMvxAsyncCommand _addNewCityCommand;
        public IMvxAsyncCommand AddNewCityCommand
        {
            get => _addNewCityCommand ?? (_addNewCityCommand = new TorAsyncCommand(AddNewCity, null, true));
        }

        #endregion

        #region Constructor

        public CityListVM(IMvxLogProvider logProvider, IMvxNavigationService navigationService,
            IUserInteraction userInteraction, IMvxMessenger messenger)
            : base(logProvider, navigationService, userInteraction, messenger)
        {
        }

        #endregion

        #region Private

        private async Task DeleteCityExecute(CityListItemVM city)
        {

        }

        private async Task AddNewCity()
        {
            //Items.Add(new CityListItemVM("Palma de Mallorca"));
            await NavigationService.Navigate<EditCityVM>();
        }

        #endregion

        #region Protected

        protected override Task ItemSelectedExecute(CityListItemVM item)
        {
            return null;
        }

        protected override Task ReloadExecute()
        {
            return null;
        }

        protected async override Task SetupItems()
        {
            var cityList = new List<CityListItemVM>();

            await Mvx.IoCProvider.Resolve<IMvxMainThreadAsyncDispatcher>().ExecuteOnMainThreadAsync(() =>
            {
                Items.Add(new CityListItemVM("Saint Petersburg"));
                Items.Add(new CityListItemVM("Helsinki"));
                Items.Add(new CityListItemVM("Amsterdam"));
                Items.Add(new CityListItemVM("Palma de Mallorca"));
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

            Items = new ObservableCollection<CityListItemVM>();
        }

        #endregion
    }
}
