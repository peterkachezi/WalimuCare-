using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using WalimuV2.Views.Dependants;
using WalimuV2.Views.hospitals;
using WalimuV2.Views.Others;
using WalimuV2.Views.Policy;
using WalimuV2.Views.TrackHospitalVisit;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuV2.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

		private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
		{
			try
			{
				await Browser.OpenAsync("https://wa.me/254781704000", BrowserLaunchMode.SystemPreferred);
			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Contact Us Page");
			}
		}

		public void SendErrorMessageToAppCenter(Exception ex, string NameOfModule, string MemberNumber = "", string PhoneNumber = "")
		{
			var properties = new Dictionary<string, string>
									{
										{ "NameOfModule", NameOfModule },
										{ "MemberNumber", MemberNumber},
										{ "PhoneNumber", PhoneNumber}

									};
			Crashes.TrackError(ex, properties);
		}

		private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
		{
			try
			{
				await Shell.Current.GoToAsync(nameof(PolicyDetailsPage));
			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex,"Something went wrong");
					
			}

		}

		private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
		{
			try
			{
				await Browser.OpenAsync("https://app.blissmedicalcentre.com/PatientPortalWeb", BrowserLaunchMode.SystemPreferred);
			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Contact Us Page");
			}
		}

		private async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
		{
			try
			{			
				await Shell.Current.Navigation.PushAsync(new PolicyLimitsPage());

			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Something went wrong");

			}
		}

		private async void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
		{
			try
			{
				await Shell.Current.Navigation.PushAsync(new DependantPage());

			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Something went wrong");

			}
		}

		private async void TapGestureRecognizer_Tapped_5(object sender, EventArgs e)
		{
			try
			{
				await Shell.Current.Navigation.PushAsync(new HospitalVisitPage());

			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Something went wrong");

			}
		}

		private async void TapGestureRecognizer_Tapped_6(object sender, EventArgs e)
		{
			try
			{
				await Shell.Current.Navigation.PushAsync(new ContactUsPage());

			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Something went wrong");

			}
		}

		private async void TapGestureRecognizer_Tapped_7(object sender, EventArgs e)
		{
			try
			{
				await Shell.Current.Navigation.PushAsync(new Covid19Page());

			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Something went wrong");

			}
		}
	}
}