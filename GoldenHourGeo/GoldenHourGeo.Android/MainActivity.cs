using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Gms.Common;
using Android.Gms.Location;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using System.Threading.Tasks;
using Android;
using Android.Util;

namespace GoldenHourGeo.Droid
{
    [Activity(MainLauncher = true, Label = "MainActivity")]
    public class MainActivity : Activity
    {
        FusedLocationProviderClient fusedLocationProviderClient;
        View rootLayout;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            fusedLocationProviderClient = LocationServices.GetFusedLocationProviderClient(this);
            rootLayout = FindViewById(Resource.Layout.Main);
            checkPermission();

            if (IsGooglePlayServicesInstalled())
            {
                var lastLocation = await GetLastLocationFromDevice();
                double? longitude = lastLocation?.longitude;
                double? latitude = lastLocation?.latitude;

                WeatherService weather = new WeatherService();
                

                TextView weatherResponseText = FindViewById<TextView>(Resource.Id.textView1);
                weatherResponseText.Text = weather.GetWeather(latitude.ToString(), longitude.ToString());
            }
            else
            {
                // If there is no Google Play Services installed, then this sample won't run.
                Snackbar.Make(rootLayout, Resource.String.missing_googleplayservices_terminating, Snackbar.LengthIndefinite)
                        .SetAction(Resource.String.ok, delegate { FinishAndRemoveTask(); })
                        .Show();
            }


            // Create your application here
        }

        public async Task<LocationResult> GetLastLocationFromDevice()
        {
            var loc = new LocationResult();
            var location = await fusedLocationProviderClient.GetLastLocationAsync();
            loc.latitude = location.Latitude;
            loc.longitude = location.Longitude;

            return loc;
        }

        bool IsGooglePlayServicesInstalled()
        {
            var queryResult = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (queryResult == ConnectionResult.Success)
            {
                Log.Info("MainActivity", "Google Play Services is installed on this device.");
                return true;
            }

            if (GoogleApiAvailability.Instance.IsUserResolvableError(queryResult))
            {
                // Check if there is a way the user can resolve the issue
                var errorString = GoogleApiAvailability.Instance.GetErrorString(queryResult);
                Log.Error("MainActivity", "There is a problem with Google Play Services on this device: {0} - {1}",
                            queryResult, errorString);
            }

            return false;
        }

        public void checkPermission()
        {
            ActivityCompat.RequestPermissions(this,
                      new String[] { Manifest.Permission.AccessFineLocation, Manifest.Permission.AccessCoarseLocation },
                       123);
            ActivityCompat.RequestPermissions(this,
                       new String[] { Manifest.Permission.AccessFineLocation, Manifest.Permission.AccessFineLocation },
                        123);
        }
    }
}