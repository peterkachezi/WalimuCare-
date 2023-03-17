using WalimuV2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuV2.Views.Others
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SubmitComplaintsPage : ContentPage
	{
		public SubmitComplaintsPage ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = DependencyService.Get<SubmitComplaintsViewModel>();
        }
        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}