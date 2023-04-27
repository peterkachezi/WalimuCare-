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
	public partial class ConfirmOtpResetPage : ContentPage
	{
		public ConfirmOtpResetPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			BindingContext = DependencyService.Get<ConfirmOtpViewModelReset>();
		}
	}
}