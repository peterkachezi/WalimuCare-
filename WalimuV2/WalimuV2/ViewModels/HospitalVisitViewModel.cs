using Newtonsoft.Json;
using RestSharp;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WalimuV2.ApiResponses;
using WalimuV2.Models;
using WalimuV2.Services;
using WalimuV2.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WalimuV2.ViewModels
{

	public class HospitalVisitViewModel : AppViewModel
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

		private ObservableCollection<ListOfVisit> claims;
		public ObservableCollection<ListOfVisit> Claims
		{
			get { return claims; }
			set { claims = value; OnPropertyChanged(nameof(Claims)); }
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

		public HospitalVisitViewModel()
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

                        await ShowLoadingMessage("Please wait as we sign you in");

                        var client = new HttpClient();

						client.DefaultRequestHeaders.Accept.Clear();

						client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

						HttpResponseMessage getData = await client.GetAsync(ApiDetail.ApiUrl + "api/HospitalVisits/MemberNo?MemberNo=" + memberNo + "");

						if (getData.IsSuccessStatusCode)
						{
							string results = getData.Content.ReadAsStringAsync().Result;

							var getDependants = JsonConvert.DeserializeObject<root>(results);

							var claimsData = getDependants.listOfVisits.OrderByDescending(x => x.mvcDate).Take(6).ToList();

							var claimsObservableCollections = new ObservableCollection<ListOfVisit>();

							foreach (var item in claimsData)
							{
								claimsObservableCollections.Add(item);
							}
							Claims = claimsObservableCollections;

							IsRefreshing = false;

							IsActive = false;

                            await RemoveLoadingMessage();

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
