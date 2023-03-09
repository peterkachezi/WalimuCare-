using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Android.Media.MediaDrm;

namespace WalimuV2.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WalimuLoaderPage : PopupPage
	{
		public string MessageText { get; set; }

		public string fileName { get; set; }

		private string loaderHtml;

		public string LoaderHtml
		{
			get { return loaderHtml; }
			set
			{
				loaderHtml = value;
				OnPropertyChanged();
			}
		}
		public WalimuLoaderPage(string messageText)
		{
			try
			{
				InitializeComponent();

				MessageText = messageText;

				BindingContext = this;

			}
			catch (Exception ex)
			{

				InitializeComponent();

				Debug.WriteLine(ex);
			}


		}

		protected async override void OnAppearing()
		{
			try
			{
				lblMessage.Text = MessageText;

				string text = "<!DOCTYPE html><html lang='en'> <head> <meta charset='utf-8'><style>.beatingHeart{width: 50px; margin: 50px auto; height: 50px;}.beatingHeart .heart{position: absolute; width: 50px; height: 45px; opacity: 0.6; -webkit-animation: beat 3.0s infinite ease-in-out;}.heart:before, .heart:after{position: absolute; content: ''; left: 25px; top: 0; width: 25px; height: 40px; background: #FF69B4; -moz-border-radius: 50px 50px 0 0; border-radius: 50px 50px 0 0; -webkit-transform: rotate(-45deg); -moz-transform: rotate(-45deg); -ms-transform: rotate(-45deg); -o-transform: rotate(-45deg); transform: rotate(-45deg); -webkit-transform-origin: 0 100%; -moz-transform-origin: 0 100%; -ms-transform-origin: 0 100%; -o-transform-origin: 0 100%; transform-origin: 0 100%;}.fushiaHeart:before, .fushiaHeart:after{background: #ff1d8e;}.pinkHeart:before, .pinkHeart:after{background: #FF0080;}.heart:after{left: 0; -webkit-transform: rotate(45deg); -moz-transform: rotate(45deg); -ms-transform: rotate(45deg); -o-transform: rotate(45deg); transform: rotate(45deg); -webkit-transform-origin: 100% 100%; -moz-transform-origin: 100% 100%; -ms-transform-origin: 100% 100%; -o-transform-origin: 100% 100%; transform-origin: 100% 100%;}.beatingHeart .heart2{-webkit-animation-delay: -1.0s; animation-delay: -1.0s;}.beatingHeart .heart3{-webkit-animation-delay: -1.5s; animation-delay: -1.5s;}.beatingHeart .heart4{-webkit-animation-delay: -2.0s; animation-delay: -2.0s;}.beatingHeart .heart5{-webkit-animation-delay: -2.5s; animation-delay: -2.5s;}@-webkit-keyframes beat{0%, 100%{-webkit-transform: scale(0.0)}50%{-webkit-transform: scale(1.0)}}/*spinner */.spinner{/*margin: 50px auto 0;*/ width: 100%; text-align: center;}.spinner .heart{width: 50px; height: 45px; display: inline-block; opacity: 0.8; -webkit-animation: bouncedelay 1.4s infinite ease-in-out; animation: bouncedelay 1.4s infinite ease-in-out; /* Prevent first frame from flickering when animation starts */ -webkit-animation-fill-mode: both; animation-fill-mode: both;}.heart:before, .heart:after{background: #EA212A;}.spinner .heart1{-webkit-animation-delay: -0.32s; animation-delay: -0.32s;}.spinner .heart2{-webkit-animation-delay: -0.16s; animation-delay: -0.16s;}@-webkit-keyframes bouncedelay{0%, 80%, 100%{-webkit-transform: scale(0.0)}40%{-webkit-transform: scale(1.0)}}@keyframes bouncedelay{0%, 80%, 100%{transform: scale(0.0); -webkit-transform: scale(0.0);}40%{transform: scale(1.0); -webkit-transform: scale(1.0);}}#member-details, #cover-details{margin-top: 10px; padding: 10px; border: 1px #f0efef solid; border-radius: 5px 5px 5px 5px;}#member-details table{border: 0px;}#member-details table thead th{border-bottom: 1px #f0efef solid;}</style> </head> <body> <div class='spinner'> <div class='heart heart1'></div></div></body></html>";

				LoaderHtml = text;



			}
			catch (Exception)
			{

				await DisplayAlert("Oooops", "Something went wrong", "OK");
			}

		}
	}
}