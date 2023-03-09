using Android.Util;
using Java.Lang.Reflect;
using Newtonsoft.Json;
using RestSharp;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Provider.MediaStore;
using System.Threading.Tasks;
using System.Windows.Input;
using WalimuV2.ApiResponses;
using WalimuV2.Models;
using WalimuV2.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.IO;

namespace WalimuV2.ViewModels
{
	public class ECardViewModel : AppViewModel
	{

		private string photoPath;
		public string PhotoPath
		{
			get { return photoPath; }
			set
			{
				photoPath = value;
				OnPropertyChanged(nameof(PhotoPath));
			}
		}

		//private readonly DependantService dependantService;

		private List<Member> dependants;
		public List<Member> Dependants
		{
			get { return dependants; }

			set { dependants = value; OnPropertyChanged(); }
		}

		private Member selectedDependant;
		public Member SelectedDependant
		{
			get { return selectedDependant; }

			set { selectedDependant = value; OnPropertyChanged(); }
		}

		private byte[] fileBytesToBeUploaded;
		public byte[] FileBytesToBeUploaded
		{
			get { return fileBytesToBeUploaded; }
			set { fileBytesToBeUploaded = value; }
		}
		private bool isUploadVisible = true;
		public bool IsUploadVisible
		{
			get { return isUploadVisible; }
			set { isUploadVisible = value; }
		}

		private string selectedFileName;

		public string SelectedFileName
		{
			get { return selectedFileName; }
			set { selectedFileName = value; }
		}
		public ICommand GetDependantsCommand { get; set; }

		public ICommand ViewECardCommand { get; set; }

		public ICommand DownloadECardCommand { get; set; }

		public ICommand TakePhotoCommand { get; set; }

		public ICommand PickPictureCommand { get; set; }

		public ICommand ShowUploadPopUpCommand { get; set; }

		public ICommand UploadImageToServerCommand { get; set; }

		public ICommand ClosePopUpCommand { get; set; }

		public ECardViewModel()
		{
			//dependantService = DependencyService.Get<DependantService>();

			GetDependantsCommand = new Command(async () => await GetDependants());

			//ViewECardCommand = new Command<string>(async x => await ViewECard(x));

			DownloadECardCommand = new Command(async () => await DownloadECard());

			TakePhotoCommand = new Command(async () => await TakePhoto());

			PickPictureCommand = new Command(async () => await PickPicture());

			//ShowUploadPopUpCommand = new Command(async () => await ShowUploadPopUp());

			UploadImageToServerCommand = new Command(async () => await UploadImageToServer());

			ClosePopUpCommand = new Command(async () => await RemoveLoadingMessage());

			Task.Run(async () => await GetDependants());
		}


