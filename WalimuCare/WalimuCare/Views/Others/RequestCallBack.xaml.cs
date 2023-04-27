using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalimuCare.CustomRenderer;
using WalimuCare.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuCare.Views.Others
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RequestCallBack : CustomContentPageRenderer
	{
		public RequestCallBack()
		{
			InitializeComponent();

            BindingContext = DependencyService.Get<RequestCallBackViewModel>();

        }

   

	}
}