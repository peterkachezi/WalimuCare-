using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuV2.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPageTwo : ContentPage
	{
		public LoginPageTwo()
		{
			InitializeComponent();
		}
		private void btnLogin_Clicked(object sender, EventArgs e)
		{

			var memberNumber = Preferences.Get("memberNumber", string.Empty);

			if(memberNumber ==null || memberNumber ==""  || string.IsNullOrEmpty(memberNumber))
			{
				Navigation.PushAsync(new TheRealLoginPage());

			}
			else
			{
				Navigation.PushAsync(new FinalLoginPage());

			}
		}
		private void btnSignUp_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new SignUpPage());
		}

	}
}