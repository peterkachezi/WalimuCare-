using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalimuV2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuV2.Views.Downloads
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyDownloadCenterPage : ContentPage
	{
		public MyDownloadCenterPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			BindingContext = DependencyService.Get<DownloadsViewModel>();
		}
	}
}