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

namespace WalimuV2.ViewModels
{
	public class PolicyLimitViewModel : AppViewModel
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

		private ObservableCollection<PolicyLimit> policyLimits;
		public ObservableCollection<PolicyLimit> PolicyLimits
		{
			get { return policyLimits; }
			set { policyLimits = value; OnPropertyChanged(nameof(PolicyLimits)); }
		}

		public ICommand RefreshCommand { get; set; }

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

		public PolicyLimitViewModel()
		{

			try
			{
				//PageTitle = "Hospital Visits";
				//PageSubTitle = "View costs, diagnosis concerning your hospital visit";
				//MemberNo = Preferences.Get("MemberNumber", "");
				//PhoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), "");
				//var SchemeIdInt = Preferences.Get("SchemeId", 45);
				//SchemeId = SchemeIdInt.ToString();

				RefreshCommand = new Command(async () => await GetHospitalVisit());


				Task.Run(async () =>
				{
					await GetHospitalVisit();
				});
			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Find Hospital", MemberNo, PhoneNumber);

				Console.WriteLine(ex);
			}
		}


		public async Task GetHospitalVisit()
		{
			try
			{
				var memberNo = Preferences.Get("memberNumber", string.Empty);

				if (string.IsNullOrEmpty(memberNo))
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

						HttpResponseMessage getData = await client.GetAsync(ApiDetail.ApiUrl + "api/PolicyLimit/GetPolicyLimit?MemberNo=" + memberNo + "&PolicyNo=2");

						if (getData.IsSuccessStatusCode)
						{
							string results = getData.Content.ReadAsStringAsync().Result;

							var getDependants = JsonConvert.DeserializeObject<List<PolicyLimit>>(results);

							var claimsObservableCollections = new ObservableCollection<PolicyLimit>();

							foreach (var item in getDependants)
							{
								claimsObservableCollections.Add(item);
							}
							PolicyLimits = claimsObservableCollections;

							IsRefreshing = false;

							IsActive = false;

						}
						if (getData.IsSuccessStatusCode == false)
						{
							IsEmptyIllustrationVisible = true;

							NoDataAvailableMessage = "You do not have any Hospital Visits";

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
	}
}
