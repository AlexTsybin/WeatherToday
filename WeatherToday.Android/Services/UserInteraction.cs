using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross;
using MvvmCross.Platforms.Android;
using WeatherToday.Android.Domain;
using WeatherToday.Core.Services.Platform;

namespace WeatherToday.Android.Services
{
    public class UserInteraction : IUserInteraction
    {
        protected Activity CurrentActivity
        {
            get => Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
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

        public void CloseApp()
        {
            CurrentActivity?.FinishAffinity();
            Process.KillProcess(Process.MyPid());
        }
    }
}