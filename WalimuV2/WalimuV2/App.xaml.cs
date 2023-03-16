using Microsoft.AppCenter.Crashes;
using System;
using System.Diagnostics;
using WalimuV2.Interfaces;
using WalimuV2.Models;
using WalimuV2.Services;
using WalimuV2.ViewModels;
using WalimuV2.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuV2
{
	public partial class App : Application
	{
		public App()
		{
			try
			{
				InitializeComponent();

				Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTI0Nzg0MkAzMjMwMmUzNDJlMzBHZS9Cdnl2ZHRKY3dZTmtIQnlJNDEzamJiK21nakxBbXRrUnFZd1p1ZHFjPQ==\r\n");

				XF.Material.Forms.Material.Init(this);

				RegisterDependencyModels();

				SetApiDetails();

			    Preferences.Clear();

				var firstName = Preferences.Get("firstName", string.Empty);

				var lastName = Preferences.Get("lastName", string.Empty);

				var phoneNumber = Preferences.Get("phoneNumber", string.Empty);

				var memberNumber = Preferences.Get("memberNumber", string.Empty);

				var password = Preferences.Get("password", string.Empty);	

				if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(memberNumber))
				{
					MainPage = new WelcomeScreenPage();

					return;
				}

				if (firstName != null || lastName != null || phoneNumber != null)
				{

					if(memberNumber ==null || memberNumber == "")
					{
						MainPage = new NavigationPage(new TheRealLoginPage());

						return;
					}
					MainPage = new NavigationPage(new FinalLoginPage());

					return;
				}				

				else
				{
					MainPage = new NavigationPage(new TheRealLoginPage());

					return;
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				MainPage = new TheRealLoginPage();

				Crashes.TrackError(ex);

				Console.WriteLine(ex);
			}

		}

		public void SetApiDetails()
		{
			if (Debugger.IsAttached)
			{
				ApiDetail.EndPoint = ApiDetail.LocalEndPoint;
			}
			else
			{
				//means we are on production
				ApiDetail.EndPoint = ApiDetail.PublicEndPoint;
				//ApiDetail.EndPoint = ApiDetail.LocalEndPoint;
			}
		}
		public void RegisterDependencyModels()
		{
			try
			{
				//view models 
				DependencyService.Register<MockDataStore>();

				DependencyService.Register<AppShellViewModel>();

				DependencyService.Register<PolicyLimitViewModel>();

				DependencyService.Register<SignUpViewModel>();

				DependencyService.Register<ConfirmOtpViewModel>();

				DependencyService.Register<ConfirmMemberDetailsViewModel>();

				DependencyService.Register<TheRealLoginViewModel>();

				DependencyService.Register<DependantsViewModel>();

				DependencyService.Register<HospitalVisitViewModel>();

				DependencyService.Register<PolicyDetailsViewModel>();	

				DependencyService.Register<RequestCallBackViewModel>();

				DependencyService.Register<FindHospitalViewModel>();

				DependencyService.Register<UserProfileViewModel>();

				DependencyService.Register<RequestCallBackViewModel>();

				DependencyService.Register<EnableGpsViewModel>();

				DependencyService.Register<FAQPageViewModel>();

				DependencyService.Register<ConfirmOtpViewModelReset>();

				DependencyService.Register<ResetPinPageViewModel>();

				DependencyService.Register<Covid19ViewModel>();

				DependencyService.Register<UserLocationViewModel>();

				DependencyService.Register<ECardViewModel>();

				DependencyService.Register<DownloadsViewModel>();

				DependencyService.Register<IDownload>();

				DependencyService.Register<ILocSettings>();

				DependencyService.Register<IFileSavePdf>();
			}
			catch (Exception ex)
			{
				Console.Write(ex);
			}
		}
		protected override void OnStart()
		{
		}
		protected override void OnSleep()
		{
		}
		protected override void OnResume()
		{
		}
	}
}
