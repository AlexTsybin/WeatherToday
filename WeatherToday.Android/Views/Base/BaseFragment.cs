using Android.Content.Res;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Views;
using WeatherToday.Android.Activity;
using WeatherToday.Core.ViewModels.Base;

namespace WeatherToday.Android.Views
{
    public abstract class BaseFragment : MvxFragment
    {
        #region Fields

        private Toolbar _toolbar;

        #endregion

        #region Properties

        protected abstract int FragmentId { get; }

        public MvxAppCompatActivity ParentActivity
        {
            get => (MvxAppCompatActivity)Activity;
        }

        protected CommonActivity MainActivity
        {
            get => Activity as CommonActivity;
        }

        #endregion

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(FragmentId, null);

            _toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);

            if (_toolbar != null)
            {
                ParentActivity.SetSupportActionBar(_toolbar);
                ParentActivity.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            }

            HasOptionsMenu = true;

            return view;
        }

        /// <summary>
        /// Данный метод используется для корректировки биндинга с VM в случае, когда фрагмент был закеширован
        /// </summary>
        public void ReloadFragment()
        {
            //UnbindFragment();
            //SetupBaseBinding();
            //BindNewVM();
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
        }
    }

    public abstract class BaseFragment<TViewModel> : BaseFragment, IMvxFragmentView<TViewModel>
        where TViewModel : BaseVM
    {
        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}