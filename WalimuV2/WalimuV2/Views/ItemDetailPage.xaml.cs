using System.ComponentModel;
using WalimuV2.ViewModels;
using Xamarin.Forms;

namespace WalimuV2.Views
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