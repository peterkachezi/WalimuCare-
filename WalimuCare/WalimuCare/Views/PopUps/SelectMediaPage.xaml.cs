using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalimuCare.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuCare.Views.PopUps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class SelectMediaPage : PopupPage
    {
        public SelectMediaPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = DependencyService.Get<UserProfileViewModel>();

        }
    }

}