using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using WalimuV2.ApiResponses;
using WalimuV2.Services;
using WalimuV2.Views.Others;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WalimuV2.ViewModels
{
    public class SubmitComplaintsViewModel : AppViewModel
    {

        private List<Complaint> complaintsType;
        public List<Complaint> ComplaintsType
        {
            get { return complaintsType; }
            set { complaintsType = value; OnPropertyChanged(); }
        }


        private List<MemberComplaint> memberComplaints;
        public List<MemberComplaint> MemberComplaints
        {
            get { return memberComplaints; }

            set { memberComplaints = value; OnPropertyChanged(); }
        }


        private string complaintDescription;
        public string ComplaintDescription
        {
            get { return complaintDescription; }

            set { complaintDescription = value; OnPropertyChanged(); }
        }

        private string complaintCategory;
        public string ComplaintCategory
        {
            get { return complaintCategory; }

            set { complaintCategory = value; OnPropertyChanged(); }
        }

        private string hospitalName;
        public string HospitalName
        {
            get { return hospitalName; }

            set { hospitalName = value; OnPropertyChanged(); }
        }

        private string hospitalDepartment;
        public string HospitalDepartment
        {
            get { return hospitalDepartment; }

            set { hospitalDepartment = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ComplaintsSelectList> selectionList;
        public ObservableCollection<ComplaintsSelectList> SelectionList
        {
            get { return selectionList; }

            set { selectionList = value; }
        }
        public bool isMemberDetails;
        public bool IsMemberDetails
        {
            get { return isMemberDetails; }

            set { isMemberDetails = value; OnPropertyChanged(); }
        }
        public bool isDependents;
        public bool IsDependents
        {
            get { return isDependents; }

            set { isDependents = value; OnPropertyChanged(); }
        }
        public bool isPreAuth;
        public bool IsPreAuth
        {
            get { return isPreAuth; }

            set { isPreAuth = value; OnPropertyChanged(); }
        }
        public bool isOtpRequest;
        public bool IsOtpRequest
        {
            get { return isOtpRequest; }

            set { isOtpRequest = value; OnPropertyChanged(); }
        }
        public bool isPoorService;
        public bool IsPoorService
        {
            get { return isPoorService; }

            set { isPoorService = value; OnPropertyChanged(); }
        }

        private bool lessApproval;
        public bool LessApproval
        {
            get { return lessApproval; }

            set { lessApproval = value; OnPropertyChanged(); }
        }

        private bool rejected;
        public bool Rejected
        {

            get { return rejected; }
            set { rejected = value; OnPropertyChanged(); }
        }

        private bool delayedOTP;
        public bool DelayedOTP
        {
            get { return delayedOTP; }

            set { delayedOTP = value; OnPropertyChanged(); }
        }
        private bool invalidOTP;
        public bool InvalidOTP
        {
            get { return invalidOTP; }

            set { invalidOTP = value; OnPropertyChanged(); }
        }

        private bool fraudulentOTP;
        public bool FraudulentOTP
        {
            get { return fraudulentOTP; }

            set { fraudulentOTP = value; OnPropertyChanged(); }
        }

        private bool daughter;
        public bool Daughter
        {
            get { return daughter; }

            set { daughter = value; OnPropertyChanged(); }
        }

        private bool son;
        public bool Son
        {
            get { return son; }

            set { son = value; OnPropertyChanged(); }
        }

        private bool spouse;
        public bool Spouse
        {
            get { return spouse; }

            set { spouse = value; OnPropertyChanged(); }
        }

        private bool incorrectDetails;
        public bool IncorrectDetails
        {
            get { return incorrectDetails; }

            set { incorrectDetails = value; OnPropertyChanged(); }
        }
        private bool incorrectJob;
        public bool IncorrectJob
        {

            get { return incorrectJob; }

            set { incorrectJob = value; OnPropertyChanged(); }
        }
        private bool incorrectGender;
        public bool IncorrectGender
        {
            get { return incorrectGender; }

            set { incorrectGender = value; OnPropertyChanged(); }
        }
        private bool memberUpdate;
        public bool MemberUpdate
        {

            get { return memberUpdate; }

            set { memberUpdate = value; OnPropertyChanged(); }
        }

        private Complaint selectedComplaintType;
        public Complaint SelectedComplaintType
        {
            get { return selectedComplaintType; }
            set
            {
                selectedComplaintType = value; OnPropertyChanged(); ShowHideCat();
            }
        }

        private string otpCategory;
        public string OtpCategory
        {
            get { return otpCategory; }

            set { otpCategory = value; OnPropertyChanged(); }
        }
        private string preAuthCategory;
        public string PreAuthCategory
        {
            get { return preAuthCategory; }

            set { preAuthCategory = value; OnPropertyChanged(); }
        }
        private string dependentCategory;
        public string DependentCategory
        {
            get { return dependentCategory; }

            set { dependentCategory = value; OnPropertyChanged(); }
        }
        private string memberCategory;
        public string MemberCategory
        {
            get { return memberCategory; }

            set { memberCategory = value; OnPropertyChanged(); }
        }
        private string dependentName;
        public string DependentName
        {
            get { return dependentName; }

            set { dependentName = value; OnPropertyChanged(); }
        }

        public ICommand SubmitComplaintCommand { get; set; }

        public ICommand GetMemberComplaintsCommand { get; set; }

        public ICommand AddNewComplaintCommand { get; set; }

        public ICommand GoToMyComplaintsCommand { get; set; }

        public SubmitComplaintsViewModel()
        {
            IsOtpRequest = false;

            IsPoorService = false;

            IsPreAuth = false;

            IsDependents = false;

            IsMemberDetails = false;

            IsCustomNavBarVisible = true;

            SubmitComplaintCommand = new Command(async () => await SubmitComplaint());

            GetMemberComplaintsCommand = new Command(async () => await GetMemberComplaints());

            AddNewComplaintCommand = new Command(async () => await AddNewComplaint());

            GoToMyComplaintsCommand = new Command(async () => await GoToMyComplaints());

            Task.Run(async () =>
            {
                //await GetMemberComplaints();
                await GetComplaintsTypes();
            });
        }

        public async Task GetComplaintsTypes()
        {
            try
            {

                ComplaintsType = new List<Complaint>();

                SelectionList = new ObservableCollection<ComplaintsSelectList>();

                await ShowLoadingMessage();

                RestClient client = new RestClient(ApiDetail.EndPoint);

                RestRequest restRequest = new RestRequest()
                {
                    Resource = "/Complaints/GetComplaintTopics",

                    Method = Method.Get
                };

                var response = await client.ExecuteAsync(restRequest);

                if (response.IsSuccessful)
                {
                    var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<List<Complaint>>>(response.Content);

                    if (deserializedResponse.success)
                    {
                        ComplaintsType = deserializedResponse.data;

                        foreach (var item in ComplaintsType)
                        {
                            ComplaintsSelectList complaintsSelectList = new ComplaintsSelectList();

                            complaintsSelectList.id = item.id;

                            complaintsSelectList.topic = item.topic;

                            SelectionList.Add(complaintsSelectList);
                        }

                        await RemoveLoadingMessage();
                    }
                    else
                    {
                        await ShowErrorMessage("Sorry, no Complaint types were found");
                    }
                }
                else
                {
                    await ShowErrorMessage();
                }
            }
            catch (Exception ex)
            {
                await ShowErrorMessage();

                SendErrorMessageToAppCenter(ex, "Submit Complaints");
            }
        }
        public void ShowHideCat()
        {
            switch (SelectedComplaintType.topic)
            {
                case "Wrong Member details":
                    IsMemberDetails = true;
                    IsOtpRequest = false;
                    IsPoorService = false;
                    IsPreAuth = false;
                    IsDependents = false;
                    ComplaintCategory = MemberCategory;
                    break;

                case "Preauth Complaints":
                    IsPreAuth = true;
                    IsOtpRequest = false;
                    IsPoorService = false;
                    IsDependents = false;
                    IsMemberDetails = false;
                    ComplaintCategory = PreAuthCategory;
                    break;
                case "Dependent missing":
                    IsDependents = true;
                    IsOtpRequest = false;
                    IsPoorService = false;
                    IsPreAuth = false;
                    IsMemberDetails = false;
                    ComplaintCategory = DependentCategory;
                    break;
                case "Poor services":
                    IsPoorService = true;
                    IsOtpRequest = false;
                    IsPreAuth = false;
                    IsDependents = false;
                    IsMemberDetails = false;
                    ComplaintCategory = "";
                    break;
                case "OTP Requests":
                    IsOtpRequest = true;
                    IsPoorService = false;
                    IsPreAuth = false;
                    IsDependents = false;
                    IsMemberDetails = false;
                    ComplaintCategory = OtpCategory;
                    break;
                default:
                    IsOtpRequest = false;
                    IsPoorService = false;
                    IsPreAuth = false;
                    IsDependents = false;
                    IsMemberDetails = false;
                    ComplaintCategory = "";
                    break;
            }

        }
        public void SetComplaintCategory()
        {
            if (Rejected)
            {
                ComplaintCategory = "Rejected";
            }
            else if (LessApproval)
            {
                ComplaintCategory = "Less Approval";
            }
            else if (DelayedOTP)
            {
                ComplaintCategory = "Delayed OTP";
            }
            else if (InvalidOTP)
            {
                ComplaintCategory = "Invalid OTP";
            }
            else if (FraudulentOTP)
            {
                ComplaintCategory = "Fraudulent OTP";
            }
            else if (Daughter)
            {
                ComplaintCategory = "Daughter";
            }
            else if (Son)
            {
                ComplaintCategory = "Son";
            }
            else if (Spouse)
            {
                ComplaintCategory = "Spouse";
            }
            else if (IncorrectDetails)
            {
                ComplaintCategory = "Incorrect Name";
            }
            else if (IncorrectJob)
            {
                ComplaintCategory = "Incorrect Job";
            }
            else if (IncorrectGender)
            {
                ComplaintCategory = "Incorrect Gender";
            }
            else if (MemberUpdate)
            {
                ComplaintCategory = "Member Update";
            }
        }
        public async Task SubmitComplaint()
        {
            SetComplaintCategory();
            try
            {
                if (SelectedComplaintType == null)
                {
                    await ShowErrorMessage("Please Select one Complaint");

                    return;
                }

                if (ComplaintDescription == null || (ComplaintDescription != null && ComplaintDescription.Trim() == ""))
                {
                    await ShowErrorMessage("Please Add Some Description to your complaint");

                    return;
                }

                await ShowLoadingMessage();

                var complaint = new
                {
                    Topic = SelectedComplaintType.topic,

                    MemberNumber = Preferences.Get("memberNumber", string.Empty),

                    FirstName = Preferences.Get("firstName", string.Empty),

                    LastName = Preferences.Get("lastName", string.Empty),

                    PhoneNumber = Preferences.Get("phoneNumber", string.Empty),

                    description = ComplaintDescription,

                    hospitalName = HospitalName,

                    hospitalDepartment = HospitalDepartment,

                    complaintType = ComplaintCategory,

                    dependentName = DependentName,
                  
                };

                var json = JsonConvert.SerializeObject(complaint);

                HttpContent httpContent = new StringContent(json);

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var client = new HttpClient();

                var response = await client.PostAsync(ApiDetail.ApiUrl + "api/Complaints/SubmitComplaint", httpContent);


                if (response.IsSuccessStatusCode)
                {
                    await ShowSuccessMessage("Your Issue has been Submitted successfully kindly await a call in 30 minutes");

                    ComplaintDescription = "";

                    selectedComplaintType = null;

                    await Task.Delay(2000);

                    await GetMemberComplaints();

                    await Shell.Current.GoToAsync(nameof(ComplaintsPage));
                }
                else
                {
                    await ShowErrorMessage();
                }
            }
            catch (Exception ex)
            {
                await ShowErrorMessage();

                SendErrorMessageToAppCenter(ex, "Submit Complaints");
            }
        }

        public async Task GetMemberComplaints()
        {
            try
            {
                IsListViewVisible = true;

                IsEmptyIllustrationVisible = false;

                NoDataAvailableMessage = "";

                IsRefreshing = true;

                await ShowLoadingMessage();

                var memberNo = Preferences.Get("memberNumber", string.Empty);

                var client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync(ApiDetail.ApiUrl + "api/Complaints/GetComplaints?MemberNumber=" + memberNo + "");

                if (getData.IsSuccessStatusCode)
                {

                    string results = getData.Content.ReadAsStringAsync().Result;

                    var deserializedResponse = JsonConvert.DeserializeObject<List<MemberComplaint>>(results);

                    if (deserializedResponse.Count > 0)
                    {
                        MemberComplaints = deserializedResponse.ToList();

                        await RemoveLoadingMessage();

                        IsListViewVisible = true;

                        IsEmptyIllustrationVisible = false;

                        NoDataAvailableMessage = "";

                        IsRefreshing = false;
                    }

                    if (deserializedResponse.Count == 0)
                    {
                        IsListViewVisible = false;

                        IsEmptyIllustrationVisible = true;

                        NoDataAvailableMessage = "Sorry you dont have any complaints lodged";

                        IsRefreshing = false;
                    }

                    //else
                    //{
                    //    await RemoveLoadingMessage();

                    //    IsListViewVisible = false;

                    //    IsEmptyIllustrationVisible = true;

                    //    NoDataAvailableMessage = "Something went wrong, Please try again";

                    //    IsRefreshing = false;
                    //}

                }
                else
                {
                    await RemoveLoadingMessage();

                    IsListViewVisible = false;

                    IsEmptyIllustrationVisible = true;

                    NoDataAvailableMessage = "Something went wrong, Please try again";

                    IsRefreshing = false;
                }

            }
            catch (Exception ex)
            {
                await ShowErrorMessage();

                SendErrorMessageToAppCenter(ex, "Submit Complaints");

                IsListViewVisible = false;

                IsEmptyIllustrationVisible = true;

                NoDataAvailableMessage = "Something went wrong, Please try again";

                IsRefreshing = false;
            }
        }


        public async Task AddNewComplaint()
        {
            try
            {

                await GetComplaintsTypes();

                await Shell.Current.GoToAsync(nameof(SubmitComplaintsPage));
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Submit Complaint");
            }
        }

        public async Task GoToMyComplaints()
        {
            try
            {

                await GetMemberComplaints();


                await Shell.Current.GoToAsync(nameof(ComplaintsPage));
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Submit Complaints");
            }
        }
    }
}
