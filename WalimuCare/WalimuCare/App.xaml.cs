using Microsoft.AppCenter.Crashes;
using System;
using System.Diagnostics;
using WalimuCare.Interfaces;
using WalimuCare.Services;
using WalimuCare.ViewModels;
using WalimuCare.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WalimuCare
{
    public partial class App : Application
    {
        public App()
        {
            try
            {
                InitializeComponent();

                Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt+QHJqVk1hXk5Hd0BLVGpAblJ3T2ZQdVt5ZDU7a15RRnVfR11nSXxRfkFgWHdYeA==;Mgo+DSMBPh8sVXJ1S0R+X1pFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF5jTH5XdkdhUXtdcnRVTg==;ORg4AjUWIQA/Gnt2VFhiQlJPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSXtTckVhW3dac3ZUQmg=;MTkxNjI0MUAzMjMxMmUzMjJlMzNaQ3ExcVd5azJTSVE3dzZ4ZjRtN21NaWR2M3JqTVhlVlV6UWRIelo5V2JVPQ==;MTkxNjI0MkAzMjMxMmUzMjJlMzNNL1J6bFdvK3pNQUNBRG9QY3ZnMWZUeXF2VkpvQWVDenFPVWpZQTB3ZTdnPQ==;NRAiBiAaIQQuGjN/V0d+Xk9HfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5WdkFjW31WdHdXTmhU;MTkxNjI0NEAzMjMxMmUzMjJlMzNKbFpTdTNxL1pacUF4LzBOSDEvb0F3UXpIajBoTGhIMTdRUjl4cWNCdXFNPQ==;MTkxNjI0NUAzMjMxMmUzMjJlMzNlTFZ0U3JvbTRDRno5dmpSazNmTnNtZElYSitJdDZRZjJvUXRGYWsvcVBBPQ==;Mgo+DSMBMAY9C3t2VFhiQlJPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSXtTckVhW3dac3BTRGg=;MTkxNjI0N0AzMjMxMmUzMjJlMzNOSHhpTlZWeWMyaTRKd0F5MjYrQ3dlRzMwZk9ZcmdyWVhsUnlieW4zWlU4PQ==;MTkxNjI0OEAzMjMxMmUzMjJlMzNhdGFZQ1lZQytEZ3czQkFONnVXU29kR2VISmdxdjlITEdjR0d1ZzdhS2RRPQ==;MTkxNjI0OUAzMjMxMmUzMjJlMzNKbFpTdTNxL1pacUF4LzBOSDEvb0F3UXpIajBoTGhIMTdRUjl4cWNCdXFNPQ==");

                XF.Material.Forms.Material.Init(this);

                RegisterDependencyModels();

                SetApiDetails();

                //Preferences.Clear();

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

                    if (memberNumber == null || memberNumber == "")
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
                DependencyService.Register<SubmitComplaintsViewModel>();

                DependencyService.Register<DependantService>();

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