		public async Task GetDependants()
		{
			//try
			//{

			//	if (await CheckInternetConnectivity())
			//	{

			//		await ShowLoadingMessage();

			//		string MemberId = Preferences.Get(nameof(AspNetUsers.memberId), "");

			//		IsRefreshing = true;

			//		var data = await dependantService.GetDependants(MemberId);

			//		IsRefreshing = false;

			//		if (data != null)
			//		{

			//			//data.Insert(0, new Member()
			//			//{
			//			//	MemberNumber = Preferences.Get("MemberNumber", ""),
			//			//	FirstName = Preferences.Get(nameof(AspNetUsers.firstName), ""),
			//			//	LastName = Preferences.Get(nameof(AspNetUsers.lastName), ""),
			//			//	Gender = Preferences.Get(nameof(AspNetUsers.Gender), ""),
			//			//	DateOfBirth = Preferences.Get(nameof(AspNetUsers.DateOfBirth), DateTime.Now.AddYears(-100)),
			//			//	//MiddleName = Preferences.Get(nameof(AspNetUsers.userName), ""),
			//			//	Relationship = "Principal",
			//			//	Id = Preferences.Get(nameof(AspNetUsers.id), ""),
			//			//	JobGroup = Preferences.Get(nameof(AspNetUsers.jobGroup), "")
			//			//});

			//			data = data.Select(p => { p.JobGroup = Preferences.Get(nameof(AspNetUsers.jobGroup), ""); return p; }).ToList();

			//			Dependants = data;
			//		}
			//		else
			//		{
			//			Dependants = new List<Member>()
			//			{
			//				//new Member()
			//				//{
			//				//	FirstName = Preferences.Get(nameof(AspNetUsers.firstName), ""),
			//				//	LastName = Preferences.Get(nameof(AspNetUsers.lastName), ""),
			//				//	MiddleName = Preferences.Get(nameof(AspNetUsers.userName), ""),
			//				//	Relationship = "Principal",
			//				//	Id = Preferences.Get(nameof(AspNetUsers.id), ""),
			//				//	JobGroup = Preferences.Get(nameof(AspNetUsers.jobGroup),""),
			//				//	Gender = Preferences.Get(nameof(AspNetUsers.Gender), ""),
			//				//	DateOfBirth = Preferences.Get(nameof(AspNetUsers.DateOfBirth), DateTime.Now.AddYears(-100)),
			//				//}
			//			};
			//		}

			//		await RemoveLoadingMessage();

			//	}

			//}
			//catch (Exception ex)
			//{

			//	SendErrorMessageToAppCenter(ex, "E-Card");
			//}
		}

		//public async Task ViewECard(string Id)
		//{
		//	try
		//	{
		//		SelectedDependant = dependants.Where(p => p.Id == Id).FirstOrDefault();

		//		PhotoPath = null;

		//		await GetEcardPhotoFromServer();

		//		await Shell.Current.GoToAsync(nameof(ViewECardPage), true);
		//	}
		//	catch (Exception ex)
		//	{

		//		SendErrorMessageToAppCenter(ex, "E Card");
		//	}
		//}


		public async Task DownloadECard()
		{
			//try
			//{

			//	var storageReadStatus = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

			//	var storageWriteStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

			//	PermissionStatus storageReadIsAllowed = PermissionStatus.Denied;

			//	PermissionStatus storageWriteIsAllowed = PermissionStatus.Denied;


			//	if (storageReadStatus == PermissionStatus.Denied)
			//	{
			//		storageReadIsAllowed = await Permissions.RequestAsync<Permissions.StorageRead>();
			//	}
			//	else
			//	{
			//		storageReadIsAllowed = PermissionStatus.Granted;
			//	}


			//	if (storageWriteStatus == PermissionStatus.Denied)
			//	{
			//		storageWriteIsAllowed = await Permissions.RequestAsync<Permissions.StorageWrite>();
			//	}
			//	else
			//	{
			//		storageWriteIsAllowed = PermissionStatus.Granted;
			//	}


			//	if (await CheckStoragePermisions())
			//	{
			//		string url = ApiDetail.EndPoint + "api/Reports/GenerateEcard?memberId=" + SelectedDependant.Id;

			//		string NameOfFile = SelectedDependant.FullName + ".pdf";
			//		//try
			//		//{
			//		//    await Browser.OpenAsync(url + "/" + NameOfFile);

			//		//}
			//		//catch (Exception ex)
			//		//{

			//		//    SendErrorMessageToAppCenter(ex, "Policy Details");
			//		//}


			//		await DependencyService.Get<IDownload>().DownloadFileAsync(url, NameOfFile);

			//	}






			//}
			//catch (Exception ex)
			//{

			//	SendErrorMessageToAppCenter(ex, "ECard");
			//}
		}


