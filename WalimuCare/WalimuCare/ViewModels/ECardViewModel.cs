﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.IO;
using WalimuCare.Views.Ecard;
using WalimuCare.Services;
using WalimuCare.Models;
using System.Linq;
using WalimuCare.Interfaces;

namespace WalimuCare.ViewModels
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

        private readonly DependantService dependantService;

        private List<Dependant> dependants;
        public List<Dependant> Dependants
        {
            get { return dependants; }

            set { dependants = value; OnPropertyChanged(); }
        }
        private Dependant selectedDependant;
        public Dependant SelectedDependant
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
        //public ICommand DownloadECardCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand DownloadECardCommand
        {
            get
            {
                return new Command<string>(async (Id) => await DownloadECard(Id));
            }
        }
        public ICommand TakePhotoCommand { get; set; }
        public ICommand PickPictureCommand { get; set; }
        public ICommand ShowUploadPopUpCommand { get; set; }
        public ICommand UploadImageToServerCommand { get; set; }
        public ICommand ClosePopUpCommand { get; set; }
        public ECardViewModel()
        {

            RefreshCommand = new Command(async () => await GetDependants());

            dependantService = DependencyService.Get<DependantService>();

            GetDependantsCommand = new Command(async () => await GetDependants());

            ViewECardCommand = new Command<string>(async x => await ViewECard(x));

            //DownloadECardCommand = new Command(async () => await DownloadECard());

            TakePhotoCommand = new Command(async () => await TakePhoto());

            PickPictureCommand = new Command(async () => await PickPicture());

            ClosePopUpCommand = new Command(async () => await RemoveLoadingMessage());

            Task.Run(async () => await GetDependants());
        }

        public async Task GetDependants()
        {
            try
            {
                if (await CheckInternetConnectivity())
                {
                    await ShowLoadingMessage("Please wait as we fetch data..");

                    string MemberId = Preferences.Get(nameof(AspNetUsers.memberId), "");

                    var data = await dependantService.GetDependants();
                                    

                    if (data != null)
                    {
                        await RemoveLoadingMessage();

                        data.Insert(0, new Dependant()
                        {
                            PrincipalNumber = Preferences.Get("memberNumber", string.Empty),

                            Gender = Preferences.Get("gender", string.Empty),

                            Id = Preferences.Get("memberId", string.Empty),

                            JobGroup = Preferences.Get("jobGroup", string.Empty),

                            FirstName = Preferences.Get(nameof(AspNetUsers.firstName), ""),

                            LastName = Preferences.Get(nameof(AspNetUsers.lastName), ""),

                            DateOfBirth = Preferences.Get(nameof(AspNetUsers.DateOfBirth), DateTime.Now.AddYears(-100)),

                            //Id = Preferences.Get(nameof(AspNetUsers.id), ""),

                            Relation = "Self"

                        }); ;
                        IsRefreshing = false;

                        data = data.ToList();

                        Dependants = data;
                    }
                    else
                    {
                        Dependants = new List<Dependant>()
                        {
                            new Dependant()
                            {
                                FirstName = Preferences.Get(nameof(AspNetUsers.firstName), ""),

                                LastName = Preferences.Get(nameof(AspNetUsers.lastName), ""),

                                MiddleName = Preferences.Get(nameof(AspNetUsers.userName), ""),

                                Id = Preferences.Get(nameof(AspNetUsers.id), ""),

                                JobGroup = Preferences.Get(nameof(AspNetUsers.jobGroup),""),

                                Gender = Preferences.Get(nameof(AspNetUsers.Gender), ""),

                                DateOfBirth = Preferences.Get(nameof(AspNetUsers.DateOfBirth), DateTime.Now.AddYears(-100)),
                            }

                        };
                        IsRefreshing = false;

                    }
                    await RemoveLoadingMessage();
                }
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "E-Card");
            }
        }
        public async Task ViewECard(string Id)
        {
            try
            {
                SelectedDependant = dependants.Where(p => p.Id == Id).FirstOrDefault();

                PhotoPath = null;

                //await GetEcardPhotoFromServer();

                await Shell.Current.GoToAsync(nameof(ViewECardPage), true);
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "E Card");
            }
        }
        public async Task DownloadECard(string MemberId)
        {
            try
            {
                await Browser.OpenAsync("https://ecard.makl-psms.com/DownloadEcard/Download/?MemberId=" + MemberId + "", BrowserLaunchMode.SystemPreferred);

                //await Browser.OpenAsync("https://ecard.makl-psms.com/DownloadEcard/Download/?MemberId=8C804F42-A02D-44B1-9859-C052B0CC6319", BrowserLaunchMode.SystemPreferred);

                //var storageReadStatus = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

                //var storageWriteStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

                //PermissionStatus storageReadIsAllowed = PermissionStatus.Denied;

                //PermissionStatus storageWriteIsAllowed = PermissionStatus.Denied;

                //if (storageReadStatus == PermissionStatus.Denied)
                //{
                //    storageReadIsAllowed = await Permissions.RequestAsync<Permissions.StorageRead>();
                //}
                //else
                //{
                //    storageReadIsAllowed = PermissionStatus.Granted;
                //}

                //if (storageWriteStatus == PermissionStatus.Denied)
                //{
                //    storageWriteIsAllowed = await Permissions.RequestAsync<Permissions.StorageWrite>();
                //}
                //else
                //{
                //    storageWriteIsAllowed = PermissionStatus.Granted;
                //}
                //if (await CheckStoragePermisions())
                //{
                //    string url = ApiDetail.EndPoint + "api/Reports/GenerateEcard?memberId=" + SelectedDependant.Id;

                //    string NameOfFile = SelectedDependant.FullName + ".pdf";
                //    //try
                //    //{
                //    //    await Browser.OpenAsync(url + "/" + NameOfFile);

                //    //}
                //    //catch (Exception ex)
                //    //{

                //    //    SendErrorMessageToAppCenter(ex, "Policy Details");
                //    //}
                //    await DependencyService.Get<IDownload>().DownloadFileAsync(url, NameOfFile);
                //}
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "ECard");
            }
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
        //    try
        //    {
        //        await App.Current.MainPage.Navigation.PushPopupAsync(new SelectEcardPhotoType());
        //    }
        //    catch (Exception ex)
        //    {

        //        SendErrorMessageToAppCenter(ex, "E-Card View");
        //    }
        //}

        //public async Task UploadImageToServer()
        //{
        //    try
        //    {


        //        await ShowLoadingMessage();

        //        RestClient client = new RestClient(ApiDetail.EndPoint);

        //        RestRequest restRequest = new RestRequest()
        //        {
        //            Resource = "/api/Reports/SaveEcardPhotoForMember",
        //            Method = Method.Post
        //        };

        //        UpdateProfileImage updateProfileImage = new UpdateProfileImage()
        //        {
        //            memberId = SelectedDependant.Id,
        //            profileImgFile = FileBytesToBeUploaded.Length > 0 ? Base64.EncodeToString(FileBytesToBeUploaded, Base64Flags.NoWrap) : "",
        //            profileImgName = SelectedFileName
        //        };

        //        restRequest.AddJsonBody(updateProfileImage);

        //        var response = await client.ExecuteAsync(restRequest);

        //        if (response.IsSuccessful)
        //        {

        //            var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<bool>>(response.Content);

        //            if (deserializedResponse.success)
        //            {
        //                //await ShowSuccessMessage();

        //                await GetEcardPhotoFromServer();
        //            }
        //            else
        //            {
        //                await ShowErrorMessage();
        //            }

        //        }
        //        else
        //        {
        //            await ShowErrorMessage();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        SendErrorMessageToAppCenter(ex, "E-Card");
        //        await ShowErrorMessage();
        //    }
        //}

        //public async Task GetEcardPhotoFromServer()
        //{
        //    try
        //    {


        //        await ShowLoadingMessage();

        //        RestClient client = new RestClient(ApiDetail.EndPoint);

        //        RestRequest restRequest = new RestRequest()
        //        {
        //            Resource = "/api/Reports/GetEcardPhoto",
        //            Method = Method.Get
        //        };

        //        restRequest.AddQueryParameter("memberId", SelectedDependant.Id);



        //        var response = await client.ExecuteAsync(restRequest);

        //        if (response.IsSuccessful)
        //        {
        //            isUploadVisible = false;
        //            var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<ECardPhotoDto>>(response.Content);

        //            if (deserializedResponse.success)
        //            {

        //                PhotoPath = deserializedResponse.data.PhotoUrl;
        //                IsUploadVisible = false;
        //            }
        //            else
        //            {

        //            }

        //        }

        //        await RemoveLoadingMessage();

        //    }
        //    catch (Exception ex)
        //    {
        //        SendErrorMessageToAppCenter(ex, "E-Card");
        //        await ShowErrorMessage();
        //    }
        //}

              

    }
}