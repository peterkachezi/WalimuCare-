using WalimuCare.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuCare.Views.Others
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CallBacksPage : ContentPage
    {
        public CallBacksPage()
        {
            InitializeComponent();

            BindingContext = DependencyService.Get<RequestCallBackViewModel>();

        }
        
    }
}