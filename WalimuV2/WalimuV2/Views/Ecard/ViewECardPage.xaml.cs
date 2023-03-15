using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalimuV2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuV2.Views.Ecard
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewECardPage : ContentPage
	{
		public ViewECardPage()
		{
			InitializeComponent();

            BindingContext = DependencyService.Get<ECardViewModel>();

        }
    }
}