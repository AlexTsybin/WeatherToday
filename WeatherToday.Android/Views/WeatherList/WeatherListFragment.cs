﻿using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Refractored.Fab;
using WeatherToday.Core.ViewModels.Base;
using WeatherToday.Core.ViewModels.Weather;
using WeatherToday.Localization;

namespace WeatherToday.Android.Views
{
    [MvxFragmentPresentation(typeof(MainVM), Resource.Id.content_frame, true)]
    [Register(nameof(WeatherListFragment))]
    public class WeatherListFragment : BaseFragment<WeatherListVM>, IScrollDirectorListener
    {
        #region Fields

        private FloatingActionButton _fab;

        #endregion

        protected override int FragmentId => Resource.Layout.page_weather_list;

        protected MainActivity MyActivity
        {
            get => Activity as MainActivity;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            ParentActivity.SupportActionBar.Title = Strings.weather_list_title;
            ParentActivity.SupportActionBar.SetDisplayHomeAsUpEnabled(false);

            var recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.weather_list);
            if (recyclerView != null)
            {
                var layoutManager = new LinearLayoutManager(Activity);
                recyclerView.SetLayoutManager(layoutManager);
            }

            _fab = view.FindViewById<FloatingActionButton>(Resource.Id.fab_add_city);
            _fab.AttachToRecyclerView(recyclerView, this);
            _fab.Enabled = true;

            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            ParentActivity.SupportActionBar.Title = "WeatherToday";
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.menu_weather_list, menu);

            base.OnCreateOptionsMenu(menu, inflater);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_city_list:
                    ViewModel.CityListCommand.Execute();
                    return true;
                case Resource.Id.action_location:
                    ViewModel.LocationCommand.Execute();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public void OnScrollDown()
        {
        }

        public void OnScrollUp()
        {
        }
    }
} 