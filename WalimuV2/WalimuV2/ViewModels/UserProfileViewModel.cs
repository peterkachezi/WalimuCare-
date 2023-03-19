using Newtonsoft.Json;
using RestSharp;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WalimuV2.ApiResponses;
using WalimuV2.Models;
using WalimuV2.Services;
using WalimuV2.Views;
using WalimuV2.Views.PopUps;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WalimuV2.ViewModels
{
    public class UserProfileViewModel : AppViewModel
    {

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
        private string idNumber;
        public string IdNumber
        {
            get { return idNumber; }
            set
            {
                idNumber = value;
                OnPropertyChanged(nameof(IdNumber));
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

        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        private string schemeName;
        public string SchemeName
        {
            get { return schemeName; }
            set
            {
                schemeName = value;
                OnPropertyChanged(nameof(SchemeName));
            }
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

        private string newPhoneNumber;
        public string NewPhoneNumber
        {
            get { return newPhoneNumber; }
            set { newPhoneNumber = value; OnPropertyChanged(nameof(NewPhoneNumber)); CheckIfPhoneNumberIsCorrect(); }
        }

        private string schoolName;

        public string SchoolName
        {
            get { return schoolName; }
            set { schoolName = value; OnPropertyChanged(nameof(SchoolName)); }
        }

        private string stationName;

        public string StationName
        {
            get { return stationName; }
            set { stationName = value; OnPropertyChanged(nameof(StationName)); }
        }

        //public string NHIF { get; private set; }
        //public string County { get; private set; }

        private string nhifNo;

        public string NhifNo
        {
            get { return nhifNo; }
            set { nhifNo = value; OnPropertyChanged(nameof(NhifNo)); }
        }
        private string countyOfResidence;

        public string CountyOfResidence
        {
            get { return countyOfResidence; }
            set { countyOfResidence = value; OnPropertyChanged(nameof(CountyOfResidence)); }
        }

        private string postalAddress;

        public string PostalAddress
        {
            get { return postalAddress; }
            set { postalAddress = value; OnPropertyChanged(nameof(PostalAddress)); }
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


        private string gender;
        public string Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }
        private Guid memberId;
        public Guid MemberId
        {
            get { return memberId; }
            set
            {
                memberId = value;
                OnPropertyChanged(nameof(MemberId));
            }
        }

        public ICommand ShowChangePhoneNumberCommand { get; set; }
        public ICommand TakePhotoCommand { get; set; }
        public ICommand ReturnToHomePageCommand { get; set; }
        public ICommand UpdateNewPhoneNumberCommand { get; set; }
        public ICommand UpdateNewEmailCommand { get; set; }
        public ICommand ShowChangePinPageCommand { get; set; }
        public ICommand PickPictureCommand { get; set; }
        public ICommand SelectOptionsCommand { get; set; }
        public ICommand RemovePhotoCommand { get; set; }
        public ICommand ClosePopUpCommand { get; set; }
        public ICommand ShowChangeEmailCommand { get; set; }
        public ICommand UpdateChangeEmailCommand { get; set; }
        public ICommand ShowChangeDoBCommand { get; set; }
        public ICommand UpdateChangeDoBCommand { get; set; }
        public ICommand ShowChangeStationCommand { get; set; }
        public ICommand UpdateChangeStationCommand { get; set; }
        public ICommand ShowChangeCountyCommand { get; set; }
        public ICommand UpdateChangeCountyCommand { get; set; }
        public ICommand ShowChangePostalCommand { get; set; }
        public ICommand UpdateChangePostalCommand { get; set; }
        public ICommand UpdateNewStationCommand { get; set; }
        public ICommand UpdateNewCountyCommand { get; set; }
        public ICommand UpdateNewDoBCommand { get; set; }
        public ICommand UpdateNewPostalCommand { get; set; }
        public ICommand ShowChangeIdNumberCommand { get; set; }
        public ICommand UpdateNewIDCommand { get; set; }
        public ICommand UpdateNewNHIFCommand { get; set; }
        public ICommand ShowChangeNhifCommand { get; set; }
        public ICommand ShowChangeSchoolNameCommand { get; set; }
        public ICommand UpdateNewSchoolCommand { get; set; }
        public UserProfileViewModel()
        {
            try
            {
                SetData();
                //ShowChangeSchoolNameCommand = new Command(async () => await ShowChangeSchoolNamePage());
                //ShowChangeIdNumberCommand = new Command(async () => ShowChangeIdNumberPage());
                //ShowChangePhoneNumberCommand = new Command(async () => await ShowChangePhoneNumberPage());
                TakePhotoCommand = new Command(async () => await TakePhoto());
                ReturnToHomePageCommand = new Command(ReturnToHomePage);
                UpdateNewPhoneNumberCommand = new Command(async () => await UpdateNewPhoneNumber());
                UpdateNewEmailCommand = new Command(async () => await UpdateNewEmail());
                UpdateNewStationCommand = new Command(async () => await UpdateNewStation());
                UpdateNewSchoolCommand = new Command(async () => await UpdateNewSchool());
                UpdateNewCountyCommand = new Command(async () => await UpdateNewCounty());
                UpdateNewDoBCommand = new Command(async () => await UpdateNewDoB());
                UpdateNewPostalCommand = new Command(async () => await UpdateNewPostal());
                UpdateNewIDCommand = new Command(async () => await UpdateNewIdNumber());
                UpdateNewNHIFCommand = new Command(async () => await UpdateNewNhifNumber());
                ShowChangePinPageCommand = new Command(ShowChangePinPage);
                SelectOptionsCommand = new Command(async () => await SelectOptions());
                RemovePhotoCommand = new Command(RemovePhoto);
                PickPictureCommand = new Command(async () => await PickPicture());
                ClosePopUpCommand = new Command(async () => await ClosePopUp());
                EnableSubmitBtn = false;
                //ShowChangeEmailCommand = new Command(async () => await ShowChangeEmailPage());
                //ShowChangeDoBCommand = new Command(async () => await ShowChangeDoBPage());
                //ShowChangeStationCommand = new Command(async () => await ShowChangeStationPage());
                //ShowChangeCountyCommand = new Command(async () => await ShowChangeCountyPage());
                //ShowChangePostalCommand = new Command(async () => await ShowChangePostalPage());

                PageTitle = "User Profile";
                PageSubTitle = "View and Edit your details";
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "User Profile", "", "");

            }
        }
        //public void ShowChangeIdNumberPage()
        //{
        //    try
        //    {
        //        //await App.Current.MainPage.Navigation.PushAsync(new ChangePhoneNumber());
        //        //await Shell.Current.GoToAsync(nameof(ChangeIdNumber));
        //    }
        //    catch (Exception ex)
        //    {
        //        SendErrorMessageToAppCenter(ex, "User Profile", "", "");
        //    }
        //}
        //public async Task ShowChangeSchoolNamePage()
        //{
        //	try
        //	{
        //		//await App.Current.MainPage.Navigation.PushAsync(new ChangePhoneNumber());
        //		await Shell.Current.GoToAsync(nameof(ChangeSchoolName));
        //	}
        //	catch (Exception ex)
        //	{
        //		SendErrorMessageToAppCenter(ex, "User Profile", "", "");
        //	}
        //}


        //public async Task ShowChangePhoneNumberPage()
        //{
        //	try
        //	{
        //		//await App.Current.MainPage.Navigation.PushAsync(new ChangePhoneNumber());
        //		await Shell.Current.GoToAsync(nameof(ChangePhoneNumber));
        //	}
        //	catch (Exception ex)
        //	{
        //		SendErrorMessageToAppCenter(ex, "User Profile", "", "");
        //	}
        //}
        //public async Task ShowChangeEmailPage()
        //{
        //	try
        //	{
        //		await Shell.Current.GoToAsync(nameof(ChangeEmail));
        //	}
        //	catch (Exception ex)
        //	{
        //		SendErrorMessageToAppCenter(ex, "User Profile", "", "");
        //	}
        //}
        //public async Task ShowChangeDoBPage()
        //{
        //	try
        //	{
        //		await Shell.Current.GoToAsync(nameof(ChangeDoB));
        //	}
        //	catch (Exception ex)
        //	{
        //		SendErrorMessageToAppCenter(ex, "User Profile", "", "");
        //	}
        //}
        //public async Task ShowChangeStationPage()
        //{
        //	try
        //	{
        //		await Shell.Current.GoToAsync(nameof(ChangeStation));
        //	}
        //	catch (Exception ex)
        //	{
        //		SendErrorMessageToAppCenter(ex, "User Profile", "", "");
        //	}
        //}
        //public async Task ShowChangeCountyPage()
        //{
        //	try
        //	{
        //		await Shell.Current.GoToAsync(nameof(ChangeCounty));
        //	}
        //	catch (Exception ex)
        //	{
        //		SendErrorMessageToAppCenter(ex, "User Profile", "", "");
        //	}
        //}
        //public async Task ShowChangePostalPage()
        //{
        //	try
        //	{
        //		await Shell.Current.GoToAsync(nameof(ChangePostal));
        //	}
        //	catch (Exception ex)
        //	{
        //		SendErrorMessageToAppCenter(ex, "User Profile", "", "");
        //	}
        //}
        public void SetData()
        {
            try
            {
                PageTitle = "Profile";

                string firstName = Preferences.Get("firstName", string.Empty);

                string lastName = Preferences.Get("lastName", string.Empty);

                FullName = firstName + " " + lastName;

                PhoneNumber = Preferences.Get("phoneNumber", string.Empty);

                MemberNumber = Preferences.Get("memberNumber", string.Empty);

                Gender = Preferences.Get("gender", string.Empty);

                dateOfBirth = Preferences.Get("dateOfBirth", string.Empty);

                jobGroup = Preferences.Get("jobGroup", string.Empty);

                // string theDateOfBirth = Preferences.Get("dateofbirth", "");

                //int theGender = Preferences.Get("gender", 3);

                //DateOfBirth = theDateOfBirth;

                //if (theGender != 3)
                //{
                //	Gender = theGender == 1 ? "Male" : "Female";
                //}

                //DateTime usersBirthYear = new DateTime();

                //var changeIsSuccessful = DateTime.TryParse(theDateOfBirth, out usersBirthYear);

                //if (changeIsSuccessful)
                //{
                //	DateOfBirth = usersBirthYear.ToString("dd-MMM-yyyy");
                //}
                //else
                //{
                //	DateOfBirth = theDateOfBirth;

                //}
                //await GetSchemeNameAndJobGroup();

                PhotoPath = Preferences.Get("ProfilePhoto", "avator");
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "User Profile", MemberNumber, PhoneNumber);
            }
        }

        //public async Task GetSchemeNameAndJobGroup()
        //{
        //	try
        //	{
        //		await ShowLoadingMessage();

        //		RestClient client = new RestClient(ApiDetail.EndPoint);

        //		RestRequest restRequest = new RestRequest()
        //		{
        //			Method = Method.Post,

        //			Resource = "/Members/GetSchemeNameAndJobGroup"
        //		};

        //		restRequest.AddQueryParameter("MemberNumber", MemberNumber);

        //		var response = await Task.Run(() =>
        //		{
        //			return client.Execute(restRequest);
        //		});

        //		await RemoveLoadingMessage();

        //		if (response.IsSuccessful)
        //		{
        //			var result = JsonConvert.DeserializeObject<BaseResponse<SchemeAndJobGroup>>(response.Content);

        //			if (result.success)
        //			{

        //				JobGroup = result.data.jobGroup;
        //				SchemeName = result.data.schemeName;
        //			}
        //		}
        //	}
        //	catch (Exception ex)
        //	{

        //		SendErrorMessageToAppCenter(ex, "User Profile", MemberNumber, PhoneNumber);
        //	}
        //}

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

                SendErrorMessageToAppCenter(ex, "User Profile", MemberNumber, PhoneNumber);
            }
        }

        public void ReturnToHomePage()
        {
            try
            {
                Application.Current.MainPage = new AppShell();
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "User Profile", MemberNumber, PhoneNumber);
            }
        }
        //UpdateNewEmailCommand()
        public async Task UpdateNewSchool()
        {
            MemberDetailsUpdate memberChange = new MemberDetailsUpdate()
            {
                MemberId = MemberId,
                Field = "School",
                Change = SchoolName
            };
            await UpdateMember(memberChange);
        }
        public async Task UpdateNewIdNumber()
        {
            MemberDetailsUpdate memberChange = new MemberDetailsUpdate()
            {
                MemberId = MemberId,
                Field = "ID",
                Change = IdNumber
            };
            await UpdateMember(memberChange);
        }
        public async Task UpdateNewNhifNumber()
        {
            MemberDetailsUpdate memberChange = new MemberDetailsUpdate()
            {
                MemberId = MemberId,
                Field = "NHIF",
                Change = nhifNo
            };
            await UpdateMember(memberChange);
        }
        public async Task UpdateNewPostal()
        {
            MemberDetailsUpdate memberChange = new MemberDetailsUpdate()
            {
                MemberId = MemberId,
                Field = "Postal",
                Change = PostalAddress
            };
            await UpdateMember(memberChange);
        }
        public async Task UpdateNewDoB()
        {
            MemberDetailsUpdate memberChange = new MemberDetailsUpdate()
            {
                MemberId = MemberId,
                Field = "Dob",
                Change = DateOfBirth
            };
            await UpdateMember(memberChange);

        }
        public async Task UpdateNewCounty()
        {
            MemberDetailsUpdate memberChange = new MemberDetailsUpdate()
            {
                MemberId = MemberId,
                Field = "County",
                Change = CountyOfResidence
            };
            await UpdateMember(memberChange);

        }
        public async Task UpdateNewStation()
        {
            MemberDetailsUpdate memberChange = new MemberDetailsUpdate()
            {
                MemberId = MemberId,
                Field = "Station",
                Change = StationName
            };
            await UpdateMember(memberChange);
        }
        public async Task UpdateNewEmail()
        {
            MemberDetailsUpdate memberChange = new MemberDetailsUpdate()
            {
                MemberId = MemberId,
                Field = "Email",
                Change = Email
            };
            await UpdateMember(memberChange);
        }
        public async Task UpdateNewPhoneNumber()
        {
            MemberDetailsUpdate memberChange = new MemberDetailsUpdate()
            {
                MemberId = MemberId,
                Field = "Phone",
                Change = PhoneNumber
            };
            await UpdateMember(memberChange);
        }

        public async Task UpdateMember(MemberDetailsUpdate memberChange)
        {

            try
            {
                await ShowLoadingMessage();
                RestClient client = new RestClient(ApiDetail.EndPoint);

                RestRequest restRequest = new RestRequest()
                {
                    Method = Method.Post,
                    Resource = "/Members/UpdateMemberDetails"
                };
                MemberDetailsUpdate member = new MemberDetailsUpdate()
                {
                    MemberId = memberChange.MemberId,
                    Field = memberChange.Field,
                    Change = memberChange.Change
                };

                restRequest.AddJsonBody(member);


                var response = await Task.Run(() =>
                {
                    return client.Execute(restRequest);
                });

                await Task.Delay(5000);

                await RemoveLoadingMessage();

                await ShowSuccessMessage(member.Field + "Update Request Sent Successfully");

                await App.Current.MainPage.Navigation.PopAsync();

                await RemoveLoadingMessage();

                if (response.IsSuccessful)
                {
                    var result = JsonConvert.DeserializeObject<BaseResponse<bool>>(response.Content);

                    if (result.success)
                    {

                        _ = result.data;
                        await ShowSuccessMessage(member.Field + "Updated Successfully");
                    }
                    else
                    {
                        await ShowSuccessMessage(member.Field + "Update failed");
                    }
                }
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Change" + memberChange.Field + "Failed");
            }
        }

        private void CheckIfPhoneNumberIsCorrect()
        {
            try
            {
                if (NewPhoneNumber != null && NewPhoneNumber.Length >= 10)
                {
                    EnableSubmitBtn = true;
                }
                else
                {
                    EnableSubmitBtn = false;
                }
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Change Phone Number", MemberNumber, PhoneNumber);
            }
        }

        private void ShowChangePinPage()
        {
            try
            {
                Application.Current.MainPage = new NavigationPage(new SetPinPage(true));
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "User Profile", MemberNumber, PhoneNumber);
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

        public async void RemovePhoto()
        {
            try
            {
                Preferences.Set("ProfilePhoto", "avator.png");

                PhotoPath = "avator.png";

                //DependencyService.Get<HomePageViewModel>().GetProfilePicture();
                DependencyService.Get<AppShellViewModel>().GetProfilePicture();
                await RemoveLoadingMessage();
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "User Profile");
            }
        }

        public async Task ClosePopUp()
        {
            try
            {
                await RemoveLoadingMessage();
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "User Profile");
            }
        }

    }

}
