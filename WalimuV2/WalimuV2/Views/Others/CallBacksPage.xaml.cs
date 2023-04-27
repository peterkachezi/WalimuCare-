using WalimuV2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuV2.Views.Others
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