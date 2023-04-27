using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Android.App;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

[assembly: ExportFont("Volte Medium.otf", Alias = "Volte")]

[assembly: ExportFont("Volte Bold.otf", Alias = "VolteBold")]

[assembly: ExportFont("Volte Semibold.otf", Alias = "VolteSemiBold")]

[assembly: ExportFont("DINOT-Regular.ttf", Alias = "DinotRegular")]

[assembly: ExportFont("DINOT-Bold.ttf", Alias = "DinotBold")]

[assembly: UsesPermission(Android.Manifest.Permission.WriteExternalStorage)]

[assembly: UsesPermission(Android.Manifest.Permission.ReadExternalStorage)]

[assembly: UsesFeature("android.hardware.location.gps", Required = false)]

[assembly: UsesFeature("android.hardware.camera", Required = true)]

[assembly: UsesFeature("android.hardware.camera.autofocus", Required = true)]