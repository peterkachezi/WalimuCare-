using Xamarin.Forms.GoogleMaps.Android.Factories;
using AndroidBitmapDescriptor = Android.Gms.Maps.Model.BitmapDescriptor;
using AndroidBitmapDescriptorFactory = Android.Gms.Maps.Model.BitmapDescriptorFactory;
namespace WalimuCare.Droid
{
	public sealed class BitmapConfig : IBitmapDescriptorFactory
	{


		public AndroidBitmapDescriptor ToNative(Xamarin.Forms.GoogleMaps.BitmapDescriptor descriptor)
		{
			int iconId = 0;


			iconId = Resource.Drawable.hospitalmapicon;
			//switch (descriptor.Id)
			//{
			//    case "hospitalmapicon":
			//        iconId = Resource.Drawable.hospitalmapicon;
			//        break;
			//    case "hospitalmapicon":
			//        iconId = Resource.Drawable.hospitalmapicon;
			//        break;
			//}
			return AndroidBitmapDescriptorFactory.FromResource(iconId);
		}
	}
}