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
using MvvmCross.Platforms.Android.Presenters.Attributes;
using WeatherToday.Android.Activity;
using WeatherToday.Core.ViewModels.Forecast;

namespace WeatherToday.Android.Views.Forecast
{
    [MvxFragmentPresentation(typeof(ForecastVM), Resource.Id.content_frame, true)]
    [Register(nameof(ForecastDetailsFragment))]
    class ForecastDetailsFragment : BaseFragment<ForecastDetailsVM>
    {
        #region Properties

        protected override int FragmentId => Resource.Layout.page_forecast_details;

        protected ForecastActivity MyActivity
        {
            get => Activity as ForecastActivity;
        }

        #endregion

        #region Public

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            return view;
        }

        #endregion
    }
}