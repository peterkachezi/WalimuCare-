using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WalimuCare.Interfaces
{
	public interface IDownload
	{
		Task DownloadFile(string Url, string NameOfFile);
		Task<bool> DownloadFileAsync(string url, string NameOfFile);
		event EventHandler<DownloadEventArgs> OnFileDownloaded;

	}

	public class DownloadEventArgs : EventArgs
	{
		public bool FileSaved = false;
		public DownloadEventArgs(bool fileSaved)
		{
			FileSaved = fileSaved;
		}
		//private async Task DownloadApkAsync()
		//{
		//    var downloadedFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "myApp_v1.2.3.apk");

		//    var success = await DownloadFileAsync("https://www.myapp.com/apks/v1.2.3.apk", downloadedFilePath);

		//    if (success)
		//    {
		//        Console.WriteLine($"File downloaded to: {downloadedFilePath}");
		//    }
		//    else
		//    {
		//        Console.WriteLine("Download failed");
		//    }
		//}


	}
}
