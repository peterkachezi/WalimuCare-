using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WalimuV2.Models;
using WalimuV2.Services;
using WalimuV2.Views;
using Xamarin.Forms;
using Xamarin.Essentials;
using WalimuV2.Views.Dependants;

namespace WalimuV2.ViewModels
{
	public class DependantsViewModel : AppViewModel
	{
		private string schemeId;
		public string SchemeId
		{
			get { return schemeId; }
			set { schemeId = value; OnPropertyChanged(nameof(SchemeId)); }
		}

		private string memberNo;
		public string MemberNo
		{
			get { return memberNo; }
			set { memberNo = value; OnPropertyChanged(nameof(MemberNo)); }
		}
		public string PhoneNumber { get; set; }

		private ObservableCollection<Dependant> dependant;
		public ObservableCollection<Dependant> Dependant
		{
			get { return dependant; }
			set { dependant = value; OnPropertyChanged(nameof(Dependant)); }
		}

		public ICommand RefreshCommand { get; set; }
		public ICommand ViewDependantDetailsCommand { get; set; }

		private bool isActive;
		public bool IsActive
		{
			get { return isActive; }
			set { isActive = value; OnPropertyChanged(nameof(IsActive)); }
		}

		private bool isListViewVisible;

		public bool IsListViewVisible
		{
			get { return isListViewVisible; }

			set
			{
				isListViewVisible = value;

				OnPropertyChanged();
			}
		}

		public DependantsViewModel()
		{
			try
			{
				RefreshCommand = new Command(async () => await GetDependant());

                ViewDependantDetailsCommand = new Command(async () => await ShowDependantDetails());

                PageTitle = "View Dependant Details";

                PageSubTitle = "Upload Dependant Passport Photo";

                Task.Run(async () =>
				{
					await GetDependant();
				});
			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Find Hospital", MemberNo, PhoneNumber);

				Console.WriteLine(ex);
			}
		}

		public async Task GetDependant()
		{
			try
			{
				var memberNo = Preferences.Get("memberNumber", string.Empty);

				if (memberNo == "")
				{
					await Application.Current.MainPage.Navigation.PushPopupAsync(new WalimuErrorPage("Something is not right, please log out then Login again"));

					return;
				}

				if (await CheckInternetConnectivity())
				{
					if (await CheckIfApiDetailsAreSetUp())
					{
						IsRefreshing = true;

						IsActive = true;

						IsEmptyIllustrationVisible = false;

						IsListViewVisible = true;

						var client = new HttpClient();

						client.DefaultRequestHeaders.Accept.Clear();

						client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

						HttpResponseMessage getData = await client.GetAsync(ApiDetail.ApiUrl + "api/Dependants/MemberNo?MemberNo=" + memberNo + "");

						if (getData.IsSuccessStatusCode)
						{
							string results = getData.Content.ReadAsStringAsync().Result;

							var getDependants = JsonConvert.DeserializeObject<List<Dependant>>(results);

							var claimsObservableCollections = new ObservableCollection<Dependant>();

							foreach (var item in getDependants)
							{
								claimsObservableCollections.Add(item);
							}

							Dependant = claimsObservableCollections;

							IsRefreshing = false;

							IsActive = false;
						}

						if (getData.IsSuccessStatusCode == false)
						{
							IsEmptyIllustrationVisible = true;

							NoDataAvailableMessage = "You do not have any dependant";

							IsListViewVisible = false;
						}
						
					}
				}
				else
				{
					await ShowErrorMessage();

					IsRefreshing = false;

					IsActive = false;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);

				IsRefreshing = false;

				IsActive = false;

				IsEmptyIllustrationVisible = true;

				NoDataAvailableMessage = "Sorry Something went wrong";

				SendErrorMessageToAppCenter(ex, "Track Hospital Visits", MemberNo, PhoneNumber);
			}
		}

        public async Task ShowDependantDetails()
        {
            try
            {
                Shell.Current.FlyoutIsPresented = false;
                //App.Current.MainPage = new NavigationPage(new ProfilePage());
                await Shell.Current.GoToAsync(nameof(DependantDetailPage));

            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "App View Model", "", "");
            }
        }

    }
}
