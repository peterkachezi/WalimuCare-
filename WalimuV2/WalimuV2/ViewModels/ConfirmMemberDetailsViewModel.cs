using Newtonsoft.Json;
using RestSharp;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
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
	public class ConfirmMemberDetailsViewModel : AppViewModel
	{
		private VerifyMember verifyMember;
		public VerifyMember VerifyMember
		{
			get { return verifyMember; }
			set { verifyMember = value; }
		}

		private string fullName;

		public string FullName
		{
			get { return fullName; }
			set { fullName = value; OnPropertyChanged(nameof(FullName)); }
		}

		private string memberNumber;

		public string MemberNumber
		{
			get { return memberNumber; }
			set { memberNumber = value; OnPropertyChanged(nameof(MemberNumber)); }
		}


		private string gender;

		public string Gender
		{
			get { return gender; }
			set { gender = value; }
		}

		private string phoneNumber;

		public string PhoneNumber
		{
			get { return phoneNumber; }
			set { phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
		}

		private string email;

		public string Email
		{
			get { return email; }
			set { email = value; OnPropertyChanged(nameof(Email)); }
		}


		private bool isTermsAndConditionsAccepted;

		public bool IsTermsAndConditionsAccepted
		{
			get { return isTermsAndConditionsAccepted; }
			set
			{
				isTermsAndConditionsAccepted = value;
				OnPropertyChanged(nameof(IsTermsAndConditionsAccepted));


				IsConfirmDetailsActivated = value;
			}
		}


		private bool isConfirmDetailsActivated;

		public bool IsConfirmDetailsActivated
		{
			get { return isConfirmDetailsActivated; }
			set
			{
				isConfirmDetailsActivated = value;
				OnPropertyChanged(nameof(IsConfirmDetailsActivated));
			}
		}

		private bool isCancelBtnActivated;

		public bool IsCancelBtnActivated
		{
			get { return isCancelBtnActivated; }
			set { isCancelBtnActivated = value; OnPropertyChanged(nameof(IsCancelBtnActivated)); }
		}

		private string memberId;

		public string MemberId
		{
			get { return memberId; }
			set { memberId = value; OnPropertyChanged(nameof(MemberId)); }
		}

		private string jobGroup;

		public string JobGroup
		{
			get { return jobGroup; }
			set
			{
				jobGroup = value;
				OnPropertyChanged(nameof(JobGroup));
			}
		}

		private string dateOfBirth;

		public string DateOfBirth
		{
			get { return dateOfBirth; }
			set
			{
				dateOfBirth = value;
				OnPropertyChanged(nameof(DateOfBirth));
			}
		}

		public ICommand ConfirmDetails { get; set; }
		public ICommand InCorrectDetails { get; set; }

		public ICommand CallNowCommand { get; set; }

		public ICommand CancelIncorrectDetailsCommand { get; set; }

		public ICommand ShowTermsAndConditionsCommand { get; set; }
		public ICommand ShowPrivacyPoliciesCommand { get; set; }

		public ConfirmMemberDetailsViewModel()
		{
			try
			{
				ConfirmDetails = new Command(async () => await SaveMemberDetailsToHK());
				InCorrectDetails = new Command(async () => await RedirectToSignUpPage());
				CallNowCommand = new Command(() => CallNow());
				CancelIncorrectDetailsCommand = new Command(async () => await CancelIncorrectDetails());
				ShowTermsAndConditionsCommand = new Command(async () => await ShowTermsAndConditions());
				ShowPrivacyPoliciesCommand = new Command(async () => await ShowPrivacyPolicy());


				var data = DependencyService.Get<SignUpViewModel>();

				//FullName = data.VerifyMember.data.bls_member.first_name + " " + data.VerifyMember.data.bls_member.last_name;
				//MemberNumber = data.VerifyMember.data.bls_member.member_id;
				//Gender = data.VerifyMember.data.bls_member.gender == 1 ? "Male" : "Female";
				//PhoneNumber = data.VerifyMember.data.bls_member.mobile_phone_number;
				//Email = data.VerifyMember.data.bls_member.email ?? "";
				//JobGroup = data.VerifyMember.data.bls_member.job_group;

				//var dob = data.VerifyMember.data.bls_member.date_of_birth;

				//DateTime dateTime = Convert.ToDateTime(dob);

				//DateOfBirth = dateTime.ToString("dd-MMM-yyyy");

			}
			catch (Exception ex)
			{

				SendErrorMessageToAppCenter(ex, "Confirm Member Details", "", PhoneNumber);
			}
		}

		public async Task SaveMemberDetailsToHK()
		{
			try
			{

				if (await CheckInternetConnectivity())
				{
					if (await CheckIfApiDetailsAreSetUp())
					{
						await ShowLoadingMessage("Please wait as we verify your details");

						RestClient client = new RestClient(ApiDetail.EndPoint);

						RestRequest restRequest = new RestRequest()
						{
							Method = Method.Post,
							Resource = "/Members/AddMemberDetails"
						};


						var data = DependencyService.Get<SignUpViewModel>();

						var payload = data.VerifyMember.data;


						restRequest.AddJsonBody(payload);


						var response = await Task.Run(() =>
						{
							return client.Execute(restRequest);
						});


						if (response.IsSuccessful)
						{
							await App.Current.MainPage.Navigation.PopPopupAsync(true);



							var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<ConfirmMemberDetailsResponse>>(response.Content);

							if (deserializedResponse.success)
							{

								MemberId = deserializedResponse.data.memberId;

								try
								{
									await App.Current.MainPage.Navigation.PopAllPopupAsync(true);
								}
								catch (Exception ex)
								{
									Console.WriteLine(ex.Message);

								}

								App.Current.MainPage = new NavigationPage(new SetPinPage());
							}
							else
							{

								await ShowErrorMessage("Sorry Something went wrong when confirming your details");

							}
						}
						else
						{
							await ShowErrorMessage();
						}
					}
				}






			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Confirm Member Details", "", PhoneNumber);
				await ShowErrorMessage("Sorry Something went wrong when confirming your details");
				Console.WriteLine(ex);

			}
		}

		public async Task RedirectToSignUpPage()
		{
			try
			{
				//await ShowErrorMessage("Please contact our Customer Care.");

				//Thread.Sleep(3000);

				//Application.Current.MainPage = new NavigationPage(new SignUpPage());

				await App.Current.MainPage.Navigation.PushPopupAsync(new CallForAssistancePage());
			}
			catch (Exception ex)
			{
				await ShowErrorMessage();
				SendErrorMessageToAppCenter(ex, "Confirm Member Details", "", PhoneNumber);
				Console.WriteLine(ex);
			}
		}

		public void CallNow()
		{
			try
			{
				PhoneDialer.Open("+254719091000");
			}
			catch (Exception ex)
			{

				SendErrorMessageToAppCenter(ex, "Confirm Details", MemberNumber, PhoneNumber);
			}
		}

		public async Task CancelIncorrectDetails()
		{
			try
			{
				await RemoveLoadingMessage();
			}
			catch (Exception ex)
			{

				SendErrorMessageToAppCenter(ex, "Confirm Details", MemberNumber, PhoneNumber);
			}
		}


		public async Task ShowTermsAndConditions()
		{
			try
			{
				await Browser.OpenAsync("https://healthierkenya.co.ke/Assesment/Terms");
			}
			catch (Exception ex)
			{

				SendErrorMessageToAppCenter(ex, "Confirm Member Details");
			}
		}

		public async Task ShowPrivacyPolicy()
		{
			try
			{
				await Browser.OpenAsync("https://healthierkenya.co.ke/Assesment/Privacy");
			}
			catch (Exception ex)
			{

				SendErrorMessageToAppCenter(ex, "Confirm Member Details");
			}
		}

	}

	public class ConfirmMemberDetailsResponse
	{
		public string memberId { get; set; }
	}


}
