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
	public partial class ConfirmMemberDetails : ContentPage
	{
		public ConfirmMemberDetails()
		{
			InitializeComponent();

			BindingContext = DependencyService.Get<ConfirmMemberDetailsViewModel>();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			//btnContinue.IsEnabled = false;
		}

	}
}