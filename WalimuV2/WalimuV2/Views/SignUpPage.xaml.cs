using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WalimuV2.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuV2.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignUpPage : ContentPage
	{
		public int CountTimesSignInTried { get; set; }
		public SignUpPage()
		{
			InitializeComponent();

			BindingContext = DependencyService.Get<SignUpViewModel>();
		}

		private async void btnSignUp_Clicked(object sender, EventArgs e)
		{

			try
			{
				if (CountTimesSignInTried < 1)
				{
					await Navigation.PushPopupAsync(new WalimuLoaderPage("Signup in progress..."));

					await Task.Run(() =>
					{
						Thread.Sleep(5000);
					});

					MainThread.BeginInvokeOnMainThread(() =>
					{
						Navigation.PopPopupAsync(false);
					});

					await Navigation.PushPopupAsync(new WalimuErrorPage("Invalid Member Id or Phone Number"));

					CountTimesSignInTried++;
				}
				else
				{
					//await Navigation.PushAsync(new ConfirmOtpPage());
				}
			}
			catch (Exception ex)
			{

				await DisplayAlert("Oooops", ex.Message, "OK");
			}

		}

		private void btnLogin_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new FinalLoginPage());
		}
	}
}