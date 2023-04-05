using Android.Webkit;
using Microsoft.AppCenter.Crashes;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WalimuV2.Models;
using WalimuV2.Services;
using WalimuV2.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace WalimuV2.ViewModels
{
    public class AppViewModel : INotifyPropertyChanged
    {
        MaterialSnackbarConfiguration snackbarConfiguration = new MaterialSnackbarConfiguration() { BackgroundColor = Color.FromHex("3FAC49"), MessageTextColor = Color.Black, Margin = 0, CornerRadius = 0 };
        public AppViewModel()
        {
            try
            {
                IsRefreshing = false;

                BackButtonCommand = new Command<string>(async x => await BackButton(x));

                RemovePopUpCommand = new Command(async () => await RemoveLoadingMessage());

                IsNavBarVisible = false;

                CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;

            }
            catch (Exception)
            {

            }
        }

        private async void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await ShowErrorMessage("Please switch on Mobile Data or Connect to a wifi");
                }
                else
                {
                    await RemoveLoadingMessage();
                }
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "App View model");

                await ShowErrorMessage();
            }
        }
        private bool isBusy { get; set; }
        public bool IsBusy
        {
            get { return isBusy; }

            set
            {
                isBusy = value;

                OnPropertyChanged(nameof(IsBusy));
            }
        }
        private bool enableSubmitBtn { get; set; }
        public bool EnableSubmitBtn
        {
            get { return enableSubmitBtn; }

            set
            {
                enableSubmitBtn = value;

                OnPropertyChanged(nameof(EnableSubmitBtn));
            }
        }

        private bool isRefreshing;

        public bool IsRefreshing
        {
            get { return isRefreshing; }

            set
            {
                isRefreshing = value;

                OnPropertyChanged(nameof(IsRefreshing));

                Console.WriteLine($"Is Refreshing {IsRefreshing}");
            }
        }

        private string pageTitle;
        public string PageTitle
        {
            get { return pageTitle; }
            set
            {
                pageTitle = value;

                OnPropertyChanged();
            }
        }


        private string pageSubTitle;
        public string PageSubTitle
        {
            get { return pageSubTitle; }
            set { pageSubTitle = value; }
        }
        private bool isEmptyIllustrationVisible;
        public bool IsEmptyIllustrationVisible
        {
            get { return isEmptyIllustrationVisible; }

            set
            {
                isEmptyIllustrationVisible = value;

                OnPropertyChanged();
            }
        }
        private string noDataAvailableMessage;
        public string NoDataAvailableMessage
        {
            get { return noDataAvailableMessage; }

            set
            {
                noDataAvailableMessage = value;

                OnPropertyChanged();
            }
        }

        private bool isListViewVisible;
        public bool IsListViewVisible
        {
            get { return isListViewVisible; }

            set { isListViewVisible = value; OnPropertyChanged(); }
        }

        private bool isNavBarVisible;
        public bool IsNavBarVisible
        {
            get { return isNavBarVisible; }
            set { isNavBarVisible = value; OnPropertyChanged(); }
        }
        private bool isCustomNavBrVisible;
        public bool IsCustomNavBarVisible
        {
            get { return isCustomNavBrVisible; }
            set { isCustomNavBrVisible = value; OnPropertyChanged(); }
        }
        public PermissionStatus storageReadIsAllowed { get; set; }
        public PermissionStatus storageWriteIsAllowed { get; set; }
        public ICommand BackButtonCommand { get; set; }
        public ICommand RemovePopUpCommand { get; set; }
        public async Task ShowErrorMessage(string Message = "")
        {
            try
            {
                await Application.Current.MainPage.Navigation.PopAllPopupAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                string msg = "Sorry something went wrong, please try again after sometime";

                if (Message != null && Message.Trim() != "")
                {
                    msg = Message;
                }
                await Application.Current.MainPage.Navigation.PushPopupAsync(new WalimuErrorPage(msg));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task ShowSuccessMessage(string Message = "")
        {
            try
            {
                await Application.Current.MainPage.Navigation.PopAllPopupAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

            try
            {
                string msg = "Successful";

                if (Message != null && Message.Trim() != "")
                {
                    msg = Message;
                }

                await Application.Current.MainPage.Navigation.PushPopupAsync(new WalimuSuccessPage(msg));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

        public async Task ShowLoadingMessage(string Message = "")
        {

            try
            {
                await Application.Current.MainPage.Navigation.PopAllPopupAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

            try
            {
                string msg = "Please wait";

                if (Message != null && Message.Trim() != "")
                {
                    msg = Message;
                }

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new WalimuLoaderPage(msg));

                    //await MaterialDialog.Instance.LoadingSnackbarAsync(msg, snackbarConfiguration);
                    //await MaterialDialog.Instance.LoadingDialogAsync(msg);

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

        public async Task ShowInfoMessage(string Message = "")
        {

            try
            {
                await Application.Current.MainPage.Navigation.PopAllPopupAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

            try
            {
                string msg = "This is some info";

                if (Message != null && Message.Trim() != "")
                {
                    msg = Message;
                }

                await Application.Current.MainPage.Navigation.PushPopupAsync(new WalimuInfoPage(msg));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }


        public async Task RemoveLoadingMessage()
        {
            try
            {
                if (App.Current.MainPage.Navigation.NavigationStack.Count > 0)
                {

                    await Application.Current.MainPage.Navigation.PopAllPopupAsync();

                    //MainThread.BeginInvokeOnMainThread(async () =>
                    //{
                    //	await App.Current.MainPage.Navigation.PopAllPopupAsync();
                    //});

                }

            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "App View Model", "", "");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string PropertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public void SendErrorMessageToAppCenter(Exception ex, string NameOfModule = "", string MemberNumber = "", string PhoneNumber = "")
        {
            MemberNumber = Preferences.Get("MemberNumber", "");
            PhoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), "");
            string ErrorMessage = ex.Message;

            var properties = new Dictionary<string, string>
                                    {
                                        { "NameOfModule", NameOfModule },
                                        { "MemberNumber", MemberNumber},
                                        { "PhoneNumber", PhoneNumber},
                                        { "ErrorMessage", ErrorMessage}

                                    };
            Crashes.TrackError(ex, properties);
        }

        public async Task<bool> CheckInternetConnectivity()
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    return true;
                }
                else
                {
                    await ShowErrorMessage("Sorry Please switch on your data or connect to wifi before proceeding");

                    return false;
                }
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Base View Model");
                return false;
            }
        }

        public async Task<bool> CheckIfApiDetailsAreSetUp()
        {
            try
            {

                if (ApiDetail.EndPoint == null || ApiDetail.EndPoint.Trim() == "")
                {
                    await ShowErrorMessage("Sorry Something is not right, please logout and login again");

                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Base View Model");
                return false;
            }
        }


        public async Task GoToHomePage()
        {
            try
            {


                //var count =  Shell.Current.Navigation.NavigationStack.Count;

                //if (count > 1)
                //{
                //    //await Shell.Current.Navigation.PopAsync();
                //}
                //else
                //{
                //    await Shell.Current.GoToAsync(nameof(ContactUsPage));
                //}

            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "App View Model");
            }
        }


        public async Task BackButton(string Route)
        {
            try
            {
                // await App.Current.MainPage.Navigation.PopAsync();

                //if (Route.ToLower().Contains("SubmitComplaintsPage".ToLower()))
                //{
                //    DependencyService.Get<SubmitComplaintsViewModel>().IsNavBarVisible = true;
                //    DependencyService.Get<SubmitComplaintsViewModel>().IsCustomNavBarVisible = false;
                //}

                //await Shell.Current.GoToAsync(Route);

                if (Route.Contains("HomePage"))
                {
                    await Shell.Current.GoToAsync(Route);
                }

                await Shell.Current.GoToAsync("..");


            }
            catch (Exception ex)
            {

                Route = Route.TrimStart('/');

                await Shell.Current.GoToAsync(Route);

                SendErrorMessageToAppCenter(ex, "App View Model");
            }
        }


        public static byte[] ConvertStramToByteArray(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }


        public async Task<bool> CheckStoragePermisions()
        {
            bool IsSavingFileAllowed = false;

            try
            {


                var storageReadStatus = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

                var storageWriteStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

                PermissionStatus storageReadIsAllowed = PermissionStatus.Denied;

                PermissionStatus storageWriteIsAllowed = PermissionStatus.Denied;


                if (storageReadStatus == PermissionStatus.Denied)
                {
                    storageReadIsAllowed = await Permissions.RequestAsync<Permissions.StorageRead>();
                }
                else
                {
                    storageReadIsAllowed = PermissionStatus.Granted;
                }


                if (storageWriteStatus == PermissionStatus.Denied)
                {
                    storageWriteIsAllowed = await Permissions.RequestAsync<Permissions.StorageWrite>();
                }
                else
                {
                    storageWriteIsAllowed = PermissionStatus.Granted;
                }


                if (storageWriteIsAllowed == PermissionStatus.Denied || storageReadIsAllowed == PermissionStatus.Denied)
                {

                    await ShowInfoMessage("Sorry you wont be able to download the file until you give us the permissions");
                }
                else
                {
                    IsSavingFileAllowed = true;
                }


                return IsSavingFileAllowed;


            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex);
                return IsSavingFileAllowed;
            }
        }



    }
}
