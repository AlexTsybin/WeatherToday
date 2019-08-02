using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using WeatherToday.Core.Services.Platform;
using WeatherToday.Core.ViewModels.Base;
using WeatherToday.Core.ViewModels.Base.Commands;
using WeatherToday.Core.ViewModels.Base.Interfaces;

namespace WeatherToday.Core.ViewModels.Collection
{
    public abstract class BaseCollectionVM<ItemType> : WeatherViewModel, IBaseCollectionVM<ItemType> 
        where ItemType : CollectionItemVM
    {
        #region Commands

        private IMvxAsyncCommand<ItemType> _itemSelectedCommand;
        public IMvxAsyncCommand<ItemType> ItemSelectedCommand
        {
            get => _itemSelectedCommand ?? (_itemSelectedCommand = new TorAsyncCommand<ItemType>(ItemSelectedExecute, null, true));
        }

        private IMvxAsyncCommand _reloadCommand;
        public IMvxAsyncCommand ReloadCommand
        {
            get => _reloadCommand ?? (_reloadCommand = new TorAsyncCommand(ReloadExecute));
        }

        #endregion

        #region Properties

        private bool _loadingMore;
        public bool LoadingMore
        {
            get => _loadingMore;
            set => SetProperty(ref _loadingMore, value);
        }

        private int _lastRequestedItemId;
        public int LastRequestedItemId
        {
            get => _lastRequestedItemId;
            set => SetProperty(ref _lastRequestedItemId, value);
        }

        protected ObservableCollection<ItemType> _items;
        public virtual ObservableCollection<ItemType> Items
        {
            get => _items;
            set
            {
                _items = value;

                if (_items != null)
                    _items.CollectionChanged += ObserveCollection;

                RaisePropertyChanged(() => Items);

                ItemsChanged();
            }
        }

        protected List<ItemType> _allItems;
        public List<ItemType> AllItems
        {
            get => _allItems;
        }

        protected bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;

                RaisePropertyChanged(() => IsRefreshing);

                CheckItems();
            }
        }

        private bool _isItemsAvailable;
        public bool IsItemsAvailable
        {
            get => _isItemsAvailable;
            set
            {
                _isItemsAvailable = value;
                RaisePropertyChanged(() => IsItemsAvailable);
            }
        }

        public override bool Loading
        {
            get => base.Loading;
            set
            {
                base.Loading = value;
                CheckItems();
            }
        }

        #endregion

        #region Services

        protected IDeviceService DeviceService;

        #endregion

        #region Constructor

        public BaseCollectionVM(IMvxNavigationService navigationService,
            IUserInteraction userInteraction, IMvxMessenger messenger)
            : base(navigationService, userInteraction, messenger)
        {
            DeviceService = Mvx.IoCProvider.Resolve<IDeviceService>();
        }

        public BaseCollectionVM() : base()
        {
            DeviceService = Mvx.IoCProvider.Resolve<IDeviceService>();
        }

        #endregion

        #region Private

        private void ObserveCollection(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            CheckItems();
        }

        private void CheckItems()
        {
            if (!IsRefreshing && !Loading && (_items == null || _items.Count == 0))
                IsItemsAvailable = false;
            else
                IsItemsAvailable = true;
        }

        #endregion

        #region Protected

        protected abstract Task ReloadExecute();

        protected abstract Task ItemSelectedExecute(ItemType item);

        protected abstract Task SetupItems();

        protected virtual void ItemsChanged()
        {
            CheckItems();
        }

        #endregion

        #region Public

        public virtual async Task LoadMore(bool loadNew = true)
        {
        }

        public async override Task Initialize()
        {
            await Task.WhenAll(base.Initialize(), SetupItems());
        }

        public override void Reload()
        {
            base.Reload();
            //RaisePropertyChanged(() => SelectedItemIndex);
        }

        #endregion
    }
}
