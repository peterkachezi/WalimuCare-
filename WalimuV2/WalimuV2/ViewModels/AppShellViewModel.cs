

using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WalimuV2.Extensions;
using WalimuV2.Models;
using WalimuV2.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WalimuV2.ViewModels
{
    public class AppShellViewModel : AppViewModel
    {

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        private string fullName;
        public string FullName
        {
            get { return FirstName.ToTitleCase() + " " + LastName.ToTitleCase(); }
            set
            {
                fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }
        public ICommand ViewProfileCommand { get; set; }
        public AppShellViewModel()
        {
            FirstName = Preferences.Get("firstName", string.Empty);
            LastName = Preferences.Get("lastName", string.Empty);
            Username = Preferences.Get("userName", string.Empty);

            GetProfilePicture();

            ViewProfileCommand = new Command(async () => await ShowUserProfile());
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
        public async Task ShowUserProfile()
        {
            try
            {
                Shell.Current.FlyoutIsPresented = false;
                //App.Current.MainPage = new NavigationPage(new ProfilePage());
                await Shell.Current.GoToAsync(nameof(ProfilePage));

            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "App View Model", "", "");
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


















    }
}
