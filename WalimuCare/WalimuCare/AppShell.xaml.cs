﻿using Microsoft.AppCenter.Crashes;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using WalimuCare.ViewModels;
using WalimuCare.Views;
using WalimuCare.Views.Dependants;
using WalimuCare.Views.Ecard;
using WalimuCare.Views.Others;
using WalimuCare.Views.Policy;
using WalimuCare.Views.TrackHospitalVisit;
using Xamarin.Essentials;
using Xamarin.Forms;
namespace WalimuCare
{
	public partial class AppShell : Shell
	{
		public string PhoneNumber { get; set; }
		public string MemberNumber { get; set; }
		public AppShell()
		{
			InitializeComponent();

			RegisterMyRoutes();

			this.BindingContext = new AppShellViewModel();
		}
		public void RegisterMyRoutes()
		{
			try
			{
				//Routing.RegisterRoute(nameof(TheRealLoginPage), typeof(TheRealLoginPage));
				
				//Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));

				Routing.RegisterRoute(nameof(ViewECardPage), typeof(ViewECardPage));

				Routing.RegisterRoute(nameof(ECardPage), typeof(ECardPage));

				Routing.RegisterRoute(nameof(RequestCallBack), typeof(RequestCallBack));

				Routing.RegisterRoute(nameof(PolicyDetailsPage), typeof(PolicyDetailsPage));

				Routing.RegisterRoute(nameof(DependantPage), typeof(DependantPage));

				Routing.RegisterRoute(nameof(HospitalVisitPage), typeof(HospitalVisitPage));

				Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));

				Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));

				Routing.RegisterRoute(nameof(CallBacksPage), typeof(CallBacksPage));

				Routing.RegisterRoute(nameof(ContactUsPage), typeof(ContactUsPage));

				Routing.RegisterRoute(nameof(FAQPage), typeof(FAQPage));

				Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));

				Routing.RegisterRoute(nameof(DependantDetailPage), typeof(DependantDetailPage));

				Routing.RegisterRoute(nameof(SubmitComplaintsPage), typeof(SubmitComplaintsPage));

                Routing.RegisterRoute(nameof(ComplaintsPage), typeof(ComplaintsPage));
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				throw;
			}
		}

		protected override bool OnBackButtonPressed()
		{		
			var theCurrentPage = (Shell.Current?.CurrentItem?.CurrentItem as IShellSectionController)?.PresentedPage;

			string nameOfCurrentPage = theCurrentPage.ToString();

			if (nameOfCurrentPage == "WalimuCare.Views.AboutPage")
			{
				MainThread.BeginInvokeOnMainThread(async () =>
				{
					await App.Current.MainPage.Navigation.PushPopupAsync(new ExitPage());
				});
			}
			else
			{
				App.Current.MainPage = new AppShell();
			}
			return true;
		}

		private void OnMenuItemClicked(object sender, EventArgs e)
		{
			try
			{
				var loginModel = DependencyService.Get<TheRealLoginViewModel>();

				var x = loginModel.Pin = "";

				var y = loginModel.LoginCommand.CanExecute(false);

			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Shell Pages", MemberNumber, PhoneNumber);

				Console.WriteLine(ex);
			}

			Application.Current.MainPage = new NavigationPage();

			Application.Current.MainPage = new NavigationPage(new LoginPageTwo());
		}

		public void SendErrorMessageToAppCenter(Exception ex, string NameOfModule, string MemberNumber = "", string PhoneNumber = "")
		{
			try
			{
				var properties = new Dictionary<string, string>
									{
										{ "NameOfModule", NameOfModule },

										{ "MemberNumber", MemberNumber},

										{ "PhoneNumber", PhoneNumber}
									};

				Crashes.TrackError(ex, properties);
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
