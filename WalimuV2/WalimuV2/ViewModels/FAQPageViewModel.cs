using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WalimuV2.ApiResponses;
using WalimuV2.Models;
using WalimuV2.Services;
using WalimuV2.Views.Others;
using Xamarin.Forms;

namespace WalimuV2.ViewModels
{
	public class FAQPageViewModel : AppViewModel
	{

		private List<FAQ> myFAQs;
		public List<FAQ> MyFAQs
		{
			get { return myFAQs; }
			set { myFAQs = value; OnPropertyChanged(); }
		}
		private List<FaqBase> faqBases;
		public List<FaqBase> FaqBases
		{
			get { return faqBases; }
			set { faqBases = value; OnPropertyChanged(); }
		}
		public ICommand GetFaqsCommand { get; set; }

		public FAQPageViewModel()
		{
			GetFaqsCommand = new Command(async () => await GetFaqs());

			Task.Run(async () =>
			{
				await GetFaqs();
			});
		}


		public async Task GetFaqs()
		{
			try
			{
				if (await CheckInternetConnectivity())
				{
					if (!await CheckIfApiDetailsAreSetUp())
					{

					}
					else
					{
						IsRefreshing = true;

						await Task.Delay(2000);

						FaqBases = new List<FaqBase>();

						await ShowLoadingMessage();

						RestClient client = new RestClient(ApiDetail.EndPoint);

						RestRequest restRequest = new RestRequest()
						{
							Resource = "/Complaints/GetFAQs",

							Method = Method.Get
						};

						var response = await client.ExecuteAsync(restRequest);

						if (response.IsSuccessful)
						{
							var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<List<FaqBase>>>(response.Content);

							if (deserializedResponse.success)
							{
								FaqBases = deserializedResponse.data;

								await RemoveLoadingMessage();
							}
							else
							{
								await ShowErrorMessage("Sorry, no FAQs were found");
							}
						}
						else
						{
							await ShowErrorMessage();
						}
					}
				}

				IsRefreshing = false;
			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "FAQ");
			}
		}
	}
}
