using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using MvvmCross;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.WeakSubscription;
using WeatherToday.Android.Presenter;
using WeatherToday.Core.ViewModels.Base;

namespace WeatherToday.Android.Activity
{
    public abstract class CommonActivity<TViewModel> : CommonActivity, IMvxAndroidView<TViewModel> where TViewModel : BaseVM
    {
        //#region Fields

        //private MvxNamedNotifyPropertyChangedEventSubscription<string> _titleToken;
        //private MvxNamedNotifyPropertyChangedEventSubscription<string> _subtitleToken;

        //#endregion

        #region Constructor

        protected CommonActivity(IntPtr ptr, JniHandleOwnership ownership) : base(ptr, ownership)
        {
        }

        public CommonActivity()
        {
        }

        #endregion

        #region Protected

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //if (_titleToken == null)
            //    _titleToken = ViewModel?.WeakSubscribe(() => ViewModel.PageTitle,
            //    (sender, args) =>
            //    {
            //        SetupTitle(ViewModel.PageTitle);
            //    });

            //if (_subtitleToken == null)
            //    _subtitleToken = ViewModel?.WeakSubscribe(() => ViewModel.PageSubtitle,
            //    (sender, args) =>
            //    {
            //        SetupSubtitle(ViewModel.PageSubtitle);
            //    });

            //SetupTitle(ViewModel?.PageTitle);
            //SetupSubtitle(ViewModel?.PageSubtitle);
        }

        protected override void OnResume()
        {
            base.OnResume();

            //SetupTitle(ViewModel?.PageTitle);
            //SetupSubtitle(ViewModel?.PageSubtitle);
        }

        #endregion

        #region Public

        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }


        #endregion
    }

    public abstract class CommonActivity : MvxAppCompatActivity
    {
        #region Fields

        public global::Android.Support.V7.Widget.Toolbar MainToolbar;
        protected View View;

        #endregion

        #region Properties

        //public TextView TitleView { get; private set; }
        //public TextView SubtitleView { get; private set; }

        private ICustomPresenter _customPresenter;
        public ICustomPresenter CustomPresenter
        {
            get => _customPresenter ?? (_customPresenter = Mvx.IoCProvider.Resolve<ICustomPresenter>());
        }

        #endregion

        #region Constructor

        protected CommonActivity(IntPtr ptr, JniHandleOwnership ownership) : base(ptr, ownership)
        {
        }

        public CommonActivity()
        {
        }

        #endregion

        #region Protected

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(View ?? (View = CreateView()));

            //MainToolbar = View.FindViewById<global::Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            //if (MainToolbar != null)
            //{
            //    //TitleView = MainToolbar.FindViewById<TextView>(Resource.Id.screen_title);
            //    //SubtitleView = MainToolbar.FindViewById<TextView>(Resource.Id.screen_subtitle);
            //    SetSupportActionBar(MainToolbar);
            //    SupportActionBar.SetHomeButtonEnabled(true);
            //    SupportActionBar?.SetDisplayShowTitleEnabled(false);
            //    SupportActionBar?.SetDisplayUseLogoEnabled(false);
            //    SupportActionBar?.SetDisplayShowCustomEnabled(false);
            //    SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //}
        }

        protected abstract View CreateView();

        #endregion

        #region Public

        //public void SetupTitle(string title)
        //{
        //    if (MainToolbar == null)
        //        return;

        //    TitleView.Text = title;
        //}

        //public void SetupSubtitle(string subtitle)
        //{
        //    if (MainToolbar == null)
        //        return;

        //    if (string.IsNullOrEmpty(subtitle))
        //    {
        //        SubtitleView.Visibility = ViewStates.Gone;
        //        SubtitleView.Text = string.Empty;
        //    }
        //    else
        //    {
        //        SubtitleView.Visibility = ViewStates.Visible;
        //        SubtitleView.Text = subtitle;
        //    }
        //}

        public void HideSoftKeyboard()
        {
            if (CurrentFocus == null)
                return;

            InputMethodManager inputMethodManager = (InputMethodManager)GetSystemService(InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);

            CurrentFocus.ClearFocus();
        }

        #endregion
    }
}