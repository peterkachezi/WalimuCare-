using Newtonsoft.Json;
using RestSharp;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
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

namespace WalimuV2.ViewModels
{
	public class ConfirmOtpViewModelReset : AppViewModel
	{

		private string originalPhoneNumber;
		public string OriginalPhoneNumber
		{
			get { return originalPhoneNumber; }
			set
			{
				originalPhoneNumber = value;
				OnPropertyChanged(nameof(OriginalPhoneNumber));
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
		private string otp;
		public string Otp
		{
			get { return otp; }
			set { otp = value; OnPropertyChanged(nameof(Otp)); }
		}
		private bool enableVerifyOtpBtn { get; set; }
		public bool EnableVerifyOtpBtn
		{
			get { return enableVerifyOtpBtn; }
			set
			{
				enableVerifyOtpBtn = value;
				OnPropertyChanged(nameof(EnableVerifyOtpBtn));
			}
		}
		private bool enableResendOtpBtn { get; set; }
		public bool EnableResendOtpBtn
		{
			get { return enableResendOtpBtn; }
			set
			{
				enableResendOtpBtn = value;
				OnPropertyChanged(nameof(EnableResendOtpBtn));
			}
		}


		public ICommand SendOTPCommand { get; set; }
		public ICommand ReSendOTPCommand { get; set; }
		public ICommand VerifyOTPCommand { get; set; }
		public int NumberOfSecondsRemaining { get; set; }

		private string resendText;
		public string ResendText
		{
			get { return resendText; }
			set { resendText = value; OnPropertyChanged(nameof(ResendText)); }
		}
		public Timer Timer { get; set; }

		public ConfirmOtpViewModelReset()
		{


			ReSendOTPCommand = new Command(async () => await ResendOtp());

			VerifyOTPCommand = new Command(async () => await VerifyOtp());

			EnableVerifyOtpBtn = true;

			PhoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), "");

			OriginalPhoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), "");

			string standardizedPhoneNumber = DependencyService.Get<ResetPinPageViewModel>().StandardizePhoneNumber(PhoneNumber);

			PhoneNumber = FormatPhoneNumberWithX(standardizedPhoneNumber);

			NumberOfSecondsRemaining = 60;

			ResendText = "Resend OTP in (60) sec";

			var now = DateTime.Now.TimeOfDay;

			EnableResendOtpBtn = false;


			Task.Run(() =>
			{
				Timer = new Timer(UpdateResendSeconds, "state", 1000, 1000);
			});

		}

		public async Task ResendOtp()
		{
			try
			{
				IsBusy = true;
				EnableSubmitBtn = false;

				EnableResendOtpBtn = false;

				await ShowLoadingMessage("Please wait as we Re-send OTP");

				RestClient client = new RestClient(ApiDetail.EndPoint);

				RestRequest restRequest = new RestRequest()
				{
					Method = Method.Post,
					Resource = "/Members/ReSendOTP"
				};


				object payload = new
				{
					AddressMobileNumber = OriginalPhoneNumber

				};


				restRequest.AddJsonBody(payload);


				var response = await Task.Run(() =>
				{
					return client.Execute(restRequest);
				});

				var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<object>>(response.Content);

				if (deserializedResponse.success)
				{

					await ShowSuccessMessage("OTP has been resent successully");
					;
					Thread.Sleep(2000);

					await App.Current.MainPage.Navigation.PopPopupAsync();

					EnableResendOtpBtn = true;
				}
				else
				{
					await ShowErrorMessage("Sorry, something went wrong, please resend OTP");

				}

				IsBusy = false;
				EnableSubmitBtn = true;
			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Confirm Otp", "", OriginalPhoneNumber);

				await ShowErrorMessage();

				EnableResendOtpBtn = true;
			}
		}
		public async Task VerifyOtp()
		{
			try
			{
				IsBusy = true;
				EnableSubmitBtn = false;

				if (Otp == null || Otp == "")
				{

					await ShowErrorMessage("Please Enter OTP before verifying");
					return;
				}

				if (await CheckInternetConnectivity())
				{
					if (await CheckIfApiDetailsAreSetUp())
					{

						await ShowLoadingMessage();

						RestClient client = new RestClient(ApiDetail.EndPoint);

						RestRequest restRequest = new RestRequest()
						{
							Method = Method.Post,
							Resource = "/Members/verifyResetPinOTP"
						};


						object payload = new
						{
							OtpNumber = OriginalPhoneNumber,
							OtpCode = Otp
						};


						restRequest.AddJsonBody(payload);


						var response = await Task.Run(() =>
						{
							return client.Execute(restRequest);
						});


						var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<object>>(response.Content);

						if (deserializedResponse.success)
						{
							await ShowSuccessMessage("OTP Verified Successfully");

							Thread.Sleep(2000);

							await RemoveLoadingMessage();

							MainThread.BeginInvokeOnMainThread(() =>
							{
								App.Current.MainPage = new NavigationPage(new SetPinResetPage());
							});

						}
						else
						{
							await ShowErrorMessage("The OTP could not be verified");
						}


					}
				}

				IsBusy = false;
				EnableSubmitBtn = true;
			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Confirm Otp", "", OriginalPhoneNumber);
				await ShowErrorMessage();
			}
		}

		public void UpdateResendSeconds(object state)
		{
			try
			{
				NumberOfSecondsRemaining = NumberOfSecondsRemaining - 1;


				ResendText = $"Resend OTP in ({NumberOfSecondsRemaining}) sec";

				if (NumberOfSecondsRemaining == 0)
				{

					MainThread.BeginInvokeOnMainThread(() =>
					{
						EnableResendOtpBtn = true;
						Timer.Dispose();
					});

				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				//SendErrorMessageToAppCenter(ex, "");
			}
		}

		public static string FormatPhoneNumberWithX(string theString)
		{
			try
			{
				var aStringBuilder = new StringBuilder(theString);

				//aStringBuilder.Remove(6,2); first argument represents position, next argument represents number of characters
				aStringBuilder.Remove(4, 4);
				aStringBuilder.Insert(4, "XXXX");
				theString = aStringBuilder.ToString();

				return theString;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine("An error occured");
				return null;
			}
		}

	}
}