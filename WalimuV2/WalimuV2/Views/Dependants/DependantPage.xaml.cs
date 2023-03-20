using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using WalimuV2.CustomRenderer;
using WalimuV2.Models;
using WalimuV2.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuV2.Views.Dependants
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DependantPage : CustomContentPageRenderer
    {
        public DependantPage()
        {
            InitializeComponent();

            BindingContext = DependencyService.Get<DependantsViewModel>();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

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
        //private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var dependantId = ((TappedEventArgs)e).Parameter;

        //        var client = new HttpClient();

        //        client.DefaultRequestHeaders.Accept.Clear();

        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        HttpResponseMessage getData = await client.GetAsync(ApiDetail.ApiUrl + "api/Dependants/" + dependantId + "");

        //        if (getData.IsSuccessStatusCode)
        //        {
        //            string results = getData.Content.ReadAsStringAsync().Result;

        //            var getDependants = JsonConvert.DeserializeObject<Dependant>(results);

        //            FullName = getDependants.FullName;

        //            //IsRefreshing = false;

        //            //IsActive = false;
        //        }

        //        Shell.Current.FlyoutIsPresented = false;
        //        await Shell.Current.GoToAsync(nameof(DependantDetailPage));
        //    }
        //    catch (Exception ex)
        //    {
        //        SendErrorMessageToAppCenter(ex, "App View Model", "", "");
        //        throw;
        //    }
        //}

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
    }
}