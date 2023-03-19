using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalimuV2.CustomRenderer;
using WalimuV2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuV2.Views.Dependants
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DependantDetailPage : CustomContentPageRenderer
    {
        public DependantDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                BindingContext = DependencyService.Get<DependantsViewModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }
    }
}