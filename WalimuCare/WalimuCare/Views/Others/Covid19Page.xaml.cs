using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalimuCare.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuCare.Views.Others
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Covid19Page : ContentPage
	{
		public Covid19Page ()
		{
			InitializeComponent ();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();

			BindingContext = DependencyService.Get<Covid19ViewModel>();
		}

		private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
		{
			App.Current.MainPage = new NavigationPage(new AppShell());

		}
	}
}