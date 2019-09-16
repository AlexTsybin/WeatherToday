using System;
using Android.OS;
using MvvmCross.Platforms.Android.Views;
using WeatherToday.Android.Views;
using WeatherToday.Core.ViewModels.Base;

namespace WeatherToday.Android.Activity
{
    public abstract class BaseHostActivity<TViewModel, TFragmentType> : CommonActivity<TViewModel>, IMvxAndroidView<TViewModel>
        where TViewModel : BaseVM
        where TFragmentType : BaseFragment
    {
        protected TFragmentType MainFragment;

        protected void InitializeMainFragment()
        {
            MainFragment = (TFragmentType)Activator.CreateInstance(typeof(TFragmentType));
            MainFragment.ViewModel = ViewModel;

            var ft = SupportFragmentManager.BeginTransaction();

            ft.SetCustomAnimations(Resource.Animation.abc_fade_in, Resource.Animation.abc_fade_out, Resource.Animation.abc_fade_in, Resource.Animation.abc_fade_out);

            ft.Add(Resource.Id.content_frame, MainFragment, ViewModel.GetUniqName());

            ft.CommitAllowingStateLoss();
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            if (bundle == null)
                InitializeMainFragment();
        }
    }
}