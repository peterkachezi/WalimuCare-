using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalimuCare.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuCare.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TheRealLoginPage : ContentPage
	{
		public TheRealLoginViewModel theRealLoginViewModel { get; set; }

		public TheRealLoginPage()
		{
			InitializeComponent();

			theRealLoginViewModel = DependencyService.Get<TheRealLoginViewModel>();		

			BindingContext = theRealLoginViewModel;
		}
	}
}