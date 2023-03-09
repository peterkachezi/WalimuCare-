using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalimuV2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuV2.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfirmOtpPage : ContentPage
	{
		public ConfirmOtpPage()
		{
			InitializeComponent();

			BindingContext = DependencyService.Get<ConfirmOtpViewModel>();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			//btnResendOtp.IsEnabled = false;
		}
	}
}