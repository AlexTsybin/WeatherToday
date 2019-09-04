using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using WeatherToday.Android.Activity;
using WeatherToday.Core.ViewModels.Forecast;

namespace WeatherToday.Android.Views.Forecast
{
    [MvxFragmentPresentation(null, Resource.Id.content_frame, true)]
    [Register(nameof(ForecastFragment))]
    public class ForecastFragment : BaseFragment<ForecastVM>
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
                var layoutManager = new LinearLayoutManager(Activity);
                recyclerView.SetLayoutManager(layoutManager);
            }

            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
        }

        #endregion
    }
}