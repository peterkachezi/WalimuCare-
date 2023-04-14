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

                var client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync(ApiDetail.ApiUrl + "api/Dependants/MemberNo?MemberNo=" + memberNo + "");

                if (getData.IsSuccessStatusCode)
                {
                    IsRefreshing = true;

                    string results = getData.Content.ReadAsStringAsync().Result;

                    var getDependants = JsonConvert.DeserializeObject<List<Dependant>>(results);

                    return getDependants.Where(x => x.Status == 1).ToList();
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
