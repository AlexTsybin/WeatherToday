using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace WeatherToday.Android.Domain
{
    class DialogOnDismissListener : Java.Lang.Object, IDialogInterfaceOnDismissListener
    {
        private readonly Action _onDismiss;

        public DialogOnDismissListener(Action onDismiss)
        {
            _onDismiss = onDismiss;
        }

        public void OnDismiss(IDialogInterface dialog)
        {
            _onDismiss?.Invoke();
        }
    }
}