using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System;
using WeatherToday.Core.ViewModels.Location;
using Android.Gms.Common;
using Android.Gms.Location;
using Android.Util;
using System.Threading.Tasks;
using WeatherToday.Localization;
using Android.Support.V4.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;

namespace WeatherToday.Android.Activity
{
    [MvxActivityPresentation]
    [Activity(
        Label = "Location",
        AlwaysRetainTaskState = true,
        Theme = "@style/AppTheme",
        ScreenOrientation = ScreenOrientation.User)]
    public class LocationActivity : CommonActivity<LocationVM>, IOnMapReadyCallback
    {
        #region Fields

        private Button _locationButton;
        private TextView _cityTextView;
        private MapFragment _mapFragment;

        private FusedLocationProviderClient _fusedLocationProviderClient;

        #endregion

        #region Private

        private bool IsGooglePlayServicesInstalled()
        {
            var queryResult = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (queryResult == ConnectionResult.Success)
            {
                Log.Info("LocationActivity", "Google Play Services is installed on this device.");
                return true;
            }

            if (GoogleApiAvailability.Instance.IsUserResolvableError(queryResult))
            {
                // Check if there is a way the user can resolve the issue
                var errorString = GoogleApiAvailability.Instance.GetErrorString(queryResult);
                Log.Error("LocationActivity", "There is a problem with Google Play Services on this device: {0} - {1}",
                          queryResult, errorString);

                // Alternately, display the error to the user.
            }

            return false;
        }

        private async void GetCurrentLocation(object sender, EventArgs e)
        {
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) == Permission.Granted)
            {
                IsGooglePlayServicesInstalled();

                await GetLastLocationFromDevice();

                _mapFragment.GetMapAsync(this);
            }
            else
            {
                // The app does not have permission ACCESS_FINE_LOCATION 
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.AccessFineLocation }, 0);
            }
        }

        private async Task GetLastLocationFromDevice()
        {
            // This method assumes that the necessary run-time permission checks have succeeded.
            _locationButton.SetText(Strings.getting_last_location, TextView.BufferType.Normal);

            global::Android.Locations.Location location = await _fusedLocationProviderClient.GetLastLocationAsync();

            if (location == null)
            {
                // Seldom happens, but should code that handles this scenario
                _locationButton.SetText(Strings.get_current_location, TextView.BufferType.Normal);
            }
            else
            {
                ViewModel.Lat = location.Latitude;
                ViewModel.Lon = location.Longitude;
                ViewModel.UpdateCity(location.Latitude, location.Longitude);

                _locationButton.SetText(Strings.get_current_location, TextView.BufferType.Normal);
            }
        }

        private void GoToMap(object sender, EventArgs e)
        {
            string lat = ViewModel.Latitude?.Replace(",", ".") ?? string.Empty;
            string lon = ViewModel.Longtitude?.Replace(",", ".") ?? string.Empty;

            var geoUri = global::Android.Net.Uri.Parse("geo:" + lat + "," + lon);
            var mapIntent = new Intent(Intent.ActionView, geoUri);
            StartActivity(mapIntent);
        }

        private bool CheckPermissions()
        {
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) == Permission.Granted)
            {
                return true;
            }
            else
            {
                // The app does not have permission ACCESS_FINE_LOCATION 
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.AccessFineLocation }, 0);
            }

            return false;
        }

        #endregion

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            _fusedLocationProviderClient = LocationServices.GetFusedLocationProviderClient(this);

            _locationButton = FindViewById<Button>(Resource.Id.get_current_city);
            _locationButton.Click += GetCurrentLocation;

            _cityTextView = FindViewById<TextView>(Resource.Id.current_geo_city);
            _cityTextView.Click += GoToMap;

            _mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            _mapFragment.GetMapAsync(this);
        }

        protected override View CreateView()
        {
            return this.BindingInflate(Resource.Layout.activity_location, null);
        }

        #region Public

        public void OnMapReady(GoogleMap map)
        {
            // Do something with the map, i.e. add markers, move to a specific location, etc.
            if (CheckPermissions())
            {
                IsGooglePlayServicesInstalled();

                var lat = ViewModel.Lat;
                var lon = ViewModel.Lon;

                LatLng coord = new LatLng(lat, lon);
                map.MyLocationEnabled = true;
                map.MoveCamera(CameraUpdateFactory.NewLatLngZoom(coord, 13));

                MarkerOptions markerOptions = new MarkerOptions();
                markerOptions.SetPosition(new LatLng(lat, lon));
                markerOptions.SetTitle("Sidney");

                map.AddMarker(markerOptions);

                map.MapType = GoogleMap.MapTypeNormal;
            }
        }

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

        #endregion
    }
}