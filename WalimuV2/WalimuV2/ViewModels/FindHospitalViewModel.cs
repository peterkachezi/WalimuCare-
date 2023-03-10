

using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using WalimuV2.ApiResponses;
using WalimuV2.Interfaces;
using WalimuV2.Models;
using WalimuV2.Services;
using WalimuV2.Views.hospitals;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Map = Xamarin.Forms.GoogleMaps.Map;

namespace WalimuV2.ViewModels
{
	public class FindHospitalViewModel : AppViewModel
	{

		private bool proceedWithoutEnablingLocationPermission;
		public bool ProceedWithoutEnablingLocationPermission
		{
			get { return proceedWithoutEnablingLocationPermission; }
			set
			{
				proceedWithoutEnablingLocationPermission = value;
				OnPropertyChanged();
			}
		}


		private bool showEnableLocationServicesLabel;
		public bool ShowEnableLocationServicesLabel
		{
			get { return showEnableLocationServicesLabel; }
			set { showEnableLocationServicesLabel = value; OnPropertyChanged(); }
		}


		private bool userProceedsWithoutGpsBiengTurnedOn;
		public bool UserProceedsWithoutGpsBiengTurnedOn
		{
			get { return userProceedsWithoutGpsBiengTurnedOn; }
			set { userProceedsWithoutGpsBiengTurnedOn = value; OnPropertyChanged(); }
		}



		private bool hasErrorMessageAppearedMoreThanTwice;
		public bool HasErrorMessageAppearedMoreThanTwice
		{
			get { return hasErrorMessageAppearedMoreThanTwice; }
			set { hasErrorMessageAppearedMoreThanTwice = value; }
		}



		private bool isGpsEnabled;
		public bool IsGpsEnabled
		{
			get { return isGpsEnabled; }
			set
			{
				isGpsEnabled = value; OnPropertyChanged();

				if (IsGpsEnabled)
				{
					IsGpsEnabledVisible = false;
				}
				else
				{
					IsGpsEnabledVisible = true;
				}
			}
		}

		private bool isGpsEnabledVisible;
		public bool IsGpsEnabledVisible
		{
			get { return isGpsEnabledVisible; }
			set { isGpsEnabledVisible = value; OnPropertyChanged(); }
		}

		private int numberOfHospitals;
		public int NumberOfHospitals
		{
			get { return numberOfHospitals; }
			set
			{
				numberOfHospitals = value;

				OnPropertyChanged(nameof(NumberOfHospitals));
			}
		}

		public string PhoneNumber { get; set; }

		private string selectedSearchOption;
		public string SelectedSearchOption
		{
			get { return selectedSearchOption; }
			set { selectedSearchOption = value; OnPropertyChanged(); }
		}

		//public string SelectedSearchOption { get; set; }

		public bool IsAnimationOutInvoked { get; set; }
		private List<County> lstLocations { get; set; }
		public List<County> LstLocations
		{
			get { return lstLocations; }
			set
			{
				lstLocations = value;
				OnPropertyChanged(nameof(LstLocations));
			}
		}
		public List<County> OriginalLstLocations { get; set; }
		public List<Speciality> lstSpecialities { get; set; }
		public List<Speciality> LstSpecialities
		{
			get
			{
				return lstSpecialities;
			}

			set
			{
				lstSpecialities = value;

				OnPropertyChanged(nameof(LstSpecialities));
			}
		}

		public List<Speciality> OriginalLstSpecialities { get; set; }
		public List<Hospital> lstHospitals { get; set; }
		public List<Hospital> LstHospitals
		{
			get { return lstHospitals; }
			set
			{
				lstHospitals = value;

				NumberOfHospitals = lstHospitals.Count();

				OnPropertyChanged(nameof(LstHospitals));
			}
		}
		public List<Hospital> OriginalLstHospitals { get; set; }

		private HospitalDetails selectedHospitalDetails;
		public HospitalDetails SelectedHospitalDetails
		{
			get { return selectedHospitalDetails; }
			set
			{
				selectedHospitalDetails = value;

				OnPropertyChanged(nameof(SelectedHospitalDetails));
			}
		}

