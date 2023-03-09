using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WalimuV2.ViewModels;
using WalimuV2.CustomRenderer;

namespace WalimuV2.Views.Policy
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PolicyLimitsPage : CustomContentPageRenderer
	{
		public PolicyLimitsPage()
		{
			InitializeComponent();
			BindingContext = DependencyService.Get<PolicyLimitViewModel>();

		}

		private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{

		}
	}
}