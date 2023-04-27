using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WalimuCare.ViewModels;
using WalimuCare.CustomRenderer;

namespace WalimuCare.Views.Policy
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