using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using WeatherToday.Android.Activity;
using WeatherToday.Core.ViewModels.Forecast;

namespace WeatherToday.Android.Views.Forecast
{
    [MvxFragmentPresentation(typeof(ForecastVM), Resource.Id.content_frame, true,
        ViewModelType = typeof(ForecastVM))]
    [Register(nameof(ForecastFragment))]
    public class ForecastFragment : BaseFragment
    {
        #region Properties

        protected override int FragmentId => Resource.Layout.page_forecast;

        protected ForecastActivity MyActivity
        {
            get => Activity as ForecastActivity;
        }

        #endregion

        #region Public

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.forecast_list);
            if (recyclerView != null)
            {
                var layoutManager = new LinearLayoutManager(MyActivity);
                recyclerView.SetLayoutManager(layoutManager);
            }

            var hourlyRecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.hourly_list);
            if (hourlyRecyclerView != null)
            {
                var layoutManager = new LinearLayoutManager(MyActivity, LinearLayoutManager.Horizontal, false);
                hourlyRecyclerView.SetLayoutManager(layoutManager);
            }

            return view;
        }

        #endregion
    }
}