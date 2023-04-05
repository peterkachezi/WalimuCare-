using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WalimuV2.ApiResponses;
using WalimuV2.Models;
using WalimuV2.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WalimuV2.ViewModels
{
	public class PolicyDetailsViewModel : AppViewModel
	{

		public string PhoneNumber { get; set; }
		public string MemberNumber { get; set; }

		private ObservableCollection<PolicyWriteUpResponse> policyWriteUpResponses;
		public ObservableCollection<PolicyWriteUpResponse> PolicyWriteUpResponses
		{
			get { return policyWriteUpResponses; }
			set
			{
				policyWriteUpResponses = value;
				OnPropertyChanged(nameof(PolicyWriteUpResponses));
			}
		}


		private PolicyWriteUpResponse selectedPolicyWriteUpResponse;
		public PolicyWriteUpResponse SelectedPolicyWriteUpResponse
		{
			get { return selectedPolicyWriteUpResponse; }
			set { selectedPolicyWriteUpResponse = value; OnPropertyChanged(); OpenSelectedPolicyWriteUp(); }
		}



		public ICommand RefreshCommand { get; set; }

		public ICommand DownloadBrochureCommand { get; set; }

		public PolicyDetailsViewModel()
		{
			RefreshCommand = new Command(async () => await GetBenefitData());
			DownloadBrochureCommand = new Command(async () => await DownloadBrochure());

			PageTitle = "Policy Details";
			PageSubTitle = "Read how each benefit applies";

			PhoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), "");
			MemberNumber = Preferences.Get("MemberNumber", "");

			Task.Run(async () =>
			{
				await GetBenefitData();
			});



		}


		public async Task GetBenefitData()
		{
			try
			{

				if (await CheckInternetConnectivity())
				{
					if (await CheckIfApiDetailsAreSetUp())
					{
						await ShowLoadingMessage();

						IsRefreshing = true;
						IsEmptyIllustrationVisible = false;
						NoDataAvailableMessage = "";
						IsListViewVisible = true;


						RestClient client = new RestClient(ApiDetail.EndPoint);

						RestRequest restRequest = new RestRequest()
						{
							Method = Method.Get,
							Resource = "/Members/GetPolicyDetails"
						};



						var response = await client.ExecuteAsync(restRequest);

						//var response = await client.ExecuteAsync(restRequest);

						if (response.IsSuccessful)
						{
							var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<List<PolicyWriteUpResponse>>>(response.Content);


							if (deserializedResponse.success)
							{
								var data = deserializedResponse.data;

								PolicyWriteUpResponses = new ObservableCollection<PolicyWriteUpResponse>();

								foreach (var item in data)
								{
									char[] trimChars = new char[] { 'l', 'i' };

									PolicyWriteUpResponse policyWriteUp = new PolicyWriteUpResponse()
									{
										BenefitName = item.BenefitName,
										Title = item.Details.Where(p => p.StartsWith("p")).FirstOrDefault()?.TrimStart('p').TrimEnd('p').TrimStart('P').TrimEnd('P').Trim(),
										Details = item.Details.Where(p => p.StartsWith("li")).Select(x => { x = x.TrimStart('l'); x = x.TrimStart('i'); x = x.TrimStart(); return x.Trim(); }).ToList()
									};

									MainThread.BeginInvokeOnMainThread(() =>
									{
										PolicyWriteUpResponses.Add(policyWriteUp);
									});

								}

								await RemoveLoadingMessage();
							}
							else
							{

								IsEmptyIllustrationVisible = true;
								NoDataAvailableMessage = "Sorry Something went wrong";
								IsListViewVisible = false;
								await RemoveLoadingMessage();
								await ShowErrorMessage();
							}
						}
						else
						{
							IsEmptyIllustrationVisible = true;
							NoDataAvailableMessage = "Sorry Something went wrong";
							IsListViewVisible = false;
						}

						IsRefreshing = false;
					}

				}


			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Policy Details", MemberNumber, PhoneNumber);
				//await ShowErrorMessage();
				IsEmptyIllustrationVisible = true;
				NoDataAvailableMessage = "Sorry Something went wrong";
				IsListViewVisible = false;
			}
		}


		public async Task DownloadBrochure()
		{
			try
			{
				await Browser.OpenAsync("https://drive.google.com/u/0/uc?id=1RVwZgaTae5WuPA6UGI4FRxnSv0CEgOpU&export=download");

			}
			catch (Exception ex)
			{

				SendErrorMessageToAppCenter(ex, "Policy Details");
			}
		}


		public void OpenSelectedPolicyWriteUp()
		{
			try
			{

				var newPolicy = PolicyWriteUpResponses.Select(p => { p.IsVisible = false; return p; });

				foreach (var item in newPolicy)
				{
					if (item.BenefitName == SelectedPolicyWriteUpResponse.BenefitName)
					{
						item.IsVisible = true;
					}
				}

				PolicyWriteUpResponses = (ObservableCollection<PolicyWriteUpResponse>)newPolicy;

			}
			catch (Exception ex)
			{

				SendErrorMessageToAppCenter(ex, "Policy Write Up");
			}
		}

	}
}
