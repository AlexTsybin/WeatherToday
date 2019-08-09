using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Android.Support.V4.App;
using MvvmCross;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Views;
using WeatherToday.Android.Activity;
using WeatherToday.Android.Views;
using WeatherToday.Core.Services.Platform;
using WeatherToday.Localization;

namespace WeatherToday.Android.Presenter
{
    public class WeatherAndroidPresenter : MvxAppCompatViewPresenter, ICustomPresenter
    {
        public WeatherAndroidPresenter(IEnumerable<Assembly> androidViewAssemblies)
            : base(androidViewAssemblies)
        {
        }

        public bool CanPop(FragmentManager fragmentManager = null)
        {
            if (fragmentManager == null)
                return false;

            if (CurrentActivity is MainActivity)
                return fragmentManager.BackStackEntryCount > 1;
            else
                return fragmentManager.BackStackEntryCount > 0;
        }

        public async void MoveBack(FragmentManager fragmentManager)
        {
            if (CanPop(fragmentManager))
            {
                Fragment lastFragment = fragmentManager.Fragments.LastOrDefault();

                if (lastFragment is IMvxView mvxFragment)
                    await Close(mvxFragment.ViewModel);
            }
            else if (CurrentActivity is IBackHandler backHandledActivity)
            {
                if (await backHandledActivity.BackPressed())
                    return;
                else if (CurrentActivity is CommonActivity commonActivity)
                    await Close(commonActivity.ViewModel);
                else
                    CurrentActivity?.Finish();
            }
            else if (CurrentActivity is MainActivity)
                Mvx.IoCProvider.Resolve<IUserInteraction>().ShowCustomNotification(Strings.message_close_app, 3000, Strings.yes, () =>
                {
                    Mvx.IoCProvider.Resolve<IUserInteraction>().CloseApp();
                });
            else if (CurrentActivity != null && CurrentActivity is CommonActivity commonActivity)
                await Close(commonActivity.ViewModel);
            else
                CurrentActivity.Finish();
        }

        public MvxFragment GetTopFragment()
        {
            return CurrentFragmentManager?.Fragments?.LastOrDefault() as MvxFragment;
        }
    }
}