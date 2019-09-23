using Android;
using Android.Content.Res;
using Android.Content.PM;
using Android.Gms.Location;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using MvvmCross;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Refractored.Fab;
using System;
using System.Threading.Tasks;
using WeatherToday.Android.Helpers;
using WeatherToday.Core;
using WeatherToday.Core.Models.City;
using WeatherToday.Core.Services;
using WeatherToday.Core.ViewModels.Base;
using WeatherToday.Core.ViewModels.Weather;
using WeatherToday.Localization;
using Xamarin.Essentials;

namespace WeatherToday.Android.Views
{
    [MvxFragmentPresentation(typeof(MainVM), Resource.Id.content_frame, true)]
    [Register(nameof(WeatherListFragment))]
    public class WeatherListFragment : BaseFragment<WeatherListVM>, IScrollDirectorListener
    {
        #region Fields

        private MvxRecyclerView _rootRecyclerView;
        private Refractored.Fab.FloatingActionButton _fab;
        private FusedLocationProviderClient _fusedLocationProviderClient;

        private readonly string TAG = nameof(WeatherListFragment);

        #endregion

        #region Properties

        protected override int FragmentId => Resource.Layout.page_weather_list;

        protected MainActivity MyActivity
        {
            get => Activity as MainActivity;
        }

        #endregion

        #region Private

        private async Task GetLastLocationFromDevice()
        {
            // This method assumes that the necessary run-time permission checks have succeeded.
            global::Android.Locations.Location location = await _fusedLocationProviderClient.GetLastLocationAsync();

            if (location == null)
            {
                // Seldom happens, but should code that handles this scenario
            }
            else
            {
                var lat = location.Latitude;
                var lon = location.Longitude;
                var cityName = await Mvx.IoCProvider.Resolve<IWeatherService>().GetCityFromCoordinates(lat, lon);

                var city = new CityBO();
                city.CityName = cityName;
                await App.Database.SaveCityAsync(city);

                await ViewModel.Initialize();
            }
        }

        #endregion

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            ParentActivity.SupportActionBar.Title = Strings.weather_list_title;
            ParentActivity.SupportActionBar.SetDisplayHomeAsUpEnabled(false);

            _rootRecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.weather_list);
            if (_rootRecyclerView != null)
            {
                var layoutManager = new LinearLayoutManager(Activity);
                _rootRecyclerView.SetLayoutManager(layoutManager);
            }

            _fab = view.FindViewById<global::Refractored.Fab.FloatingActionButton>(Resource.Id.fab_add_city);
            _fab.AttachToRecyclerView(_rootRecyclerView, this);
            _fab.Enabled = true;

            _fusedLocationProviderClient = LocationServices.GetFusedLocationProviderClient(Context);

            // Starting an app with setting first city by location API
            if (Preferences.Get("first_launch", true))
            {
                RequestPermissions(new string[] { Manifest.Permission.AccessFineLocation }, Resources.GetInteger(Resource.Integer.request_fine_location));

                Preferences.Set("first_launch", false);
            }

            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            ParentActivity.SupportActionBar.Title = "WeatherToday";
        }

        public override async void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            if (requestCode == Resources.GetInteger(Resource.Integer.request_fine_location))
            {
                // Received permission result for camera permission.
                Log.Info(TAG, "Received response for Location permission request.");

                // Check if the only required permission has been granted
                if ((grantResults.Length == 1) && (grantResults[0] == Permission.Granted))
                {
                    // Location permission has been granted, okay to retrieve the location of the device.
                    Log.Info(TAG, "Location permission has now been granted.");

                    if (PlatformHelper.IsGooglePlayServicesInstalled(Context))
                        await GetLastLocationFromDevice();
                }
                else
                {
                    Log.Info(TAG, "Location permission was NOT granted.");
                }
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
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