using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalimuV2.CustomRenderer;
using WalimuV2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuV2.Views.Others
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RequestCallBack : CustomContentPageRenderer
	{
		public RequestCallBack()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			BindingContext = DependencyService.Get<RequestCallBackViewModel>();
		}

	}
}