using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalimuV2.CustomRenderer;
using WalimuV2.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;
using Map = Xamarin.Essentials.Map;

namespace WalimuV2.Views.Others
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactUsPage : CustomContentPageRenderer
	{
		private string pageTitle;
		public string PageTitle
		{
			get { return pageTitle; }
			set
			{
				pageTitle = value; OnPropertyChanged();
			}
		}
		private string pageSubTitle;
		public string PageSubTitle
		{
			get { return pageSubTitle; }
			set
			{
				pageSubTitle = value; OnPropertyChanged();
			}
		}
		public string PhoneNumber { get; set; }
		public string MemberNumber { get; set; }
		public ContactUsPage()
		{
			InitializeComponent();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();

			BindingContext = this;

			PhoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), "");

			MemberNumber = Preferences.Get("MemberNumber", "");

			PageTitle = "Contact Us";
			PageSubTitle = "Talk to us on any of the platforms below";

			Position position = new Position(-1.285970541839565, 36.81322219158326);

			Pin pin = new Pin()
			{
				Type = PinType.Place,
				Label = "Healthier Kenya",
				Address = "",
				Position = position,
				Rotation = 33.3f,
				Tag = "The Arch Place",

			};

			map.Pins.Add(pin);

			map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(1)), true);

		}
		private void PhoneNumber_Tapped(object sender, EventArgs e)
		{
			try
			{
				PhoneDialer.Open("+254730604000");
			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Contact Us", MemberNumber, PhoneNumber);
				Console.WriteLine(ex);
			}
		}
		private void OtherPhoneNumber_Tapped(object sender, EventArgs e)
		{
			try
			{
				PhoneDialer.Open("1528");
			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Contact Us", MemberNumber, PhoneNumber);
				Console.WriteLine(ex);
			}
		}
		private async void Email_Tapped(object sender, EventArgs e)
		{
			try
			{
				await Email.ComposeAsync("", "", "mmc.customerservice@minet.co.ke");
			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Contact Us", MemberNumber, PhoneNumber);
				Console.WriteLine(ex);
			}
		}

		private async void Location_Tapped(object sender, EventArgs e)
		{
			try
			{

				Location location = new Location()
				{
					Latitude = -1.285970541839565,

					Longitude = 36.81322219158326,

					Accuracy = 10
				};

				await Map.OpenAsync(location);
			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Contact Us", MemberNumber, PhoneNumber);

				Console.WriteLine(ex);
			}
		}
		private async void Website_Tapped(object sender, EventArgs e)
		{
			try
			{
				await Browser.OpenAsync("https://www.minet.com/kenya/", BrowserLaunchMode.SystemPreferred);
			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Contact Us", MemberNumber, PhoneNumber);
				Console.WriteLine(ex);
			}
		}
		private async void Facebook_Tapped(object sender, EventArgs e)
		{
			try
			{
				await Browser.OpenAsync("https://www.facebook.com/MinetKenya", BrowserLaunchMode.SystemPreferred);
			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Contact Us", MemberNumber, PhoneNumber);

				Console.WriteLine(ex);
			}
		}

		private async void Twitter_Tapped(object sender, EventArgs e)
		{
			try
			{
				await Browser.OpenAsync("https://twitter.com/Minet_Kenya", BrowserLaunchMode.SystemPreferred);
			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Contact Us", MemberNumber, PhoneNumber);
				Console.WriteLine(ex);
			}
		}


		private async void Instagram_Tapped(object sender, EventArgs e)
		{
			try
			{
				await Browser.OpenAsync("https://www.instagram.com/minet_group/", BrowserLaunchMode.SystemPreferred);
			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Contact Us", MemberNumber, PhoneNumber);
				Console.WriteLine(ex);
			}
		}
		private async void Telegram_Tapped(object sender, EventArgs e)
		{
			try
			{
				await Browser.OpenAsync("https://t.me/TeachersMedicalScheme", BrowserLaunchMode.SystemPreferred);
			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Contact Us", MemberNumber, PhoneNumber);
				Console.WriteLine(ex);
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

		private async void whatsappNumber_Tapped(object sender, EventArgs e)
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
	}
}