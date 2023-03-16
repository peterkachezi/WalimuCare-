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
		private string greetings;
		public string Greetings
		{
			get { return greetings; }

			set { greetings = value; }
		}
		public void SetGreetings()
		{
			try
			{
				var timeOfDay = DateTime.Now.TimeOfDay;

				if (timeOfDay > new TimeSpan(5, 0, 0) && timeOfDay < new TimeSpan(12, 0, 0))
				{
					Greetings = "Good Morning, ";
				}
				else if (timeOfDay >= new TimeSpan(12, 0, 0) && timeOfDay < new TimeSpan(16, 0, 0))
				{
					Greetings = "Good Afternoon, ";
				}
				else if (timeOfDay >= new TimeSpan(16, 0, 0) && timeOfDay < new TimeSpan(19, 0, 0))
				{
					Greetings = "Good Evening, ";
				}
				else
				{
					Greetings = "Welcome back,  ";
				}
			}
			catch (Exception ex)
			{

				SendErrorMessageToAppCenter(ex, "Login", "");

				Greetings = "Welcome back, ";
			}
		}

        private async void TapGestureRecognizer_Tapped_8(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.Navigation.PushAsync(new FindHospitalPage());

            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Something went wrong");

            }
        }
    }
}