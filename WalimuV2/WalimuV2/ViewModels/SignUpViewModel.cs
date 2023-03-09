using Newtonsoft.Json;
using RestSharp;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WalimuV2.ApiResponses;
using WalimuV2.Models;
using WalimuV2.Services;
using WalimuV2.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Android.Media.Session.MediaSession;

namespace WalimuV2.ViewModels
{
	public class SignUpViewModel : AppViewModel
	{
		private string memberNumber;
		public string MemberNumber
		{
			get { return memberNumber; }
			set
			{
				memberNumber = value;

				OnPropertyChanged(nameof(MemberNumber));
			}
		}

		private string phoneNumber;
		public string PhoneNumber
		{
			get { return phoneNumber; }
			set
			{
				phoneNumber = value;
				OnPropertyChanged(nameof(PhoneNumber));
			}
		}

		private string password;
		public string Password
		{
			get { return password; }
			set
			{
				password = value;
				OnPropertyChanged(nameof(Password));
			}
		}

		public bool IsPopUpInStack = false;

		private VerifyMember verifyMember;
		public VerifyMember VerifyMember
		{
			get { return verifyMember; }
			set { verifyMember = value; }
		}
		public SignUpViewModel()
		{
			SignUpCommand = new Command(async () => await SignUp());
			EnableSubmitBtn = true;
		}
		public ICommand SignUpCommand { get; set; }
		public async Task SignUp1()
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

						var model = new Register
						{
							PhoneNumber = phoneNumber,

							MemberNumber = MemberNumber,

							Password = Password,
						};

						var json = JsonConvert.SerializeObject(model);

						HttpContent httpContent = new StringContent(json);

						httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

						var client = new HttpClient();

						var response = await client.PostAsync(ApiDetail.ApiUrl + "api/MemberAuth", httpContent);

						try
						{
							if (response.IsSuccessStatusCode)
							{

								await ShowSuccessMessage("Call Back request Submitted successfuly");

								EnableSubmitBtn = true;

								if (await SendOtp())
								{


									//MainThread.BeginInvokeOnMainThread(() =>
									//{
									//    App.Current.MainPage = new NavigationPage(new ConfirmMemberDetails());
									//});
									MainThread.BeginInvokeOnMainThread(() =>
									{
										App.Current.MainPage = new NavigationPage(new ConfirmOtpPage());
									});
								}


							}

							if (response.IsSuccessStatusCode == false)
							{

								await ShowErrorMessage("Sorry, Member Number and Phone Number do not match");


							}


						}
						catch (Exception e)
						{
							await ShowErrorMessage();

							SendErrorMessageToAppCenter(e, "Request CallBack ", Password, PhoneNumber);
						}

					}
				}
			}
			catch (Exception ex)
			{
				await ShowErrorMessage();

				SendErrorMessageToAppCenter(ex, "Request Call back", Password, PhoneNumber);
			}

		}
		public async Task SignUp()
		{
			try
			{

				IsPopUpInStack = true;

				if (await CheckInternetConnectivity())
				{
					if (await CheckIfApiDetailsAreSetUp())
					{
						if (!string.IsNullOrEmpty(PhoneNumber) && !string.IsNullOrEmpty(MemberNumber))
						{
							IsBusy = true;

							EnableSubmitBtn = false;

							await ShowLoadingMessage("Please wait as we verify your details");

							Preferences.Clear();

							var model = new Register
							{
								PhoneNumber = phoneNumber,

								MemberNumber = MemberNumber,

								Password = Password,
							};

							var json = JsonConvert.SerializeObject(model);

							HttpContent httpContent = new StringContent(json);

							httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

							var client = new HttpClient();

							var response = await client.PostAsync(ApiDetail.ApiUrl + "api/MemberAuth", httpContent);

							try
							{
								if (response.IsSuccessStatusCode)
								{
									var JsonResult = await response.Content.ReadAsStringAsync(); // reponse from api

									var result = JsonConvert.DeserializeObject<IsLoggedIn>(JsonResult); // deserialize json to c #

									var memberNo = result.member_number;

									var status = result.status;

									if (status == "1")
									{
										await ShowErrorMessage("Sorry an account with the same details has been found,please login");
												
									}

									if (status == "0")
									{
										await ShowErrorMessage("Sorry your data is not available in the system, kindly call 1508 or 0730604000 for assistance.");

									}

									if (status == "3")
									{
										Preferences.Set("memberNo", memberNo);

										EnableSubmitBtn = true;

										if (await SendOtp())
										{
											MainThread.BeginInvokeOnMainThread(() =>
											{
												Application.Current.MainPage = new NavigationPage(new ConfirmOtpPage());
											});
										}
									}

								}
								else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
								{
									await ShowErrorMessage("Sorry something went wrong please try again later");
								}
								else
								{
									EnableSubmitBtn = true;

									await ShowErrorMessage("Sorry something went wrong please try again later");
								}


							}
							catch (Exception ex)
							{
								EnableSubmitBtn = true;

								await ShowErrorMessage();

								SendErrorMessageToAppCenter(ex, "Sign Up", MemberNumber, PhoneNumber);

							}
						}
						else
						{
							await ShowErrorMessage("Please enter Email / Pin");

						}
					}

				}


			}
			catch (Exception ex)
			{
				await ShowErrorMessage();

				SendErrorMessageToAppCenter(ex, "Sign Up", MemberNumber, PhoneNumber);
			}
			finally
			{
				EnableSubmitBtn = true;
			}
		}
		public async Task<bool> SendOtp()
		{
			try
			{
				IsBusy = true;

				EnableSubmitBtn = false;

				await ShowLoadingMessage("Member details verified successfully, we are sending you a One Time PIN. ");

				RestClient client = new RestClient(ApiDetail.EndPoint);

				RestRequest restRequest = new RestRequest()
				{
					Method = Method.Post,

					Resource = "/Members/SendOTP"
				};


				object payload = new
				{
					AddressMobileNumber = PhoneNumber,
				};



				restRequest.AddJsonBody(payload);


				var response = await Task.Run(() =>
				{
					return client.Execute(restRequest);

				});


				var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<object>>(response.Content);

				if (deserializedResponse.success)
				{
					await ShowSuccessMessage("We have sent an One Time Pin to your phone number");

					Thread.Sleep(3000);

					await App.Current.MainPage.Navigation.PopPopupAsync();

					return true;

				}
				else
				{

					await ShowErrorMessage("Sorry, something went wrong when sending One Time Pin, please try again later");

					return false;
				}



			}
			catch (Exception ex)
			{

				await ShowErrorMessage();
				SendErrorMessageToAppCenter(ex, "Sign Up", MemberNumber, PhoneNumber);
				return false;
			}
			finally
			{
				EnableSubmitBtn = true;
			}
		}

	}
}
