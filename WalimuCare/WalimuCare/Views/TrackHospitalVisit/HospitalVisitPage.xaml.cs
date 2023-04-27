using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalimuCare.CustomRenderer;
using WalimuCare.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuCare.Views.TrackHospitalVisit
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