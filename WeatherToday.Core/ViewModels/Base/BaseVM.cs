using MvvmCross;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeatherToday.API;
using WeatherToday.Core.Messages.Common;
using WeatherToday.Core.Services.Platform;
using WeatherToday.Core.ViewModels.Base.Interfaces;

namespace WeatherToday.Core.ViewModels.Base
{
    public abstract class BaseVM : MvxNavigationViewModel, IBaseVM
    {
        #region Fields

        protected CancellationTokenSource DataLoadingCancellationTokenSource = new CancellationTokenSource();
        private MvxSubscriptionToken _updateVMToken;
        protected readonly List<MvxSubscriptionToken> SubscriptionTokens;

        #endregion

        #region Properties

        private bool _loading;
        public virtual bool Loading
        {
            get => _loading;
            set => SetProperty(ref _loading, value);
        }

        private string _pageTitle;
        public string PageTitle
        {
            get => _pageTitle;
            set => SetProperty(ref _pageTitle, value);
        }

        private string _pageSubtitle;
        public string PageSubtitle
        {
            get => _pageSubtitle;
            set => SetProperty(ref _pageSubtitle, value);
        }

        #endregion

        #region Services

        protected readonly IUserInteraction UserInteractions;
        protected readonly IMvxMessenger Messenger;

        #endregion

        #region Constructor

        protected BaseVM(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserInteraction userInteraction, IMvxMessenger messenger) : base(logProvider, navigationService)
        {
            UserInteractions = userInteraction;
            Messenger = messenger;

            SubscriptionTokens = new List<MvxSubscriptionToken>();
        }

        protected BaseVM() : this(Mvx.IoCProvider.Resolve<IMvxLogProvider>(), Mvx.IoCProvider.Resolve<IMvxNavigationService>(), 
            Mvx.IoCProvider.Resolve<IUserInteraction>(), Mvx.IoCProvider.Resolve<IMvxMessenger>())
        {
        }

        #endregion

        #region Private

        private void UpdateVMExecute(UpdateViewModelMessage message)
        {
            bool isInitialized = Mvx.IoCProvider.Resolve<IAppService>().IsServiceUp;

            if (isInitialized && message.UpdateTypes != null && message.UpdateTypes.Contains(this.GetType()))
                UpdateVM(message.HardUpdate);
        }

        #endregion

        #region Protected

        protected void DisposeDataLoadingToken()
        {
            if (DataLoadingCancellationTokenSource != null)
            {
                lock (DataLoadingCancellationTokenSource)
                {
                    DataLoadingCancellationTokenSource.Cancel();
                    DataLoadingCancellationTokenSource.Dispose();
                    DataLoadingCancellationTokenSource = null;
                }
            }
        }

        protected void DisposeMvxMessageToken<T>(MvxSubscriptionToken token)
            where T : MvxMessage
        {
            // TODO rework with base vm token controller
            if (token != null)
            {
                Messenger.Unsubscribe<T>(token);
                token = null;
            }
        }

        #endregion

        #region Public

        public virtual void Unbind()
        {
            DisposeMvxMessageToken<UpdateViewModelMessage>(_updateVMToken);
            DisposeDataLoadingToken();
        }

        public virtual void Reload()
        {
            RaisePropertyChanged(() => PageSubtitle);
            RaisePropertyChanged(() => PageTitle);
        }

        public async void CloseVM()
        {
            //await NavigationService.Close(this);
        }

        public virtual async Task UpdateVM(bool hardUpdate)
        {
        }

        public override void Prepare()
        {
            base.Prepare();

            _updateVMToken = Messenger.Subscribe<UpdateViewModelMessage>(UpdateVMExecute);
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

        #endregion
    }

    public abstract class BaseVM<TParameter> : BaseVM, IMvxViewModel<TParameter>
    {
        public BaseVM(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserInteraction userInteraction, IMvxMessenger messenger) : 
            base(logProvider, navigationService, userInteraction, messenger)
        {
        }

        public abstract void Prepare(TParameter parameter);
    }
}
