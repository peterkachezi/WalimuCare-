using Java.Lang.Reflect;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WalimuV2.ApiResponses;
using WalimuV2.ViewModels;
using Method = RestSharp.Method;
using WalimuV2.Models;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;

namespace WalimuV2.Services
{
    public class DependantService : AppViewModel
    {

        public async Task<List<Dependant>> GetDependants()
        {
            try
            {
                var memberNo = Preferences.Get("memberNumber", string.Empty);

                //RestClient client = new RestClient(ApiDetail.ur);
                ////{
                ////    BaseUrl = new Uri()
                ////};

                //RestRequest restRequest = new RestRequest()
                //{
                //    Method = Method.Post,

                //    Resource = "/Members/GetMemberDepedants"
                //};

                //restRequest.AddQueryParameter("PrincipalMbrId", MemberId);


                //var response = await Task.Run(() =>
                //{
                //    return client.Execute(restRequest);
                //});

                var client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync(ApiDetail.ApiUrl + "api/Dependants/MemberNo?MemberNo=" + memberNo + "");

                if (getData.IsSuccessStatusCode)
                {

                    //IsRefreshing = false;

                    string results = getData.Content.ReadAsStringAsync().Result;

                    var getDependants = JsonConvert.DeserializeObject<List<Dependant>>(results);

                    return getDependants.ToList();
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Dependant Service");
                return null;
            }
        }

    }
}
