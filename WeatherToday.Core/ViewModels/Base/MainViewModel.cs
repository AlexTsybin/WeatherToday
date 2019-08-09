using MvvmCross;
using MvvmCross.Commands;
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
using WeatherToday.Core.ViewModels.Base.Commands;
using WeatherToday.Core.ViewModels.Base.Interfaces;
using WeatherToday.Core.ViewModels.Weather;
using WeatherToday.Localization;

namespace WeatherToday.Core.ViewModels.Base
{
    public class MainViewModel : BaseVM
    {
        #region Commands

        private IMvxAsyncCommand _initializeMainVMsCommand;
        public IMvxAsyncCommand InitializeMainVMsCommand
        {
            get => _initializeMainVMsCommand ?? (_initializeMainVMsCommand = new TorAsyncCommand(InitializeMainVMsExecute, null, true));
        }

        #endregion

        #region Constructor

        public MainViewModel()
        {
            InitializeMainVMsCommand.Execute();
        }

        #endregion

        #region Private

        private async Task InitializeMainVMsExecute()
        {
            SetupNavigationTasks(out List<Task> tasks);

            await Task.WhenAll(tasks);
        }

        // TODO not tested
        private void ClearMvxTokens()
        {
            foreach (var token in SubscriptionTokens)
            {
                DisposeMvxMessageToken<MvxMessage>(token);
            }
        }

        #endregion

        #region Protected

        protected void SetupNavigationTasks(out List<Task> tasks)
        {
            tasks = new List<Task>();

            tasks.Add(NavigationService.Navigate<WeatherListVM>());
        }

        protected virtual bool CheckResponse(ErrorModel model, bool notify = true, string notifyMessage = null)
        {
            if (model.IsError)
            {
                if (!string.IsNullOrEmpty(model.ErrorMessage) && notify)
                    UserInteractions.AlertAsync(string.IsNullOrEmpty(notifyMessage) ? model.ErrorMessage : notifyMessage, Strings.title_error_default);

                var analitycsService = Mvx.IoCProvider.Resolve<IAnalitycsService>();
                analitycsService.TrackCustomEvent($"Response Error. Model: {model.GetType().FullName}. Error message: {model.ErrorMessage}. Notify message: {notifyMessage}");

                return false;
            }
            return true;
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

        #endregion

        #region Public

        public virtual string GetUniqName()
        {
            return GetType().FullName + GetHashCode();
        }

        public override void Prepare()
        {
            base.Prepare();
        }

        public async override Task Initialize()
        {
            await base.Initialize();
        }

        #endregion
    }
}
