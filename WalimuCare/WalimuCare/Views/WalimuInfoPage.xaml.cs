using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace WalimuCare.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WalimuInfoPage : PopupPage
	{
		public string MessageText { get; set; } = "The operation was successful";
		public WalimuInfoPage(string messageText)
		{
			InitializeComponent();

			if (messageText != null || messageText != "")
			{
				MessageText = messageText;
			}
		}
		protected override void OnAppearing()
		{
			lblMessage.Text = MessageText;
		}
	}
}