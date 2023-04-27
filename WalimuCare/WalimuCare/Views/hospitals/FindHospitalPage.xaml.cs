using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalimuCare.CustomRenderer;
using WalimuCare.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuCare.Views.hospitals
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FindHospitalPage : CustomContentPageRenderer
	{
		public FindHospitalPage ()
		{
			InitializeComponent ();

			BindingContext = DependencyService.Get<FindHospitalViewModel>();

		}
	}
}