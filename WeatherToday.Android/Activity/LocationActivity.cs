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
using Android.Gms.Location;
using Android.Util;
using System.Threading.Tasks;
using WeatherToday.Localization;
using Android.Support.V4.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using WeatherToday.Android.Helpers;
using Android.Support.Design.Widget;

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

        private LinearLayout _rootLayout;
        private Button _locationButton;
        private TextView _cityTextView;
        private MapFragment _mapFragment;

        private FusedLocationProviderClient _fusedLocationProviderClient;

        private readonly string TAG = nameof(LocationActivity);

        #endregion

        #region Private

        public void ShouldShowRequestPermission(global::Android.App.Activity activity, string permission, View view)
        {
            if (ActivityCompat.ShouldShowRequestPermissionRationale(activity, permission))
            {
                // Provide an additional rationale to the user if the permission was not granted
                // and the user would benefit from additional context for the use of the permission.
                // For example if the user has previously denied the permission.
                Log.Info(TAG, "Displaying camera permission rationale to provide additional context.");

                var requiredPermissions = new string[] { permission };
                Snackbar.Make(view,
                               Strings.permission_location_rationale,
                               Snackbar.LengthIndefinite)
                        .SetAction(Strings.ok,
                                   new Action<View>(delegate (View obj) {
                                       ActivityCompat.RequestPermissions(activity, requiredPermissions, Resources.GetInteger(Resource.Integer.request_fine_location));
                                   }
                        )
                ).Show();
            }
            else
            {
                ActivityCompat.RequestPermissions(activity, new string[] { permission }, Resources.GetInteger(Resource.Integer.request_fine_location));
            }
        }

        private async void GetCurrentLocation(object sender, EventArgs e)
        {
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) == Permission.Granted)
            {
                if (PlatformHelper.IsGooglePlayServicesInstalled(this))
                {
                    await GetLastLocationFromDevice();

                    _mapFragment.GetMapAsync(this);
                }
            }
            else
            {
                // The app does not have permission ACCESS_FINE_LOCATION 
                ShouldShowRequestPermission(this, Manifest.Permission.AccessFineLocation, _rootLayout);
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
                ShouldShowRequestPermission(this, Manifest.Permission.AccessFineLocation, _rootLayout);
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

            _rootLayout = FindViewById<LinearLayout>(Resource.Id.parent_ll);

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
                PlatformHelper.IsGooglePlayServicesInstalled(this);

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