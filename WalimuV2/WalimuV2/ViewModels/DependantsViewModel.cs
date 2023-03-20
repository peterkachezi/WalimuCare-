using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WalimuV2.Models;
using WalimuV2.Services;
using WalimuV2.Views;
using Xamarin.Forms;
using Xamarin.Essentials;
using WalimuV2.Views.Dependants;
using static Android.Provider.ContactsContract.CommonDataKinds;
using WalimuV2.Views.PopUps;
using System.IO;

namespace WalimuV2.ViewModels
{
    public class DependantsViewModel : AppViewModel
    {
        private ObservableCollection<Dependant> dependant;
        public ObservableCollection<Dependant> Dependant
        {
            get { return dependant; }
            set { dependant = value; OnPropertyChanged(nameof(Dependant)); }
        }

        private string memberNo;
        public string MemberNo
        {
            get { return memberNo; }
            set { memberNo = value; OnPropertyChanged(nameof(MemberNo)); }
        }
        public string PhoneNumber { get; set; }
        public string JobGroup { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public string Relationship { get; set; }
        public int Age { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand ViewDependantDetailsCommand { get; set; }
        public ICommand SelectOptionsCommand { get; set; }
        public ICommand TakePhotoCommand { get; set; }
        public ICommand PickPictureCommand { get; set; }
        public ICommand RemovePhotoCommand { get; set; }
        public ICommand ViewProfileCommand
        {
            get
            {
                return new Command<string>(async (Id) => await ShowDependantDetails(Id));
            }
        }
        public void sayHello(string x)
        {
            //handle parameter x to say "Hello " + x
        }
        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; OnPropertyChanged(nameof(IsActive)); }
        }

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

        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = value;

                OnPropertyChanged(nameof(FullName));
            }
        }

        private string imageUrl;

        public string ImageUrl
        {
            get { return imageUrl; }
            set
            {
                imageUrl = value;
                OnPropertyChanged(nameof(ImageUrl));
            }
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

        public DependantsViewModel()
        {
            try
            {
                RefreshCommand = new Command(async () => await GetDependant());

                SelectOptionsCommand = new Command(async () => await SelectOptions());

                PickPictureCommand = new Command(async () => await PickPicture());

                TakePhotoCommand = new Command(async () => await TakePhoto());

                RemovePhotoCommand = new Command(RemovePhoto);

                PageTitle = "Dependant's Details";

                PageSubTitle = "Upload Dependant Passport Photo";

                Task.Run(async () =>
                {
                    await GetDependant();
                });
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Find Hospital", MemberNo, PhoneNumber);

                Console.WriteLine(ex);
            }
        }
        public async Task TakePhoto()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                //var photo = await MediaPicker.PickPhotoAsync();
                // canceled
                if (photo == null)
                {
                    PhotoPath = null;
                    return;
                }
                long FileSize = 0;

                // save the file into local storage
                var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                {
                    await stream.CopyToAsync(newStream);

                    FileSize = stream.Length;
                }


                if (FileSize > 5000000)
                {
                    await ShowInfoMessage("File should be less than 5 mbz");
                    return;
                }

                Preferences.Set("ProfilePhoto", newFile);

                PhotoPath = newFile;

                //DependencyService.Get<HomePageViewModel>().GetProfilePicture();

                DependencyService.Get<AppShellViewModel>().GetProfilePicture();

                await RemoveLoadingMessage();
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "User Profile", memberNo, PhoneNumber);
            }
        }
        public async Task PickPicture()
        {
            try
            {
                //var photo = await MediaPicker.CapturePhotoAsync();
                var photo = await MediaPicker.PickPhotoAsync();
                // canceled
                if (photo == null)
                {
                    PhotoPath = null;

                    return;
                }

                long FileSize = 0;

                // save the file into local storage
                var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using (var stream = await photo.OpenReadAsync())

                using (var newStream = File.OpenWrite(newFile))
                {
                    await stream.CopyToAsync(newStream);

                    FileSize = stream.Length;
                }

                if (FileSize > 5000000)
                {
                    await ShowInfoMessage("File should be less than 5 mbz");
                    return;
                }

                Preferences.Set("ProfilePhoto", newFile);

                PhotoPath = newFile;

                //DependencyService.Get<HomePageViewModel>().GetProfilePicture();


                DependencyService.Get<AppShellViewModel>().GetProfilePicture();

                await RemoveLoadingMessage();
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "User Profile");
            }
        }
        public async void RemovePhoto()
        {
            try
            {
                Preferences.Set("ProfilePhoto", "avator.png");

                PhotoPath = "avator.png";

                DependencyService.Get<DependantsViewModel>().GetProfilePicture();

                await RemoveLoadingMessage();
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "User Profile");
            }
        }
        public void GetProfilePicture()
        {
            try
            {
                string theImageUrl = Preferences.Get("ProfilePhoto", "avator.png");

                ImageUrl = theImageUrl;
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "App Shell");
            }
        }
        public async Task SelectOptions()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new SelectMediaPage());
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "User Profile");
            }
        }
        public async Task GetDependant()
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

                        HttpResponseMessage getData = await client.GetAsync(ApiDetail.ApiUrl + "api/Dependants/MemberNo?MemberNo=" + memberNo + "");

                        if (getData.IsSuccessStatusCode)
                        {
                            string results = getData.Content.ReadAsStringAsync().Result;

                            var getDependants = JsonConvert.DeserializeObject<List<Dependant>>(results);

                            var claimsObservableCollections = new ObservableCollection<Dependant>();

                            foreach (var item in getDependants)
                            {
                                claimsObservableCollections.Add(item);
                            }

                            Dependant = claimsObservableCollections;

                            IsRefreshing = false;

                            IsActive = false;

                            await RemoveLoadingMessage();
                        }

                        if (getData.IsSuccessStatusCode == false)
                        {
                            IsEmptyIllustrationVisible = true;

                            NoDataAvailableMessage = "You do not have any dependant";

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

        public async Task ShowDependantDetails(string Id)
        {
            try
            {
                await ShowLoadingMessage("Please wait as we sign you in");

                var client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync(ApiDetail.ApiUrl + "api/Dependants/" + Id + "");

                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;

                    var getDependants = JsonConvert.DeserializeObject<Dependant>(results);

                    FullName = getDependants.FullName;

                    PhoneNumber = getDependants.PhoneNumber;

                    JobGroup = getDependants.JobGroup;

                    DateOfBirth = getDependants.DateOfBirth.ToString("dddd, dd MMMM yyyy");

                    Gender = getDependants.Gender;

                    MemberNo = getDependants.MemberNumber;

                    Relationship = getDependants.Relation;

                    DateTime dob = getDependants.DateOfBirth;

                    int age = DateTime.Now.Subtract(dob).Days;

                    age = (age / 365);

                    Age = age;

                    var s = getDependants.Status;

                    if (s == 0)
                    {
                        Status = "Awaiting Approval";
                    }
                    else if (s == 1)
                    {
                        Status = "Active";
                    }

                    else if (s == 2)
                    {
                        Status = "Suspended";
                    }

                    else if (s == 3)
                    {
                        Status = "Deleted";
                    }
                    else
                    {
                        Status = "Deleted";
                    }
                    
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        Shell.Current.FlyoutIsPresented = false;

                        await Shell.Current.GoToAsync(nameof(DependantDetailPage));
                    });

                    await RemoveLoadingMessage();
                }

                if (getData.IsSuccessStatusCode == false)
                {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new WalimuErrorPage("Something is not right, please log out then Login again"));
                }
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "App View Model", "", "");
            }
        }

    }
}
