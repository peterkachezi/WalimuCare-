using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalimuV2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuV2.Views
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