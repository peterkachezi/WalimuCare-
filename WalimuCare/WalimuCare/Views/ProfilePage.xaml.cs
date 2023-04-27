using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalimuCare.CustomRenderer;
using WalimuCare.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuCare.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : CustomContentPageRenderer
	{
		public ProfilePage()
		{
			InitializeComponent();


		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			try
			{
				BindingContext = DependencyService.Get<UserProfileViewModel>();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

			}
		}
	}
}