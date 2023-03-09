using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalimuV2.CustomRenderer;
using WalimuV2.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuV2.Views.Policy
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PolicyDetailsPage : CustomContentPageRenderer
	{
		public PolicyDetailsPage ()
		{
			InitializeComponent ();
			BindingContext = DependencyService.Get<PolicyDetailsViewModel>();

		}

		private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			try
			{
				//lstMain.ScrollTo(e.SelectedItem, ScrollToPosition.Center, false);
			}
			catch (Exception)
			{

			}
		}

		private async void Button_Clicked(object sender, EventArgs ega)
		{
			try
			{	

				await Browser.OpenAsync("https://drive.google.com/u/0/uc?id=1RVwZgaTae5WuPA6UGI4FRxnSv0CEgOpU&export=download");
			}
			catch (Exception ex)
			{
				DependencyService.Get<PolicyDetailsViewModel>().SendErrorMessageToAppCenter(ex, "Policy Details");
			}
		}
	}
}