using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalimuV2.CustomRenderer;
using WalimuV2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuV2.Views.hospitals
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