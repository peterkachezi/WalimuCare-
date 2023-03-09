using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalimuV2.CustomRenderer;
using WalimuV2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuV2.Views.TrackHospitalVisit
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HospitalVisitPage : CustomContentPageRenderer
	{
		public HospitalVisitPage()
		{
			InitializeComponent();

			BindingContext = DependencyService.Get<HospitalVisitViewModel>();

		}
		

		private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{

		}
	}
}