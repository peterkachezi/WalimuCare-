using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalimuV2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuV2.Views
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