using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WalimuCare.Interfaces;
using WalimuCare.Views.hospitals;
using Xamarin.Forms;

namespace WalimuCare.ViewModels
{
	public class EnableGpsViewModel : AppViewModel
	{

		public ICommand ShowSettingsCommand { get; set; }

		public ICommand ClosePopUpCommand { get; set; }
		public ICommand CancelButtonCommand { get; set; }
		public ICommand ProceedWithtoutTurningOnGPSCommand { get; set; }


		public EnableGpsViewModel()
		{
			ShowSettingsCommand = new Command(async () => await ShowSettings());
			ClosePopUpCommand = new Command(async () => await ClosePopUp());
			CancelButtonCommand = new Command(async () => await CancelButton());
			ProceedWithtoutTurningOnGPSCommand = new Command(async () => await ProceedWithtoutTurningOnGPS());
		}


		public async Task ShowSettings()
		{
			try
			{
				DependencyService.Get<ILocSettings>().OpenSettings();
			}
			catch (Exception ex)
			{

				SendErrorMessageToAppCenter(ex, "Enable GPS");
			}
		}

		public async Task ClosePopUp()
		{
			try
			{

				var proceedWithoutGPS = DependencyService.Get<FindHospitalViewModel>().UserProceedsWithoutGpsBiengTurnedOn;

				if (DependencyService.Get<ILocSettings>().isGpsAvailable() == false && proceedWithoutGPS == false)
				{
					//await App.Current.MainPage.Navigation.PushPopupAsync(new ConfirmProceedWithoutTurningOnGpsPage());
					await Shell.Current.Navigation.PushPopupAsync(new ConfirmProceedWithoutTurningOnGpsPage());
					return;
				}

				//await App.Current.MainPage.Navigation.PopModalAsync();
				await Shell.Current.Navigation.PopModalAsync();



				if (DependencyService.Get<FindHospitalViewModel>().RefreshHospitalViewModelCommand.CanExecute(null))
				{
					DependencyService.Get<FindHospitalViewModel>().RefreshHospitalViewModelCommand.Execute(null);
				}

			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Enable GPS");
			}
		}

		public async Task CancelButton()
		{
			try
			{
				await App.Current.MainPage.Navigation.PopAllPopupAsync();
			}
			catch (Exception ex)
			{

				SendErrorMessageToAppCenter(ex, "Enable GPS");
			}
		}

		public async Task ProceedWithtoutTurningOnGPS()
		{
			try
			{
				DependencyService.Get<FindHospitalViewModel>().UserProceedsWithoutGpsBiengTurnedOn = true;
				await ClosePopUp();
			}
			catch (Exception ex)
			{

				SendErrorMessageToAppCenter(ex, "Enable Gps");
			}
		}
	}
}
