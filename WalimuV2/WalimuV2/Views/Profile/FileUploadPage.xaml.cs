using Android.Media;
using Java.Time.Temporal;
using System;
using System.IO;
using System.Net.Http;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Stream = System.IO.Stream;

namespace WalimuV2.Views.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FileUploadPage : ContentPage
    {
        public FileUploadPage()
        {
            InitializeComponent();
        }
        private async void FileUpload_Clicked(object sender, EventArgs e)
        {
            try
            {
                var file = await MediaPicker.PickPhotoAsync();

                if (file == null)
                {
                    return;
                }

                if (file != null)
                {
                    var content = new MultipartFormDataContent
                {
                    { new StreamContent(await file.OpenReadAsync()), "file", file.FileName }
                };

                    var httClient = new HttpClient();

                    string Url = "https://c4ba-154-70-3-144.eu.ngrok.io/api/MemberAuth/Upload";

                    httClient.BaseAddress = new Uri(Url);

                    var response = await httClient.PostAsync("", content);

                    if (response.IsSuccessStatusCode)
                    {
                        lblStatus.Text = "File uploaded successfully";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }
        }

        private async void GetFileClick(object sender, EventArgs e)
        {
            try
            {
                string imageName = "1811_800.jpg";

                var httClient = new HttpClient();

                string Url = "https://c4ba-154-70-3-144.eu.ngrok.io/api/MemberAuth/GetImage?Name=1811_800.jpg";

                httClient.BaseAddress = new Uri(Url);

                var response = await httClient.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;

                    var byteArray = Convert.FromBase64String(content);

                    Stream stream = new MemoryStream(byteArray);

                    var imageSource = ImageSource.FromStream(() => stream);

                    img.Source = imageSource;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }
        }
    }
}