		public async Task TakePhoto()
		{
			try
			{

				var photo = await MediaPicker.CapturePhotoAsync();

				// canceled
				if (photo == null)
				{
					PhotoPath = null;
					return;
				}



				var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
				using (var stream = await photo.OpenReadAsync())
				{
					using (var newStream = File.OpenWrite(newFile))
					{
						await stream.CopyToAsync(newStream);
					}

					FileBytesToBeUploaded = ConvertStramToByteArray(stream);
				}

				PhotoPath = newFile;
				SelectedFileName = photo.FileName;


			}
			catch (Exception ex)
			{

				SendErrorMessageToAppCenter(ex, "User Profile");
			}
		}

		public async Task PickPicture()
		{
			try
			{

				PhotoPath = null;

				var photo = await MediaPicker.PickPhotoAsync();

				// canceled
				if (photo == null)
				{
					PhotoPath = null;
					return;
				}



				var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
				using (var stream = await photo.OpenReadAsync())
				{
					using (var newStream = File.OpenWrite(newFile))
					{
						await stream.CopyToAsync(newStream);
					}

					FileBytesToBeUploaded = ConvertStramToByteArray(stream);
				}

				PhotoPath = newFile;
				SelectedFileName = photo.FileName;

			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "User Profile");
			}
		}

		//public async Task ShowUploadPopUp()
		//{
		//	try
		//	{
		//		await App.Current.MainPage.Navigation.PushPopupAsync(new SelectEcardPhotoType());
		//	}
		//	catch (Exception ex)
		//	{

		//		SendErrorMessageToAppCenter(ex, "E-Card View");
		//	}
		//}

		public async Task UploadImageToServer()
		{
			//try
			//{


			//	await ShowLoadingMessage();

			//	RestClient client = new RestClient(ApiDetail.EndPoint);

			//	RestRequest restRequest = new RestRequest()
			//	{
			//		Resource = "/api/Reports/SaveEcardPhotoForMember",
			//		Method = Method.Post
			//	};

			//	UpdateProfileImage updateProfileImage = new UpdateProfileImage()
			//	{
			//		memberId = SelectedDependant.Id,
			//		profileImgFile = FileBytesToBeUploaded.Length > 0 ? Base64.EncodeToString(FileBytesToBeUploaded, Base64Flags.NoWrap) : "",
			//		profileImgName = SelectedFileName
			//	};

			//	restRequest.AddJsonBody(updateProfileImage);

			//	var response = await client.ExecuteAsync(restRequest);

			//	if (response.IsSuccessful)
			//	{

			//		var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<bool>>(response.Content);

			//		if (deserializedResponse.success)
			//		{
			//			//await ShowSuccessMessage();

			//			await GetEcardPhotoFromServer();
			//		}
			//		else
			//		{
			//			await ShowErrorMessage();
			//		}

			//	}
			//	else
			//	{
			//		await ShowErrorMessage();
			//	}

			//}
			//catch (Exception ex)
			//{
			//	SendErrorMessageToAppCenter(ex, "E-Card");
			//	await ShowErrorMessage();
			//}
		}

		public async Task GetEcardPhotoFromServer()
		{
			//try
			//{


			//	await ShowLoadingMessage();

			//	RestClient client = new RestClient(ApiDetail.EndPoint);

			//	RestRequest restRequest = new RestRequest()
			//	{
			//		Resource = "/api/Reports/GetEcardPhoto",
			//		Method = Method.Get
			//	};

			//	restRequest.AddQueryParameter("memberId", SelectedDependant.Id);



			//	var response = await client.ExecuteAsync(restRequest);

			//	if (response.IsSuccessful)
			//	{
			//		isUploadVisible = false;
			//		var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<ECardPhotoDto>>(response.Content);

			//		if (deserializedResponse.success)
			//		{

			//			PhotoPath = deserializedResponse.data.PhotoUrl;
			//			IsUploadVisible = false;
			//		}
			//		else
			//		{

			//		}

			//	}

			//	await RemoveLoadingMessage();

			//}
			//catch (Exception ex)
			//{
			//	SendErrorMessageToAppCenter(ex, "E-Card");
			//	await ShowErrorMessage();
			//}
		}

	}
}
