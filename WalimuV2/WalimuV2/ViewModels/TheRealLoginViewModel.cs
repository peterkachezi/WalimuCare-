using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WalimuV2.ApiResponses;
using WalimuV2.Extensions;
using WalimuV2.Models;
using WalimuV2.Services;
using WalimuV2.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WalimuV2.ViewModels
{
	public class TheRealLoginViewModel : AppViewModel
	{

		private string email;
		public string Email
		{
			get { return email; }
			set
			{
				email = value;
				OnPropertyChanged(nameof(Email));
			}
		}


		private string pin;
		public string Pin
		{
			get { return pin; }
			set
			{
				pin = value;
				OnPropertyChanged(nameof(Pin));
			}
		}


		private bool isBusy { get; set; }


		private bool enableLoginBtn { get; set; }
		public bool EnableLoginBtn
		{
			get { return enableLoginBtn; }
			set
			{
				enableLoginBtn = value;
				OnPropertyChanged(nameof(EnableLoginBtn));
			}
		}


		private bool enableLoader;
		public bool EnableLoader
		{
			get { return enableLoader; }
			set { enableLoader = value; OnPropertyChanged(nameof(EnableLoader)); }
		}


		private string fullName;
		public string FullName
		{
			get { return fullName; }
			set { fullName = value; OnPropertyChanged(nameof(FullName)); }
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

		private string greetings;
		public string Greetings
		{
			get { return greetings; }
			set { greetings = value; }
		}

		private string formatedPhoneNumber;
		public string FormatedPhoneNumber
		{
			get { return formatedPhoneNumber; }
			set
			{
				formatedPhoneNumber = value;
				OnPropertyChanged(nameof(FormatedPhoneNumber));
			}
		}
		public ICommand LoginCommand { get; set; }
		public ICommand LoginCommand2 { get; set; }
		public ICommand PinSubmit { get; set; }
		public ICommand ForgotPinCommand { get; set; }

		public ICommand ResetPinCommand { get; set; }

		public TheRealLoginViewModel()
		{
			SetApiDetails();

			LoginCommand = new Command(async () => await SubmitLogin());

			LoginCommand2 = new Command(async () => await SubmitLogin2());

			PinSubmit = new Command(async () => await SubmitLogin());

			ForgotPinCommand = new Command(SubmitForgotPin);

			ResetPinCommand = new Command(async () => await ResetPin());

			EnableLoginBtn = true;

			EnableLoader = false;

			Email = Preferences.Get(nameof(AspNetUsers.email), "");
			//Pin = Preferences.Get(nameof(AspNetUsers.pinHash), "");
			Pin = "";
			//FullName = Preferences.Get(nameof(AspNetUsers.firstName), "") + " " + Preferences.Get(nameof(AspNetUsers.lastName), "");

			var firstName = Preferences.Get("firstName", string.Empty);

			var lastName = Preferences.Get("lastName", string.Empty);

			FullName = firstName + " " + lastName;

			PhoneNumber = Preferences.Get("phoneNumber", string.Empty);

			FullName = FullName.ToTitleCase();

			SetGreetings();

			FormatedPhoneNumber = FormatPhoneNumberWithX(StandardizePhoneNumber(PhoneNumber)) + " Not You?";

		}

		public void SetApiDetails()
		{
			if (Debugger.IsAttached)
			{
				ApiDetail.EndPoint = ApiDetail.LocalEndPoint;
			}
			else
			{
				ApiDetail.EndPoint = ApiDetail.LocalEndPoint;
			}
		}
		public async Task SubmitLogin2()
		{
			LoginCommand2.CanExecute(false);

			try
			{
				if (memberNumber == "")
				{
					await Application.Current.MainPage.Navigation.PushPopupAsync(new WalimuErrorPage("Please enter your Member Number"));

					return;
				}

				EnableLoader = true;

				if (await CheckInternetConnectivity())
				{
					if (await CheckIfApiDetailsAreSetUp())
					{
						//EnableLoginBtn = false;

						await ShowLoadingMessage("Please wait as we sign you in");

						var login = new Login()
						{
							MemberNumber = memberNumber.Trim(),

							Password = pin,
						};

						var httpClient = new HttpClient();

						var json = JsonConvert.SerializeObject(login);

						var content = new StringContent(json, Encoding.UTF8, "application/json");

						var response = await httpClient.PostAsync(ApiDetail.ApiUrl + "api/MemberAuth/Login", content);

						if (response.IsSuccessStatusCode)
						{
							Rootobject user = new Rootobject();

							var JsonResult = await response.Content.ReadAsStringAsync(); // reponse from api

							var result = JsonConvert.DeserializeObject<Rootobject>(JsonResult); // deserialize json to c #

							if (result.accountStatus == "Wrong Password")
							{
								await ShowErrorMessage("You have entered wrong password");

								Preferences.Clear();

								return;
							}

							if (result.accountStatus == "Account not available")
							{
								await ShowErrorMessage("Sorry account details not found , kindly create an account");

								Preferences.Clear();

								return;
							}

							Preferences.Set("accessToken", result.access_token);// saving reponse locally in essesials for validation

							Preferences.Set("userId", result.user_Id);

							Preferences.Set("memberId", result.member_Id);

							Preferences.Set("memberNumber", result.user_name);

							Preferences.Set("tokenExpirationTime", result.expiration_Time);

							Preferences.Set("firstName", result.firstName);

							Preferences.Set("lastName", result.lastName);

							Preferences.Set("phoneNumber", result.phoneNumber);

							Preferences.Set("gender", result.gender);

							Preferences.Set("accountStatus", result.accountStatus);

							Preferences.Set("schemeStatus", result.schemeStatus);

							Preferences.Set("dateOfBirth", result.dateOfBirth);

							Preferences.Set("currentTime", DateTimeOffset.Now.ToUnixTimeSeconds());

							Device.BeginInvokeOnMainThread(() =>
							{
								Application.Current.MainPage = new AppShell();

								//Application.Current.MainPage = new NavigationPage(new AppShell());

							});

							await RemoveLoadingMessage();

						}
						else
						{
							await ShowErrorMessage("Please enter Phone Number / Pin");

							Preferences.Clear();
						}


					}
					else
					{
						try
						{
							await ShowErrorMessage("Please enter Phone Number / Pin");

							Preferences.Clear();

							return;
						}
						catch (Exception ex)
						{
							SendErrorMessageToAppCenter(ex, "Login", "", PhoneNumber);

							Preferences.Clear();
						}
					}

				}

			}
			catch (Exception ex)
			{
				EnableLoginBtn = true;

				try
				{
					SendErrorMessageToAppCenter(ex, "Login", "", PhoneNumber);

					await ShowErrorMessage();

					Thread.Sleep(2000);
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}
			finally
			{
				IsBusy = false;

				EnableLoader = false;

				EnableSubmitBtn = true;

				Pin = "";

				try
				{
					//await App.Current.MainPage.Navigation.PopAllPopupAsync();
				}
				catch (Exception ex)
				{
					SendErrorMessageToAppCenter(ex, "Login", "", PhoneNumber);

					Console.WriteLine(ex);
				}

			}

			LoginCommand.CanExecute(true);

		}

		public async Task SubmitLogin()
		{
			LoginCommand.CanExecute(false);

			try
			{
				var memberNo = "";

				var savedMemberNo = Preferences.Get("memberNumber", string.Empty);

				memberNo = savedMemberNo;

				if (memberNo == "")
				{
					//await Application.Current.MainPage.Navigation.PushPopupAsync(new WalimuErrorPage("Something is not right, please log out then Login again"));

					//return;
					memberNo = memberNumber.Trim();
				}

				EnableLoader = true;

				if (await CheckInternetConnectivity())
				{
					if (await CheckIfApiDetailsAreSetUp())
					{
						EnableLoginBtn = false;

						await ShowLoadingMessage("Please wait as we sign you in");

						var login = new Login()
						{
							MemberNumber = memberNo,

							Password = pin,
						};

						var httpClient = new HttpClient();

						var json = JsonConvert.SerializeObject(login);

						var content = new StringContent(json, Encoding.UTF8, "application/json");

						var response = await httpClient.PostAsync(ApiDetail.ApiUrl + "api/MemberAuth/Login", content);

						if (response.IsSuccessStatusCode)
						{
							Rootobject user = new Rootobject();

							var JsonResult = await response.Content.ReadAsStringAsync(); // reponse from api

							var result = JsonConvert.DeserializeObject<Rootobject>(JsonResult); // deserialize json to c #

							if (result.accountStatus == "Wrong Password")
							{
								await ShowErrorMessage("You have entered wrong password");

								return;
							}

							if (result.accountStatus == "Account not available")
							{
								await ShowErrorMessage("Sorry account details not found , kindly create an account");

								return;
							}

							Preferences.Set("accessToken", result.access_token);// saving reponse locally in essesials for validation

							Preferences.Set("userId", result.user_Id);

							Preferences.Set("memberId", result.member_Id);

							Preferences.Set("memberNumber", result.user_name);

							Preferences.Set("tokenExpirationTime", result.expiration_Time);

							Preferences.Set("firstName", result.firstName);

							Preferences.Set("lastName", result.lastName);

							Preferences.Set("phoneNumber", result.phoneNumber);

							Preferences.Set("gender", result.gender);

							Preferences.Set("accountStatus", result.accountStatus);

							Preferences.Set("schemeStatus", result.schemeStatus);

							Preferences.Set("dateOfBirth", result.dateOfBirth);

							Preferences.Set("currentTime", DateTimeOffset.Now.ToUnixTimeSeconds());

							Device.BeginInvokeOnMainThread(() =>
							{
								Application.Current.MainPage = new AppShell();

								//Application.Current.MainPage = new NavigationPage(new AppShell());

							});

							await RemoveLoadingMessage();

						}
						else
						{
							await ShowErrorMessage("Please enter Phone Number / Pin");

						}


					}
					else
					{
						try
						{
							await ShowErrorMessage("Please enter Phone Number / Pin");

							return;
						}
						catch (Exception ex)
						{
							SendErrorMessageToAppCenter(ex, "Login", "", PhoneNumber);

						}
					}

				}

			}
			catch (Exception ex)
			{
				EnableLoginBtn = true;

				try
				{
					SendErrorMessageToAppCenter(ex, "Login", "", PhoneNumber);

					await ShowErrorMessage();

					Thread.Sleep(2000);
				}
				catch (Exception e)
				{

					Console.WriteLine(e.Message);

				}

			}
			finally
			{
				IsBusy = false;

				EnableLoader = false;

				EnableSubmitBtn = true;

				Pin = "";

				try
				{
					//await App.Current.MainPage.Navigation.PopAllPopupAsync();
				}
				catch (Exception ex)
				{
					SendErrorMessageToAppCenter(ex, "Login", "", PhoneNumber);

					Console.WriteLine(ex);
				}

			}

			LoginCommand.CanExecute(true);

		}


		//public async Task SubmitLogin1()
		//{
		//	LoginCommand.CanExecute(false);

		//	try
		//	{
		//		EnableLoader = true;

		//		if (await CheckInternetConnectivity())
		//		{
		//			if (await CheckIfApiDetailsAreSetUp())
		//			{
		//				if (!string.IsNullOrEmpty(PhoneNumber) && !string.IsNullOrEmpty(Pin))
		//				{

		//					EnableLoginBtn = false;

		//					await ShowLoadingMessage("Please wait as we sign you in");

		//					RestClient client = new RestClient(ApiDetail.EndPoint);

		//					RestRequest restRequest = new RestRequest()
		//					{
		//						Method = Method.Post,

		//						Resource = "/Registration/GetUser"
		//					};

		//					string phoneNumberMain = PhoneNumber.StartsWith("0") ? PhoneNumber.TrimStart('0') : PhoneNumber;

		//					object registration = new
		//					{
		//						pin = Convert.ToInt32(Pin),

		//						PhoneNumber = phoneNumberMain
		//					};

		//					restRequest.AddJsonBody(registration);

		//					AspNetUsers user = new AspNetUsers();

		//					try
		//					{
		//						var response = await Task.Run(() =>
		//						{
		//							return client.Execute(restRequest);

		//						});

		//						if (response.IsSuccessful && response.Content.Length > 2)
		//						{
		//							EnableLoginBtn = true;

		//							EnableLoader = false;

		//							var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<AspNetUsers>>(response.Content);

		//							if (deserializedResponse.success)
		//							{

		//								user = deserializedResponse.data;

		//								Preferences.Set(nameof(user.email), user.email);
		//								Preferences.Set(nameof(user.pinHash), user.pinHash);
		//								Preferences.Set(nameof(user.firstName), user.firstName);
		//								Preferences.Set(nameof(user.lastName), user.lastName);
		//								Preferences.Set(nameof(user.userName), user.userName);
		//								Preferences.Set(nameof(user.id), user.id);
		//								Preferences.Set(nameof(user.memberId), user.memberId);
		//								Preferences.Set(nameof(user.phoneNumber), user.phoneNumber);
		//								Preferences.Set(nameof(user.jobGroup), user.jobGroup);
		//								Preferences.Set(nameof(user.DateOfBirth), user.DateOfBirth);
		//								Preferences.Set(nameof(user.Gender), user.Gender);
		//								Preferences.Set(nameof(user.schemeId), user.schemeId);
		//								Preferences.Set(nameof(user.idNumber), user.idNumber);
		//								Preferences.Set(nameof(user.station), user.station);
		//								Preferences.Set(nameof(user.county), user.county);
		//								Preferences.Set(nameof(user.nhifNo), user.nhifNo);
		//								Preferences.Set(nameof(user.postal_address), user.postal_address);
		//								Preferences.Set(nameof(user.schemeId), user.schemeId);
		//								Preferences.Set(nameof(user.memberNumber), user.memberNumber);
		//								Preferences.Set(nameof(user.jobGroup), user.jobGroup);

		//								if (string.IsNullOrEmpty(user.schemeId))
		//								{
		//									Preferences.Set("SchemeId", Convert.ToInt32(user.schemeId));
		//								}
		//								Device.BeginInvokeOnMainThread(() =>
		//								{
		//									Application.Current.MainPage = new AppShell();

		//								});

		//								await RemoveLoadingMessage();
		//							}
		//							else if (deserializedResponse.message.ToLower().Contains("wrong"))
		//							{
		//								Pin = "";
		//								await ShowErrorMessage("Sorry,you have entered a wrong pin ,please try again");
		//							}
		//							else if (deserializedResponse.message.ToLower().Contains("tsc"))
		//							{
		//								Pin = "";
		//								await ShowErrorMessage("Sorry,you are not a member of TSC Scheme");
		//							}
		//							else
		//							{
		//								Pin = "";
		//								await ShowErrorMessage();

		//							}


		//						}
		//						else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
		//						{
		//							EnableLoginBtn = true;
		//							Pin = "";

		//							try
		//							{

		//								await ShowErrorMessage("Ooops, Something is not right, try again later. If this persists please consult the Admin");

		//								return;


		//							}
		//							catch (Exception ex)
		//							{
		//								SendErrorMessageToAppCenter(ex, "Login", "", PhoneNumber);
		//							}
		//						}
		//						else
		//						{
		//							EnableLoginBtn = true;
		//							Pin = "";

		//							try
		//							{

		//								await ShowErrorMessage("Ooops Login failed , Incorrect username / password combination");

		//								return;


		//							}
		//							catch (Exception ex)
		//							{
		//								SendErrorMessageToAppCenter(ex, "Login", "", PhoneNumber);
		//							}
		//						}


		//					}
		//					catch (Exception ex)
		//					{
		//						EnableLoginBtn = true;
		//						Pin = "";

		//						try
		//						{

		//							SendErrorMessageToAppCenter(ex, "Login", "", PhoneNumber);

		//							await ShowErrorMessage("Ooops Login failed , Something went wrong please try again later");

		//							Thread.Sleep(4000);
		//						}
		//						catch (Exception e)
		//						{
		//							Console.WriteLine(e.Message);
		//						}

		//					}
		//				}
		//				else
		//				{
		//					try
		//					{
		//						await ShowErrorMessage("Please enter Phone Number / Pin");

		//						return;
		//					}
		//					catch (Exception ex)
		//					{
		//						SendErrorMessageToAppCenter(ex, "Login", "", PhoneNumber);

		//					}
		//				}
		//			}
		//		}

		//	}
		//	catch (Exception ex)
		//	{
		//		EnableLoginBtn = true;

		//		try
		//		{
		//			SendErrorMessageToAppCenter(ex, "Login", "", PhoneNumber);

		//			await ShowErrorMessage();

		//			Thread.Sleep(2000);
		//		}
		//		catch (Exception e)
		//		{

		//			Console.WriteLine(e.Message);

		//		}

		//	}
		//	finally
		//	{
		//		IsBusy = false;

		//		EnableLoader = false;

		//		EnableSubmitBtn = true;

		//		Pin = "";

		//		try
		//		{
		//			//await App.Current.MainPage.Navigation.PopAllPopupAsync();
		//		}
		//		catch (Exception ex)
		//		{
		//			SendErrorMessageToAppCenter(ex, "Login", "", PhoneNumber);

		//			Console.WriteLine(ex);
		//		}

		//	}

		//	LoginCommand.CanExecute(true);

		//}

		public void SubmitForgotPin()
		{
			try
			{
				Application.Current.MainPage = new NavigationPage(new SignUpPage());
			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Login", "", PhoneNumber);
			}
		}

		public void SetGreetings()
		{
			try
			{
				var timeOfDay = DateTime.Now.TimeOfDay;

				if (timeOfDay > new TimeSpan(5, 0, 0) && timeOfDay < new TimeSpan(12, 0, 0))
				{
					Greetings = "Good Morning,  ";
				}
				else if (timeOfDay >= new TimeSpan(12, 0, 0) && timeOfDay < new TimeSpan(16, 0, 0))
				{
					Greetings = "Good Afternoon,  ";
				}
				else if (timeOfDay >= new TimeSpan(16, 0, 0) && timeOfDay < new TimeSpan(19, 0, 0))
				{
					Greetings = "Good Evening,  ";
				}
				else
				{
					Greetings = "Welcome back,   ";
				}
			}
			catch (Exception ex)
			{

				SendErrorMessageToAppCenter(ex, "Login", "", PhoneNumber);

				Greetings = "Welcome back, ";
			}
		}

		public string StandardizePhoneNumber(string thePhoneNumber)
		{
			try
			{
				string NewPhoneNumber = "";

				NewPhoneNumber = thePhoneNumber.StartsWith("+") ? "0" + thePhoneNumber.Substring(4) : thePhoneNumber;

				NewPhoneNumber = NewPhoneNumber.StartsWith("2") ? "0" + NewPhoneNumber.Substring(3) : NewPhoneNumber;

				string PhoneNumberWithZero = NewPhoneNumber.StartsWith("0") ? NewPhoneNumber : "0" + NewPhoneNumber;

				return PhoneNumberWithZero;

			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "The Real Login", "", PhoneNumber);
				return PhoneNumber;
			}
		}


		public static string FormatPhoneNumberWithX(string theString)
		{
			try
			{

				var aStringBuilder = new StringBuilder(theString);


				//aStringBuilder.Remove(6,2); first argument represents position, next argument represents number of characters
				aStringBuilder.Remove(4, 3);

				aStringBuilder.Insert(4, "XXX");

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

		public async Task ResetPin()
		{
			try
			{
				await App.Current.MainPage.Navigation.PushAsync(new ResetPinPage());
			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Login");
			}
		}

	}

}
