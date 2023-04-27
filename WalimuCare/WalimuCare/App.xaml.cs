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

                Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt+QHFqVkNrWU5HaV1CX2BZfVl1R2lYfU4BCV5EYF5SRHJfRl1nSH9Wc01rWnk=;Mgo+DSMBPh8sVXJ1S0d+X1RPc0BBQmFJfFBmRmlafVRxdkU3HVdTRHRcQlljTX5Xd0RmXHZbd3I=;ORg4AjUWIQA/Gnt2VFhhQlJBfVpdXnxLflF1VWBTfFx6dVdWESFaRnZdQV1nSXpTckRiXHlecH1T;MTYwNjI2OEAzMjMxMmUzMTJlMzMzNUQ5RlNLNU5QbHBYL1Z3Zlh0MGhXS2xUM2dmL0lDOVF4NEFad3R5SzUyU0E9;MTYwNjI2OUAzMjMxMmUzMTJlMzMzNUdnTFRQYjZMU3pVUUR5a25WWWpGcjVHdCtvT3dRckI3UTFhS25XREVnMmM9;NRAiBiAaIQQuGjN/V0d+XU9Hc1RHQmBWfFN0RnNadV1yflREcC0sT3RfQF5jTX5XdkFiWHpYcnBcQA==;MTYwNjI3MUAzMjMxMmUzMTJlMzMzNUZ6eG9McmpZOUI2OUpQLzZsS1JkRVUva0grS2Nud09OUTBwTm0yRDRhTDA9;MTYwNjI3MkAzMjMxMmUzMTJlMzMzNWdudys1WjdnUTNiUG5EN1FQbDR0MEJCS2FYQWk1M0U1bU9sZzFWUUZBUFE9;Mgo+DSMBMAY9C3t2VFhhQlJBfVpdXnxLflF1VWBTfFx6dVdWESFaRnZdQV1nSXpTckRiXHlddXFT;MTYwNjI3NEAzMjMxMmUzMTJlMzMzNWxaeU9URjk4Y0RPTW9QT0lUSWxCSHQxWjNLeFJoTm85ZWNtQ0poRFZRaG89;MTYwNjI3NUAzMjMxMmUzMTJlMzMzNUNKYzlNb2hjSTRqbDZ3aURITS9hbU1HclpkTmRuSklROEtkL09VZGRoVjA9;MTYwNjI3NkAzMjMxMmUzMTJlMzMzNUZ6eG9McmpZOUI2OUpQLzZsS1JkRVUva0grS2Nud09OUTBwTm0yRDRhTDA9");

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
