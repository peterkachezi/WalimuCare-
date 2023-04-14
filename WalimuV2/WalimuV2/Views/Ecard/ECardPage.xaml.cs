using WalimuV2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuV2.Views.Ecard
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ECardPage : ContentPage
	{
		public ECardPage()
		{
			InitializeComponent();

			//BindingContext = DependencyService.Get<ECardViewModel>();

		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

		   BindingContext = DependencyService.Get<ECardViewModel>();
		}

     
    }
}