using Rg.Plugins.Popup.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;
using WalimuCare.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuCare.Views
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
            var memberNumber = Preferences.Get("memberNumber", string.Empty);

            if (memberNumber == null || memberNumber == "" || string.IsNullOrEmpty(memberNumber))
            {
                Navigation.PushAsync(new TheRealLoginPage());
            }
            else
            {
                Navigation.PushAsync(new FinalLoginPage());

            }

		}
	}
}