		private string searchString;
		public string SearchString
		{
			get { return searchString; }
			set
			{
				searchString = value;

				OnPropertyChanged(nameof(SearchString));

				SearchCountyOrSpecialty();
			}
		}
		private string searchBy;
		public string SearchBy
		{
			get { return searchBy; }

			set { searchBy = value; OnPropertyChanged(nameof(SearchBy)); }
		}

		private string nameOfSelectedArea;
		public string NameOfSelectedArea
		{
			get { return nameOfSelectedArea; }

			set
			{
				nameOfSelectedArea = value;
				OnPropertyChanged(nameof(NameOfSelectedArea));
			}
		}
		private bool isCountyListViewVisible;
		public bool IsCountyListViewVisible
		{
			get { return isCountyListViewVisible; }

			set
			{
				isCountyListViewVisible = value;

				OnPropertyChanged(nameof(IsCountyListViewVisible));
			}
		}

		private bool isSpecialityListViewVisible;
		public bool IsSpecialityListViewVisible
		{
			get { return isSpecialityListViewVisible; }
			set
			{
				isSpecialityListViewVisible = value;

				OnPropertyChanged(nameof(IsSpecialityListViewVisible));
			}
		}
		private bool isHospitalSearchListViewVisible;
		public bool IsHospitalSearchListViewVisible
		{
			get { return isHospitalSearchListViewVisible; }
			set
			{
				isHospitalSearchListViewVisible = value;
				OnPropertyChanged(nameof(IsHospitalSearchListViewVisible));
			}
		}

		private string placeHolderText;
		public string PlaceHolderText
		{
			get { return placeHolderText; }
			set { placeHolderText = value; OnPropertyChanged(nameof(PlaceHolderText)); }
		}
		public Location selectedLocation { get; set; }
		public Location CurrentLocation { get; set; }

		private Map map;
		public Map Map
		{
			get { return map; }

			set { map = value; OnPropertyChanged(); }
		}

		private bool isSelectCurrentLocationVisible;
		public bool IsSelectCurrentLocationVisible
		{
			get { return isSelectCurrentLocationVisible; }
			set
			{
				isSelectCurrentLocationVisible = value;
				OnPropertyChanged();
			}
		}

		//public Map Map { get; set; }

		public ICommand ShowSelectCountyOrSpecialityPageCommand { get; set; }

		public ICommand SelectCountyCommand { get; set; }

		public ICommand SelelectHospitalsInSpecialityCommand { get; set; }

		public ICommand SelectHospitalDetailsCommand { get; set; }

		public ICommand RefreshHospitalViewModelCommand { get; set; }

		public ICommand ShowEnableGpsPageCommand { get; set; }

		public ICommand RequestLocationPermisionCommand { get; set; }

		public ICommand GetUserSelectedLocationCommand { get; set; }

		public ICommand EnableCurrentLocationSelectionCommand { get; set; }

		public FindHospitalViewModel()
		{
			PageTitle = "Find Hospital";

			PageSubTitle = "Find List of Hospitals Near Your Area";

			try
			{

				Map = new Map()
				{
					HorizontalOptions = LayoutOptions.FillAndExpand,

					VerticalOptions = LayoutOptions.FillAndExpand,

					Margin = 0,

					Padding = 0
				};

				var assembly = IntrospectionExtensions.GetTypeInfo(typeof(FindHospitalViewModel)).Assembly;

				Stream stream = assembly.GetManifestResourceStream("WalimuV2.SimpleLabel.json");

				string text = "";

				using (var reader = new StreamReader(stream))
				{
					text = reader.ReadToEnd();
				}
				//string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MapStyle.json");

				//string fileContent = File.ReadAllText(fileName);

				Map.MapStyle = MapStyle.FromJson(text);

				SetSearchByOption("County");

				SelectedSearchOption = "County";

				NameOfSelectedArea = "you";

				PhoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), "");

				ShowSelectCountyOrSpecialityPageCommand = new Command(async () => await ShowSelectCountyOrSpecialityPage());

				SelectCountyCommand = new Command<int>(async x => await SearchHospitalsBySelectedCounty(x));

