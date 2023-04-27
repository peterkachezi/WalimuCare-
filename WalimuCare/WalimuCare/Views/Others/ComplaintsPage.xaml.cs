using WalimuCare.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuCare.Views.Others
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ComplaintsPage : ContentPage
    {
        public ComplaintsPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = DependencyService.Get<SubmitComplaintsViewModel>();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}