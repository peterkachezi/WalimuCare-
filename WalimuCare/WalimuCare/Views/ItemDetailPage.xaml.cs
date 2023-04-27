using System.ComponentModel;
using WalimuCare.ViewModels;
using Xamarin.Forms;

namespace WalimuCare.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}