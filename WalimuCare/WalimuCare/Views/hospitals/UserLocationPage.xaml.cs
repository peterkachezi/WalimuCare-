using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalimuCare.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuCare.Views.hospitals
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserLocationPage : ContentPage
	{
		public UserLocationPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			BindingContext = DependencyService.Get<UserLocationViewModel>();
		}
	}
}