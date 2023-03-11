using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalimuV2.Droid.Services;
using WalimuV2.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocationShare))]
namespace WalimuV2.Droid.Services
{
	public class LocationShare : ILocSettings
	{
		public bool isGpsAvailable()
		{
			bool value = false;

			Android.Locations.LocationManager manager = (Android.Locations.LocationManager)Android.App.Application.Context.GetSystemService(Context.LocationService);

			if (!manager.IsProviderEnabled(Android.Locations.LocationManager.GpsProvider))
			{
				//gps disable
				value = false;
			}
			else
			{
				//Gps enable
				value = true;
			}

			return value;
		}

		public void OpenSettings()
		{
			Intent intent = new Intent(Android.Provider.Settings.ActionLocat‌​ionSourceSettings);

			intent.AddFlags(ActivityFlags.NewTask);

			Android.App.Application.Context.StartActivity(intent);
		}
	}
}