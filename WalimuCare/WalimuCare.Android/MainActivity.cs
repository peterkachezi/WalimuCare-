using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android;
using Xamarin.Forms.GoogleMaps.Android;

namespace WalimuCare.Droid
{
	[Activity(Label = "WalimuCare", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		const int RequestLocationId = 0;

		readonly string[] LocationPermissions =
		{
			Manifest.Permission.AccessCoarseLocation,

			Manifest.Permission.AccessCoarseLocation,
		};

		protected override void OnStart()
		{
			base.OnStart();

			if ((int)Build.VERSION.SdkInt >= 11)
			{
				if(CheckSelfPermission(Manifest.Permission.AccessFineLocation) != Permission.Granted)
				{
					RequestPermissions(LocationPermissions, RequestLocationId);
				}
				else
				{

				}
			}
		}
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			Rg.Plugins.Popup.Popup.Init(this);

			Xamarin.Essentials.Platform.Init(this, savedInstanceState);

			global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

			Xamarin.FormsMaps.Init(this, savedInstanceState);

			Xamarin.FormsGoogleMaps.Init(this, savedInstanceState);

			FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);

			var platformConfig = new PlatformConfig
			{
				BitmapDescriptorFactory = new BitmapConfig()
			};

			Xamarin.FormsGoogleMaps.Init(this, savedInstanceState, platformConfig);

			LoadApplication(new App());
		}
		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
		{
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}