using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System.Threading.Tasks;
using WeatherToday.Android.Activity;
using WeatherToday.Core.ViewModels.Base;

namespace WeatherToday.Android.Views
{
    [MvxActivityPresentation]
    [Activity(
        
        Label = "WeatherApp",
        Theme = "@style/AppTheme",
        LaunchMode = LaunchMode.SingleTask,
        Name = "weatherToday.android.views.MainActivity")]
    public class MainActivity : CommonActivity<MainVM>, global::Android.Support.V4.App.FragmentManager.IOnBackStackChangedListener
    {
        #region Private

        private async Task InitStartPage()
        {
            if (ViewModel.InitializeTask.IsNotCompleted)
            {
                ViewModel.InitializeTask.PropertyChanged += async (e, o) =>
                {
                    if (o.PropertyName == "IsSuccessfullyCompleted" && ViewModel.InitializeTask.IsSuccessfullyCompleted)
                    {
                        await ViewModel.InitializeMainVMsCommand.ExecuteAsync();
                    }
                    //else if (ViewModel.InitializeTask.IsFaulted)
                    //    Mvx.IoCProvider.Resolve<IUserInteraction>().Alert(Strings.message_reload_app, null, Strings.title_error_default);
                };
            }
            else if (ViewModel.InitializeTask.IsSuccessfullyCompleted)
                await ViewModel.InitializeMainVMsCommand.ExecuteAsync();
            //else if (ViewModel.InitializeTask.IsFaulted)
            //    Mvx.IoCProvider.Resolve<IUserInteraction>().Alert(Strings.message_reload_app, null, Strings.title_error_default);
        }

        #endregion

        #region Protected

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SupportFragmentManager.RemoveOnBackStackChangedListener(this);
            SupportFragmentManager.AddOnBackStackChangedListener(this);

            InitStartPage();
        }

        protected override View CreateView()
        {
            return this.BindingInflate(Resource.Layout.activity_weather, null);
        }

        #endregion

        #region Public

        //public override bool OnCreateOptionsMenu(IMenu menu)
        //{
        //    return base.OnCreateOptionsMenu(menu);
        //}

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case global::Android.Resource.Id.Home:
                    if (CustomPresenter.CanPop(SupportFragmentManager))
                        OnBackPressed();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public override void OnBackPressed()
        {
            CustomPresenter.MoveBack(SupportFragmentManager);
        }

        public virtual void OnBackStackChanged()
        {
            if (!CustomPresenter.CanPop(SupportFragmentManager))
            {
                SupportActionBar.SetDisplayHomeAsUpEnabled(false);
            }
            else
            {
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            }
        }

        #endregion
    }
}