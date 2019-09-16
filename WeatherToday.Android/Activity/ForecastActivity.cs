using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using WeatherToday.Android.Presenter;
using WeatherToday.Android.Views.Forecast;
using WeatherToday.Core.ViewModels.Forecast;

namespace WeatherToday.Android.Activity
{
    [MvxActivityPresentation]
    [Activity(
        Label = "Forecast",
        AlwaysRetainTaskState = true,
        Theme = "@style/AppTheme",
        LaunchMode = LaunchMode.SingleTop,
        ScreenOrientation = ScreenOrientation.User)]
    public class ForecastActivity : BaseHostActivity<ForecastVM, ForecastFragment>, IBackHandler
    {
        #region Protected

        protected override View CreateView()
        {
            return this.BindingInflate(Resource.Layout.activity_forecast, null);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
        }

        #endregion

        #region Public

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case global::Android.Resource.Id.Home:
                    OnBackPressed();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed()
        {
            CustomPresenter.MoveBack(SupportFragmentManager);
        }

        public async Task<bool> BackPressed()
        {
            return false;
        }

        #endregion
    }
}