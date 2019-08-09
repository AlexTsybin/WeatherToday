using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using MvvmCross;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android;
using WeatherToday.Android.Domain;
using WeatherToday.Android.Presenter;
using WeatherToday.Core.Services.Platform;

namespace WeatherToday.Android.Services
{
    public class UserInteraction : IUserInteraction
    {
        #region Specific fields

        Snackbar _mainStackbar;
        MvxFragment _lastSnackbarFragment;

        #endregion

        protected global::Android.App.Activity CurrentActivity
        {
            get => Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
        }

        private ICustomPresenter _androidPresenter
        {
            get { return Mvx.IoCProvider.Resolve<ICustomPresenter>(); }
        }

        public Task AlertAsync(string message, string title = "", string okButton = "OK")
        {
            var tcs = new TaskCompletionSource<object>();
            Alert(message, () => tcs.SetResult(null), title, okButton);
            return tcs.Task;
        }

        public void Alert(string message, Action done = null, string title = "", string okButton = "OK")
        {
            Application.SynchronizationContext.Post(ignored =>
            {
                if (CurrentActivity == null) return;
                try
                {
                    var dialog = new AlertDialog.Builder(CurrentActivity, Resource.Style.AppCompatAlertDialogStyle)
                        .SetMessage(message)
                        .SetTitle(title)
                        .SetPositiveButton(okButton, delegate {
                            //if (done != null)
                            //    done();
                        })
                        .SetOnDismissListener(new DialogOnDismissListener(done))
                        .Create();
                    dialog.Show();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }, null);
        }

        public void ShowCustomNotification(string message, int duration = 3000, string actionName = null, Action action = null)
        {
            Application.SynchronizationContext.Post(ignored =>
            {
                if (CurrentActivity == null) return;
                try
                {
                    MvxFragment currentFragment = _androidPresenter.GetTopFragment();

                    if (_lastSnackbarFragment == null || _lastSnackbarFragment.GetHashCode() != currentFragment.GetHashCode())
                        _mainStackbar = Snackbar.Make(currentFragment.View.FindViewById<CoordinatorLayout>(Resource.Id.parent_layout), message, Snackbar.LengthLong);

                    if (_mainStackbar != null && !_mainStackbar.IsShownOrQueued)
                    {
                        _mainStackbar.SetDuration(duration);
                        _mainStackbar.SetText(message);

                        _mainStackbar.View.SetBackgroundColor(new Color(ContextCompat.GetColor(CurrentActivity, Resource.Color.colorPrimary)));

                        if (!string.IsNullOrEmpty(actionName) && action != null)
                            _mainStackbar.SetAction(actionName, v => { action(); });
                        else
                            _mainStackbar.SetAction(string.Empty, (Action<View>)null);

                        _mainStackbar.Show();
                    }

                    _lastSnackbarFragment = currentFragment;
                }
                catch
                {

                }
            }, null);
        }

        public void CloseApp()
        {
            CurrentActivity?.FinishAffinity();
            Process.KillProcess(Process.MyPid());
        }
    }
}