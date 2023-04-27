using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalimuCare.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuCare.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExitPage : PopupPage
	{
		public ExitPage()
		{
			InitializeComponent();
		}

		private async void btnCancel_Clicked(object sender, EventArgs e)
		{
			try
			{
				await DependencyService.Get<AppViewModel>().RemoveLoadingMessage();
			}
			catch (Exception ex)
			{

				DependencyService.Get<AppViewModel>().SendErrorMessageToAppCenter(ex, "Exit Page");
			}
		}

		private void btnCloseApp_Clicked(object sender, EventArgs e)
		{
			try
			{
				System.Diagnostics.Process.GetCurrentProcess().Kill();
			}
			catch (Exception ex)
			{

				DependencyService.Get<AppViewModel>().SendErrorMessageToAppCenter(ex, "Exit Page");
			}
		}
	}
}