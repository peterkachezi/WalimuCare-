using Microsoft.AppCenter.Crashes;
using System;
using System.Diagnostics;
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

                //var firstName = Preferences.Get(nameof(AspNetUsers.firstName), null);
				var firstName = Preferences.Get("firstName", string.Empty);

				var phoneNumber = Preferences.Get("phoneNumber", string.Empty);
				//MainPage = new WelcomeScreenPage();

                //var phoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), null);

                if (firstName == null || phoneNumber == null)
                {
                    MainPage = new WelcomeScreenPage();
                }
                else
                {
                    MainPage = new NavigationPage(new FinalLoginPage());
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

            DependencyService.Register<ConfirmOtpViewModelReset>();

            DependencyService.Register<ResetPinPageViewModel>();

            DependencyService.Register<TheRealLoginViewModel>();

            DependencyService.Register<RequestCallBackViewModel>();

            DependencyService.Register<PolicyLimitViewModel>();

            DependencyService.Register<UserProfileViewModel>();

            DependencyService.Register<ConfirmOtpViewModel>();

            DependencyService.Register<SignUpViewModel>();

            DependencyService.Register<ConfirmMemberDetailsViewModel>();

            DependencyService.Register<FAQPageViewModel>();

            DependencyService.Register<Covid19ViewModel>();

            DependencyService.Register<HospitalVisitViewModel>();

            DependencyService.Register<DependantsViewModel>();

            DependencyService.Register<PolicyDetailsViewModel>();

            DependencyService.Register<FindHospitalViewModel>();

            DependencyService.Register<EnableGpsViewModel>();

            DependencyService.Register<UserLocationViewModel>();

            DependencyService.Register<ECardViewModel>();

            DependencyService.Register<DownloadsViewModel>();

            //DependencyService.Register<PolicyLimitsViewModel>();

            DependencyService.Register<MockDataStore>();
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
