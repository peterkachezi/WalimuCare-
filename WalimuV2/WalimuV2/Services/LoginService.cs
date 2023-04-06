

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using WalimuV2.Models;
using Xamarin.Essentials;
using System;
using Xamarin.Forms.GoogleMaps;
using System.Text;
using System.Net.NetworkInformation;
using WalimuV2.ApiResponses;
using Android.App;

namespace WalimuV2.Services
{
    public static class LoginService
    {      

        public static async Task<bool> SubmitLogin(string CurrentPin)
        {
            try
            {
                var memberNo = "";

                var savedMemberNo = Preferences.Get("memberNumber", string.Empty);

                memberNo = savedMemberNo;
              
                var login = new Login()
                {
                    MemberNumber = memberNo,

                    Password = CurrentPin,
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

                    if (result.accountStatus == "1")
                    {
                        return true;
                    }

                    if (result.accountStatus == "Wrong Password")
                    {
                        return false;
                    }

                    if (result.accountStatus == "Account not available")
                    {
                        return false;
                    }
                }

                return false;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

                return false;
            }
        }
    }
}
