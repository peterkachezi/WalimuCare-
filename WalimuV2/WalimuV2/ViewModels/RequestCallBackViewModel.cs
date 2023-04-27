using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WalimuV2.ApiResponses;
using WalimuV2.Models;
using WalimuV2.Services;
using WalimuV2.Views.Others;
using Xamarin.Essentials;
using Xamarin.Forms;
using Java.Lang.Annotation;
using System.Collections.ObjectModel;
using WalimuV2.Views.Dependants;
using Xamarin.Forms.Internals;
using WalimuV2.Views.Policy;
using Android.Content.Res;
using Rg.Plugins.Popup.Extensions;
using WalimuV2.Views.PopUps;
using WalimuV2.Views.hospitals;
using WalimuV2.Views;

namespace WalimuV2.ViewModels
{
    public class RequestCallBackViewModel : AppViewModel
    {
        public string MemberId { get; set; }
        //public string checkController { get; set; }

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

        private string remarks;

        public string Remarks
        {
            get { return remarks; }
            set
            {
                remarks = value;
                OnPropertyChanged(nameof(Remarks));
            }
        }

        private List<CallBackrequests> callBackrequests;

        public List<CallBackrequests> CallBackrequests
        {
            get { return callBackrequests; }
            set { callBackrequests = value; OnPropertyChanged(); }
        }



        //private string selectedRdoBtn;

        //public string SelectedRdoBtn
        //{
        //	get { return selectedRdoBtn; }
        //	set
        //	{


        //		selectedRdoBtn = value;
        //		OnPropertyChanged(nameof(SelectedRdoBtn));
        //		rdBtnChecked();
        //	}
        //}

        private bool isPhnNoEditorEnabled;

        public bool IsPhnNoEditorEnabled
        {
            get { return isPhnNoEditorEnabled; }
            set
            {
                isPhnNoEditorEnabled = value;
                OnPropertyChanged(nameof(IsPhnNoEditorEnabled));
            }
        }

        public int MyProperty { get; set; }

        public ICommand GetMyNumber { get; set; }
        public ICommand GetOtherNumber { get; set; }
        public ICommand GetMemberRequestsCommand { get; set; }
        public ICommand SubmitCallBackRequest { get; set; }
        public ICommand viewCallBackLstCommand { get; set; }
        public ICommand AddNewRequestCommand { get; set; }

        public RequestCallBackViewModel()
        {
            try
            {
                SubmitCallBackRequest = new Command(async () => await SaveCallBackRequests());

                viewCallBackLstCommand = new Command(async () => await GoToLists());

                GetMemberRequestsCommand = new Command(async () => await GetCallBackrequests());

                AddNewRequestCommand = new Command(async () => await AddNewCallBackRequest());

                PhoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), "");

                PageTitle = "Request Call";

                Task.Run(async () =>
                {
                    await GetCallBackrequests();
                });

            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Request A call back", "acha jokes", PhoneNumber);
            }
        }

        public async Task AddNewCallBackRequest()
        {
            try
            {
                //await Shell.Current.GoToAsync(nameof(RequestCallBack));
                await Shell.Current.Navigation.PushAsync(new RequestCallBack());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                SendErrorMessageToAppCenter(ex, "Request Call Back");
            }
        }

        public async Task GetCallBackrequests()
        {
            try
            {
                var phoneNumber = Preferences.Get("phoneNumber", string.Empty);

                var MemberNo = Preferences.Get("memberNumber", string.Empty);

                IsListViewVisible = true;

                IsEmptyIllustrationVisible = false;

                NoDataAvailableMessage = "";

                IsRefreshing = true;

                CallBackrequests = new List<CallBackrequests>();

                var client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync(ApiDetail.ApiUrl + "api/CallBack/GetCallBacks?MemberNumber=" + MemberNo + "&PhoneNumber=" + phoneNumber + "");

                if (getData.IsSuccessStatusCode)
                {

                    string results = getData.Content.ReadAsStringAsync().Result;

                    var deserializedResponse = JsonConvert.DeserializeObject<List<CallBackrequests>>(results);

                    CallBackrequests = deserializedResponse;

                    if (CallBackrequests.Count > 0)
                    {
                        IsListViewVisible = true;

                        IsEmptyIllustrationVisible = false;

                        NoDataAvailableMessage = "";

                        IsRefreshing = false;
                    }
                    else
                    {
                        IsListViewVisible = false;

                        IsEmptyIllustrationVisible = true;

                        NoDataAvailableMessage = "Sorry you dont have any call requests lodged";

                        IsRefreshing = false;
                    }
                }
                //if (getData.IsSuccessStatusCode == false)
                //{

                //    IsListViewVisible = false;

                //    IsEmptyIllustrationVisible = true;

                //    NoDataAvailableMessage = "Something went wrong, Please try again";

                //    IsRefreshing = false;
                //}
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Request Call Back");

                IsListViewVisible = false;

                IsEmptyIllustrationVisible = true;

                NoDataAvailableMessage = "Something went wrong, Please try again";

                IsRefreshing = false;
            }
        }

        public async Task SaveCallBackRequests()
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
                        var firstName = Preferences.Get("firstName", string.Empty);

                        var lastName = Preferences.Get("lastName", string.Empty);

                        var MemberNo = Preferences.Get("memberNumber", string.Empty);

                        var model = new CallBackrequests
                        {
                            PhoneNumber = phoneNumber,

                            Remarks = remarks,

                            MemberName = firstName + " " + lastName,

                            MemberNumber = MemberNo
                        };

                        await ShowLoadingMessage("Submitting request ,please wait..");

                        var json = JsonConvert.SerializeObject(model);

                        HttpContent httpContent = new StringContent(json);

                        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        var client = new HttpClient();

                        var response = await client.PostAsync(ApiDetail.ApiUrl + "api/CallBack/SubmitRequest", httpContent);

                        try
                        {
                            if (response.IsSuccessStatusCode)
                            {                              

                                await GetCallBackrequests();

                                await ShowSuccessMessage("Call Back request Submitted successfuly");

                                //MainPage = new NavigationPage(new TheRealLoginPage());

                                await Application.Current.MainPage.Navigation.PopAsync().ConfigureAwait(false); //Remove the page currently on top.
                               // App.Current.MainPage = new NavigationPage(new AppShell());

                            }
                        }
                        catch (Exception e)
                        {
                            await ShowErrorMessage();

                            SendErrorMessageToAppCenter(e, "Request CallBack ", MemberId, PhoneNumber);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                await ShowErrorMessage();

                SendErrorMessageToAppCenter(ex, "Request Call back", MemberId, PhoneNumber);
            }
        }
        public async Task GoToLists()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(CallBackList), true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                SendErrorMessageToAppCenter(ex, "Request A call back", "Error", PhoneNumber);
            }
        }


    }
}