				SelelectHospitalsInSpecialityCommand = new Command<int>(async x => await SelelectHospitalsInSpeciality(x));

				SelectHospitalDetailsCommand = new Command<int>(async x => await GetHospitalDetails(x));

				RefreshHospitalViewModelCommand = new Command(SetUpFindHospitalViewModel);

				ShowEnableGpsPageCommand = new Command(async () => await ShowEnableGpsPage());

				RequestLocationPermisionCommand = new Command(async () => await RequestLocationPermision());

				GetUserSelectedLocationCommand = new Command(async () => await GetUserSelectedLocation());

				EnableCurrentLocationSelectionCommand = new Command(EnableCurrentLocationSelection);

				IsSelectCurrentLocationVisible = false;

				SetUpFindHospitalViewModel();

			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Find Hospital", "", PhoneNumber);

				Task.Run(async () =>
				{
					await ShowErrorMessage();
				});
			}

		}

		private async void SetUpFindHospitalViewModel()
		{
			try
			{
				IsGpsEnabled = DependencyService.Get<ILocSettings>().isGpsAvailable();

				if (IsGpsEnabled == false)
				{
					if (UserProceedsWithoutGpsBiengTurnedOn)
					{

					}
					else
					{
						await Shell.Current.Navigation.PushModalAsync(new EnableGpsPage());
						return;
					}

				}

				if (IsGpsEnabled)
				{
					await CheckIfLocationPermissionIsEnabled();
				}

				if (IsGpsEnabled == true && ShowEnableLocationServicesLabel == true && ProceedWithoutEnablingLocationPermission == false)
				{

					await Shell.Current.Navigation.PushModalAsync(new UserLocationPage());

					return;
				}

				await ShowLoadingMessage();

				IsRefreshing = true;

				NameOfSelectedArea = "you";

				IsSelectCurrentLocationVisible = false;

				if (!await CheckInternetConnectivity())
				{
					IsRefreshing = false;
					return;
				}

				if (IsGpsEnabled == true)
				{
					await GetUserCurrentLocation();
				}
				await GetAllCounties();

				await GetAllHospitals();

				await GetAllSpecialities();

				if (IsGpsEnabled)
				{
					await GetHospitalsClosestToUser();

					await GeneratePinsAndAssignOnMap();

					double zoomDistance = await GetLargestDistanceBetweenHospitals(CurrentLocation);

					await MoveOnMap(CurrentLocation, zoomDistance);

				}
				else
				{
					//SHOW PAGE TO SELECT COUNTY
					LstHospitals = OriginalLstHospitals;

					await GeneratePinsAndAssignOnMap();

					CurrentLocation = new Location()
					{
						Latitude = -1.2862394,

						Longitude = 36.78932030000001
					};

					await MoveOnMap(CurrentLocation, 50);
				}
				await RemoveLoadingMessage();

				IsRefreshing = false;
			}
			catch (Exception ex)
			{
				await ShowErrorMessage();

				SendErrorMessageToAppCenter(ex, "Find Hospital", "", PhoneNumber);
			}
		}
		public async Task GetAllCounties()
		{
			try
			{
				await Task.Delay(1000);

				RestClient client = new RestClient(ApiDetail.EndPoint);

				RestRequest restRequest = new RestRequest()
				{
					Method = Method.Get,

					Resource = "/Hospitals/GetAllCounties"
				};

				var response = await client.ExecuteAsync(restRequest);

				if (response.IsSuccessful)
				{

					var deserializedData = JsonConvert.DeserializeObject<BaseResponse<List<County>>>(response.Content);

					if (deserializedData.success)
					{
						LstLocations = deserializedData.data;

						OriginalLstLocations = deserializedData.data;

						//cmbLocations.DataSource = LstLocations.Select(p => p.county).ToList();
					}
					else
					{
						await ShowErrorMessage("Something went wrong when getting list of counties, please try again later");
					}
				}
			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Find Hospital", "", PhoneNumber);

				Console.Write(ex);
			}

		}
		public async Task GetAllHospitals()
		{
			try
			{

				//if (await CheckInternetConnectivity())
				//{
				//    if (await CheckIfApiDetailsAreSetUp())
				//    {

				RestClient client = new RestClient(ApiDetail.EndPoint);

				RestRequest restRequest = new RestRequest()
				{
					Method = Method.Get,
					Resource = "/Hospitals/GetAllClinics"
				};

				var response = await client.ExecuteAsync(restRequest);

				if (response.IsSuccessful)
				{
					var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<List<Hospital>>>(response.Content);

					if (deserializedResponse.success)
					{
						var currentLocation = CurrentLocation;

						OriginalLstHospitals = deserializedResponse.data.OrderBy(p => p.name).ToList();

						LstHospitals = new List<Hospital>();

						NumberOfHospitals = LstHospitals.Count;

					}
					//else
					//{
					//    await ShowErrorMessage("Something went wrong when getting list of Hospitals");
					//}

				}
			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Find Hospital", "", PhoneNumber);
				Console.Write(ex);
			}

		}
		public async Task GetAllSpecialities()
		{
			try
			{
				RestClient client = new RestClient(ApiDetail.EndPoint);

				RestRequest restRequest = new RestRequest()
				{
					Method = Method.Get,
					Resource = "/Specialists/GetAllSpecialists"
				};

				var response = await client.ExecuteAsync(restRequest);

				if (response.IsSuccessful)
				{
					var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<List<Speciality>>>(response.Content);

					if (deserializedResponse.success)
					{
						LstSpecialities = deserializedResponse.data;
						OriginalLstSpecialities = deserializedResponse.data;
					}
				}

			}
			catch (Exception ex)
			{

				SendErrorMessageToAppCenter(ex, "Find Hospital", "", PhoneNumber);
			}

		}
		public async Task MoveOnMap(Location location, double Kilometers)
		{
			try
			{

				if (location == null)
				{
					location = await Geolocation.GetLastKnownLocationAsync();
				}

				if (location != null)
				{
					Position position = new Position(location.Latitude, location.Longitude);

					MainThread.BeginInvokeOnMainThread(() =>
					{
						Map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(Kilometers)));

					});
					//Map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(Kilometers)));
				}

			}
			catch (Exception ex)
			{
				await ShowErrorMessage("Something went wrong when moving on map");

				SendErrorMessageToAppCenter(ex, "Find Hospital", "", PhoneNumber);
			}
		}

		public async Task GetHospitalsWithinLocation(Location selectedLocation, double radiusInKm)
		{
			try
			{

				if (selectedLocation == null)
				{
					selectedLocation = await Geolocation.GetLastKnownLocationAsync();
				}

				LstHospitals = new List<Hospital>();

				double distance = -1.0;

				foreach (var hosp in OriginalLstHospitals.Where(p => p.latitude != null && !p.latitude.Contains(",") && p.longitude != null))
				{

					bool isConversionSuccessful = false;

					double latitude = 0;

					double longitude = 0;

					isConversionSuccessful = double.TryParse(hosp.latitude, out latitude);

					isConversionSuccessful = double.TryParse(hosp.longitude, out longitude);

					if (isConversionSuccessful)
					{

						var hospitalLocation = new Location(latitude, longitude);

						distance = Location.CalculateDistance(selectedLocation, hospitalLocation, DistanceUnits.Kilometers);
						//distance = MyLocationCalculator.CalculateDistance(selectedLocation, hospitalLocation);

						hosp.DistanceFromLocation = distance;

						if (distance <= radiusInKm)
						{
							LstHospitals.Add(hosp);
						}
					}
				}
				LstHospitals = LstHospitals.OrderBy(p => p.DistanceFromLocation).ToList();

			}
			catch (Exception ex)
			{

				SendErrorMessageToAppCenter(ex, "Find Hospital", "", PhoneNumber);
				//await ShowErrorMessage();
			}
		}

		public async Task GetHospitalsClosestToUser()
		{
			try
			{
				LstHospitals = new List<Hospital>();

				for (int i = 1; i < 50; i++)
				{
					if (LstHospitals.Count == 0)
					{
						await GetHospitalsWithinLocation(CurrentLocation, i);
					}
					else
					{
						break;
					}


				}
			}
			catch (Exception ex)
			{
				await ShowErrorMessage();
				SendErrorMessageToAppCenter(ex, "Find Hospital", "", PhoneNumber);
			}
		}

		private async Task GeneratePinsAndAssignOnMap()
		{
			try
			{

				//Map = new Map()
				//{
				//    HorizontalOptions = LayoutOptions.FillAndExpand,
				//    VerticalOptions = LayoutOptions.FillAndExpand,
				//    Margin = 0,
				//    Padding = 0
				//};


				var lastPin = new Pin();


				MainThread.BeginInvokeOnMainThread(() =>
				{
					Map.Pins.Clear();
				});



				foreach (var hosp in LstHospitals.Where(p => p.latitude != null && !p.latitude.Contains(",") && p.longitude != null))
				{

					bool isConversionSuccessful = false;
					double latitude = 0;
					double longitude = 0;

					isConversionSuccessful = Double.TryParse(hosp.latitude, out latitude);
					isConversionSuccessful = Double.TryParse(hosp.longitude, out longitude);

					if (isConversionSuccessful)
					{
						Pin pin = new Pin()
						{
							Type = PinType.Place,
							Label = hosp.name,
							Address = hosp.address,
							Position = new Position(latitude, longitude),
							Rotation = 33.3f,
							Tag = hosp.name,
							//Icon = BitmapDescriptorFactory.FromBundle("hospitalmapicon"),

						};


						//pin.Clicked += Pin_Clicked;

						MainThread.BeginInvokeOnMainThread(() =>
						{
							Map.Pins.Add(pin);
						});


					}


				}

				NumberOfHospitals = LstHospitals.Count;

			}
			catch (Exception ex)
			{

				SendErrorMessageToAppCenter(ex, "Find Hospital", "", PhoneNumber);
				await ShowErrorMessage();
			}
		}

		public async Task ShowSelectCountyOrSpecialityPage()
		{
			try
			{
				await Shell.Current.Navigation.PushAsync(new SelectCountyOrSpeciality());
			}
			catch (Exception ex)
			{
				await ShowErrorMessage();
				SendErrorMessageToAppCenter(ex, "Find Hospital", "", PhoneNumber);
			}
		}

		public void SearchCountyOrSpecialty()
		{
			try
			{
				if (SearchBy != null && SearchBy.Trim() != "")
				{
					if (SearchBy.ToLower().Trim().Contains("county"))
					{
						if (SearchString != null && SearchString.Trim() != "")
						{
							LstLocations = OriginalLstLocations.Where(p => p.county.ToLower().Trim().Contains(SearchString.ToLower().Trim())).ToList();
						}
						else
						{
							LstLocations = OriginalLstLocations;
						}

					}
					else if (SearchBy.ToLower().Trim().Contains("service"))
					{
						if (SearchString != null && SearchString.Trim() != "")
						{
							LstSpecialities = OriginalLstSpecialities.Where(p => p.serviceName.ToLower().Trim().Contains(SearchString.ToLower().Trim())).ToList();
						}
						else
						{
							LstSpecialities = OriginalLstSpecialities;
						}
					}
					else
					{
						if (SearchString != null && SearchString.Trim() != "")
						{
							LstHospitals = OriginalLstHospitals.Where(p => p.name.ToLower().Trim().Contains(SearchString.ToLower().Trim())).ToList();
						}
						else
						{
							LstHospitals = OriginalLstHospitals;
						}
					}
				}
			}
			catch (Exception ex)
			{

				SendErrorMessageToAppCenter(ex, "Find Hospital", "", PhoneNumber);
			}
		}

		public async Task SearchHospitalsBySelectedCounty(int Id)
		{
			try
			{
				await ShowLoadingMessage();

				LstHospitals = new List<Hospital>();

				LstHospitals = OriginalLstHospitals.Where(p => p.county == Id).ToList();

				var countyName = OriginalLstLocations.Where(p => p.pkid == Id).Select(x => x.county).FirstOrDefault();

				NameOfSelectedArea = countyName;

				var CountyLocations = await Geocoding.GetLocationsAsync(countyName);

				var CountyLocation = CountyLocations.FirstOrDefault();

				double zoomDistance = await GetLargestDistanceBetweenHospitals(CountyLocation);

				await GeneratePinsAndAssignOnMap();

				await MoveOnMap(CountyLocation, zoomDistance);

				try
				{
					await App.Current.MainPage.Navigation.PopAsync();
					//await Shell.Current.Navigation.PopAsync();
				}
				catch (Exception e)
				{
					await Shell.Current.GoToAsync(nameof(FindHospitalPage));
					SendErrorMessageToAppCenter(e, "find hospital");
				}



				await RemoveLoadingMessage();
			}
			catch (Exception ex)
			{
				await ShowErrorMessage("Something went wrong when getting hospitals");
				SendErrorMessageToAppCenter(ex, "Find Hospital", "", PhoneNumber);
			}
		}

		public async Task<double> GetLargestDistanceBetweenHospitals(Location referenceLocation)
		{
			try
			{
				double largestDistance = 0;

				if (referenceLocation == null)
				{
					referenceLocation = await Geolocation.GetLastKnownLocationAsync();
				}

				foreach (var hosp in LstHospitals.Where(p => p.latitude != null && !p.latitude.Contains(",") && p.longitude != null))
				{

					bool isConversionSuccessful = false;
					double latitude = 0;
					double longitude = 0;

					isConversionSuccessful = Double.TryParse(hosp.latitude, out latitude);
					isConversionSuccessful = Double.TryParse(hosp.longitude, out longitude);

					if (isConversionSuccessful)
					{

						var hospitalLocation = new Location(latitude, longitude);

						var distance = Location.CalculateDistance(referenceLocation, hospitalLocation, DistanceUnits.Kilometers);

						if (distance > largestDistance)
						{
							largestDistance = distance;
						}

					}
				}

				//if(largestDistance > 50)
				//{
				//    largestDistance = 30;
				//}

				return largestDistance;
			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Find Hospital", "", PhoneNumber);
				return 20;
			}
		}

		public async Task SelelectHospitalsInSpeciality(int SpecialityId)
		{
			try
			{
				await ShowLoadingMessage();

				await GetHospitalPerSpecialityApiRequest(SpecialityId);

				var specialityName = OriginalLstSpecialities.Where(p => p.pkid == SpecialityId).Select(x => x.serviceName).FirstOrDefault();

				NameOfSelectedArea = specialityName;



				double zoomDistance = await GetLargestDistanceBetweenHospitals(CurrentLocation);

				await GeneratePinsAndAssignOnMap();

				await MoveOnMap(CurrentLocation, zoomDistance);

				try
				{
					await App.Current.MainPage.Navigation.PopAsync();
					//await Shell.Current.Navigation.PopAsync();
				}
				catch (Exception e)
				{
					await Shell.Current.GoToAsync(nameof(FindHospitalPage));
					SendErrorMessageToAppCenter(e, "find hospital");
				}

				await RemoveLoadingMessage();


			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Find Hospital", "", PhoneNumber);
				await ShowErrorMessage("Something went wrong when searching by specialities, please tr again");
			}
		}

		private async Task GetHospitalPerSpecialityApiRequest(int SpecialityId)
		{
			try
			{

				if (await CheckInternetConnectivity())
				{
					if (await CheckIfApiDetailsAreSetUp())
					{



						RestClient client = new RestClient(ApiDetail.EndPoint);

						RestRequest restRequest = new RestRequest()
						{
							Method = Method.Post,
							Resource = "/Specialists/GetClinicsPerSpecialist"
						};

						restRequest.AddQueryParameter("serviceID", SpecialityId.ToString());

						var response = await client.ExecuteAsync(restRequest);

						if (response.IsSuccessful)
						{

							var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<List<Clinic>>>(response.Content);

							if (deserializedResponse.success)
							{

								var clinics = deserializedResponse.data;
								//assign pins on map for this speciality

								var newHospitals = new List<Hospital>();

								foreach (var item in clinics)
								{
									Hospital hospital = new Hospital()
									{
										name = item.clinicName,
										latitude = item.latitude,
										longitude = item.longitude,
										pkid = item.pkId
									};


									newHospitals.Add(hospital);
								}

								LstHospitals = newHospitals;

							}
							else
							{
								await ShowErrorMessage("Something went wrong getting hospitals");
							}

						}
						else
						{
							await ShowErrorMessage("Something went wrong getting hospitals with that speciality");
						}
					}
				}

			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Find Hospital", "", PhoneNumber);
				Console.Write(ex);
			}

		}

		private async Task GetUserCurrentLocation()
		{
			try
			{
				MainThread.BeginInvokeOnMainThread(async () =>
				{

					var locationInUseAllowed = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
					//var locationAlwaysAllowed = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();

					if (locationInUseAllowed == PermissionStatus.Granted)
					{
						CurrentLocation = await Geolocation.GetLocationAsync();
					}
					else
					{

						//use hk location
						CurrentLocation = new Location()
						{
							Latitude = -1.2862394,
							Longitude = 36.78932030000001
						};
					}

				});

			}
			catch (Exception ex)
			{
				CurrentLocation = await Geolocation.GetLastKnownLocationAsync();
				SendErrorMessageToAppCenter(ex, "Find Hospital", "", PhoneNumber);
			}
		}

		public void SetSearchByOption(string SelectedOption)
		{
			try
			{
				if (SelectedOption != null)
				{
					if (SelectedOption.Trim().ToLower().Contains("county"))
					{
						SearchBy = "county";
						IsCountyListViewVisible = true;
						IsSpecialityListViewVisible = false;
						IsHospitalSearchListViewVisible = false;
						PlaceHolderText = "Enter County";
					}
					else if (SelectedOption.Trim().ToLower().Contains("service"))
					{
						SearchBy = "service";
						IsCountyListViewVisible = false;
						IsSpecialityListViewVisible = true;
						IsHospitalSearchListViewVisible = false;
						PlaceHolderText = "Enter Service Name";
					}
					else if (SelectedOption.Trim().ToLower().Contains("hospital"))
					{
						SearchBy = "hospital";
						IsCountyListViewVisible = false;
						IsSpecialityListViewVisible = false;
						IsHospitalSearchListViewVisible = true;
						PlaceHolderText = "Enter Hospital";
						LstHospitals = OriginalLstHospitals.OrderBy(p => p.name).ToList();
					}

					SelectedSearchOption = SelectedOption;

				}
			}
			catch (Exception ex)
			{

				SendErrorMessageToAppCenter(ex, "Find Hospital", "", PhoneNumber);
			}
		}

		private async Task GetHospitalDetails(int HospitalId)
		{
			try
			{

				if (await CheckInternetConnectivity())
				{
					if (await CheckIfApiDetailsAreSetUp())
					{

						if (SelectedHospitalDetails != null)
						{
							SelectedHospitalDetails.Category = "";
							SelectedHospitalDetails.DescriptionOfLocation = "";
							SelectedHospitalDetails.Email = "";
							SelectedHospitalDetails.PhoneNumber = "";
							SelectedHospitalDetails.Website = "";
							SelectedHospitalDetails.WorkingHours = "";
							SelectedHospitalDetails.HospitalName = "";
						}

						await ShowLoadingMessage();


						RestClient client = new RestClient(ApiDetail.EndPoint);

						RestRequest restRequest = new RestRequest()
						{
							Method = Method.Get,
							Resource = "/Hospitals/GetHospitalDetails"
						};

						restRequest.AddParameter("HospitalId", HospitalId);

						var response = await client.ExecuteAsync(restRequest);

						if (response.IsSuccessful)
						{


							var serializedResponse = JsonConvert.DeserializeObject<BaseResponse<HospitalDetails>>(response.Content);

							if (serializedResponse.success)
							{
								SelectedHospitalDetails = serializedResponse.data;
								selectedHospitalDetails.HospitalName = LstHospitals.Where(p => p.pkid == HospitalId).Select(x => x.name).FirstOrDefault()
;

								//lblDescriptionOfLocation.CustomText = SelectedHospitalDetails.DescriptionOfLocation ?? "";
								//lblWorkingHours.CustomText = SelectedHospitalDetails.WorkingHours ?? "";
								//lblPhoneNumber.CustomText = SelectedHospitalDetails.PhoneNumber ?? "";
								//lblEmail.CustomText = SelectedHospitalDetails.Email ?? "";
								//lblWebsite.CustomText = SelectedHospitalDetails.Website ?? "";


								try
								{
									double latitude = CurrentLocation.Latitude;
									double longitude = CurrentLocation.Longitude;

									bool checkIfConversionIsSuccessful = Double.TryParse(lstHospitals.Where(p => p.pkid == HospitalId).FirstOrDefault().latitude, out latitude);

									checkIfConversionIsSuccessful = Double.TryParse(lstHospitals.Where(p => p.pkid == HospitalId).FirstOrDefault().longitude, out longitude);

									Position position = position = new Position(latitude, longitude);

									selectedLocation = new Location(latitude, longitude);

									await MoveOnMap(selectedLocation, 1);

									await App.Current.MainPage.Navigation.PushAsync(new SelectedHospitalDetailsPage());

									await RemoveLoadingMessage();

								}
								catch (Exception ex)
								{

									SendErrorMessageToAppCenter(ex, "Find Hospital", "", PhoneNumber);
								}

							}
							else
							{
								await ShowErrorMessage("Sorry Something went wrong");
							}

						}
					}

				}

			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Find Hospital", "", PhoneNumber);

				Console.Write(ex);
			}

		}

		public async Task ShowEnableGpsPage()
		{
			try
			{
				await App.Current.MainPage.Navigation.PushModalAsync(new EnableGpsPage());
			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Find Hospital");
			}
		}
		public async Task CheckIfLocationPermissionIsEnabled()
		{
			try
			{
				var locationInUseAllowed = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
				//var locationAlwaysAllowed = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();

				if (locationInUseAllowed == PermissionStatus.Denied)
				{
					ShowEnableLocationServicesLabel = true;

					//request user to allow access to location 
					//await RequestLocationPermision();
				}
				else
				{
					ShowEnableLocationServicesLabel = false;
				}
			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Find Hospital");
			}
		}
		public async Task RequestLocationPermision()
		{
			try
			{

				await Shell.Current.Navigation.PushModalAsync(new UserLocationPage());


				////await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

				//var locationInUseAllowed = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
				////var locationAlwaysAllowed = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();

				//if (locationInUseAllowed == PermissionStatus.Denied)
				//{
				//    ShowEnableLocationServicesLabel = true;



				//}
				//else
				//{
				//    ShowEnableLocationServicesLabel = false;
				//}

				////SetUpFindHospitalViewModel();
			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Find Hospital");
			}
		}
		public async Task GetUserSelectedLocation()
		{
			try
			{

				var theSelectedLoc = Map.CameraPosition.Target;

				CurrentLocation = new Location()
				{
					Latitude = theSelectedLoc.Latitude,
					Longitude = theSelectedLoc.Longitude
				};


				await GetHospitalsClosestToUser();

				await GeneratePinsAndAssignOnMap();

				double zoomDistance = await GetLargestDistanceBetweenHospitals(CurrentLocation);

				await MoveOnMap(CurrentLocation, zoomDistance);

				IsSelectCurrentLocationVisible = false;

				var places = await Geocoding.GetPlacemarksAsync(CurrentLocation);

				if (places != null)
				{
					var featuredPlace = places?.FirstOrDefault();

					if (featuredPlace != null)
					{
						string featureName = featuredPlace.FeatureName;


						if (featureName?.Length > 10)
						{
							featureName = featureName.Substring(0, 10) + "...";
						}

						NameOfSelectedArea = featureName;
					}
					else
					{
						NameOfSelectedArea = "you";
					}
				}
				else
				{
					NameOfSelectedArea = "you";
				}


			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Find Hospital");
			}
		}
		public void EnableCurrentLocationSelection()
		{
			try
			{

				Map.Pins.Clear();
				IsSelectCurrentLocationVisible = true;

			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Find Hospital");
			}
		}
	}

}
