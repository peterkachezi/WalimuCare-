using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace WalimuV2.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GettingStartedPage : ContentPage
	{
		public GettingStartedPage()
		{
			InitializeComponent();

			MyCorousels = new List<MyCorousel>()
			{
				 new MyCorousel()
				{
					 ImageUrl = "claimspreauth.png",
					Title="Track Hospital Visits",
					SubTitle="Track your preauth status, policy utilization and hospital visits",
					PreviousBtnText = "Skip",
					NextBtnText = "Next",
					lblPreviousCommand = new Command<int>((x)=>SetCurrentItem(x)),
					lblNextCommand = new Command<int>((x)=>SetCurrentItem(x)),
					PreviousPosition = 0,
					NextPosition = 1,
					BackgroundColor= "#C4007696"

				},
				  new MyCorousel()
				{   ImageUrl = "medicalcover.png",
					Title="Buy Medical Cover",
					SubTitle="Buy medical cover for extra dependents not covered in your current scheme",
					PreviousBtnText = "Back",
					NextBtnText = "Next",
					lblPreviousCommand = new Command<int>((x)=>SetCurrentItem(x)),
					lblNextCommand = new Command<int>((x)=>SetCurrentItem(x)),
					PreviousPosition = 1,
					NextPosition = 2,
					BackgroundColor= "#C4293872"
				},
				   new MyCorousel()
				{
						 ImageUrl = "telemedicineNew.png",
					Title="Video Telemedicine",
					SubTitle="Video consult or chat with top doctors online from the comfort of your phone",
					PreviousBtnText = "Back",
					NextBtnText = "Get Started",
					lblPreviousCommand = new Command<int>((x)=>SetCurrentItem(x)),
					lblNextCommand = new Command<int>((x)=>SetCurrentItem(x)),
					PreviousPosition = 1,
					NextPosition = 4,
					BackgroundColor= "#C44c501f"
				}

			};

			BindingContext = this;
		}
		public List<MyCorousel> MyCorousels { get; set; }
		public async void SetCurrentItem(int position)
		{
			try
			{
				if (position == 0)
				{
					App.Current.MainPage = new NavigationPage(new LoginPageTwo());
				}
				else if (position == 4)
				{
					App.Current.MainPage = new NavigationPage(new LoginPageTwo());
				}
				else
				{

					if (corouselView.Position == 1 && position == 1)
					{
						corouselView.Position = 0;
					}
					else
					{
						corouselView.Position = position;
					}


				}

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				await App.Current.MainPage.Navigation.PushPopupAsync(new WalimuErrorPage("Sorry something went wrong"), true);
			}
		}
	}
	public class MyCorousel
	{
		public string ImageUrl { get; set; }
		public string Title { get; set; }
		public string SubTitle { get; set; }
		public string PreviousBtnText { get; set; }
		public string NextBtnText { get; set; }
		public ICommand lblNextCommand { get; set; }
		public ICommand lblPreviousCommand { get; set; }
		public int PreviousPosition { get; set; }
		public int NextPosition { get; set; }
		public string BackgroundColor { get; set; }
	}
}