using MvvmCross;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeatherToday.API;
using WeatherToday.API.Services.Platform;
using WeatherToday.Core.Messages.Common;
using WeatherToday.Core.Services.Platform;
using WeatherToday.Core.ViewModels.Base.Interfaces;

namespace WeatherToday.Core.ViewModels.Base
{
    public class WeatherViewModel : MvxNavigationViewModel, IBaseVM
    {
        #region Fields

        protected CancellationTokenSource DataLoadingCancellationTokenSource = new CancellationTokenSource();

        protected readonly List<MvxSubscriptionToken> SubscriptionTokens;
        private MvxSubscriptionToken _updateVMToken;

        #endregion

        #region Services

        protected readonly IUserInteraction UserInteraction;
        protected readonly IMvxMessenger Messenger;

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

        #region Constructor

        protected WeatherViewModel(IMvxNavigationService navigationService,
            IUserInteraction userInteraction, IMvxMessenger messenger) : base(Mvx.IoCProvider.Resolve<IMvxLogProvider>(), navigationService)
        {
            UserInteraction = userInteraction;
            Messenger = messenger;

            SubscriptionTokens = new List<MvxSubscriptionToken>();
        }

        protected WeatherViewModel() : this(Mvx.IoCProvider.Resolve<IMvxNavigationService>(),
            Mvx.IoCProvider.Resolve<IUserInteraction>(), Mvx.IoCProvider.Resolve<IMvxMessenger>())
        {
        }

        #endregion

        #region Private

        // TODO not tested
        private void ClearMvxTokens()
        {
            foreach (var token in SubscriptionTokens)
            {
                DisposeMvxMessageToken<MvxMessage>(token);
            }
        }

        private void UpdateVMExecute(UpdateViewModelMessage message)
        {
            bool isInitialized = Mvx.IoCProvider.Resolve<IAppService>().IsServiceUp;

            if (isInitialized && message.UpdateTypes != null && message.UpdateTypes.Contains(this.GetType()))
                UpdateVM(message.HardUpdate);
        }

        #endregion

        #region Protected

        protected virtual bool CheckResponse(ErrorModel model, bool notify = true, string notifyMessage = null)
        {
            if (model.IsError)
            {
                if (!string.IsNullOrEmpty(model.ErrorMessage) && notify)
                    UserInteraction.AlertAsync(string.IsNullOrEmpty(notifyMessage) ? model.ErrorMessage : notifyMessage, "Ooops!");

                var analitycsService = Mvx.IoCProvider.Resolve<IAnalitycsService>();
                analitycsService.TrackCustomEvent($"Response Error. Model: {model.GetType().FullName}. Error message: {model.ErrorMessage}. Notify message: {notifyMessage}");

                return false;
            }
            return true;
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

        protected void SubscribeToMvxMessage<T>(Action<T> action)
            where T : MvxMessage
        {
            SubscriptionTokens.Add(Messenger.Subscribe(action));
        }

        protected void CancelDataLoading()
        {
            DisposeDataLoadingToken();

            DataLoadingCancellationTokenSource = new CancellationTokenSource();
        }

        protected bool IsTokenActive()
        {
            return DataLoadingCancellationTokenSource != null && !DataLoadingCancellationTokenSource.IsCancellationRequested;
        }

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

        public virtual string GetUniqName()
        {
            return GetType().FullName + GetHashCode();
        }

        public virtual async Task UpdateVM(bool hardUpdate)
        {
        }

        public async void CloseVM()
        {
            await NavigationService.Close(this);
        }

        public override void Prepare()
        {
            base.Prepare();

            _updateVMToken = Messenger.Subscribe<UpdateViewModelMessage>(UpdateVMExecute);
        }

        #endregion
    }
}
