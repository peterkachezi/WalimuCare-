using WalimuV2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuV2.Views.hospitals
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EnableGpsPage : ContentPage
	{
		public EnableGpsPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			BindingContext = DependencyService.Get<EnableGpsViewModel>();
		}
	}
}