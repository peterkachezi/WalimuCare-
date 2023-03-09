using System;
using System.Collections.Generic;
using System.Text;
using static Android.Provider.MediaStore;
using System.Threading.Tasks;
using System.Windows.Input;
using WalimuV2.Models;
using WalimuV2.Services;
using Xamarin.Forms;
using Xamarin.Essentials;
using WalimuV2.Interfaces;

namespace WalimuV2.ViewModels
{
    class DownloadsViewModel : AppViewModel
	{


		public ICommand DownloadBrochureCommand { get; set; }
		public ICommand DownloadAllEcardsCommand { get; set; }

		public DownloadsViewModel()
		{
			DownloadBrochureCommand = new Command(async () => await DownloadBrochure());
			DownloadAllEcardsCommand = new Command(async () => await DownloadAllEcards());
		}


		public async Task DownloadBrochure()
		{
			try
			{

				//string url = ApiDetail.EndPoint + "api/Reports/GenerateEcard?memberId=" + SelectedDependant.Id;

				//string NameOfFile = SelectedDependant.FullName + ".pdf";

				//await DependencyService.Get<IDownload>().DownloadFile(url, NameOfFile);


				await Browser.OpenAsync("https://drive.google.com/u/0/uc?id=1RVwZgaTae5WuPA6UGI4FRxnSv0CEgOpU&export=download");

			}
			catch (Exception ex)
			{

				SendErrorMessageToAppCenter(ex, "Policy Details");
			}
		}

		public async Task DownloadAllEcards()
		{
			try
			{

				if (await CheckStoragePermisions())
				{
					string Url = ApiDetail.EndPoint + "/api/Reports/GenerateAllEcards?memberId=" + Preferences.Get(nameof(AspNetUsers.id), "");

					string NameOfFile = "AllEcards.pdf";

					await DependencyService.Get<IDownload>().DownloadFile(Url, NameOfFile);

				}

			}
			catch (Exception ex)
			{

				SendErrorMessageToAppCenter(ex, "Policy Details");
			}
		}
	}
}
