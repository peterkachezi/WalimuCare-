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
	public partial class CallForAssistancePage : PopupPage
	{
		public CallForAssistancePage()
		{
			InitializeComponent();

			var bindingContext = DependencyService.Get<ConfirmMemberDetailsViewModel>();

			BindingContext = bindingContext;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();


		}

	}
}