using WalimuV2.CustomRenderer;
using WalimuV2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuV2.Views.Dependants
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DependantPage : CustomContentPageRenderer
	{
		public DependantPage ()
		{
			InitializeComponent ();

			BindingContext = DependencyService.Get<DependantsViewModel>();
		}

		private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{

		}
	}
}