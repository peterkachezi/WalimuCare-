using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WalimuV2.ViewModels
{
	class UserLocationViewModel : AppViewModel
	{
		public ICommand ShowSettingsCommand { get; set; }

		public ICommand ClosePopUpCommand { get; set; }
		public ICommand CancelButtonCommand { get; set; }

		private bool isLocationServicesEnabled;

		public bool IsLocationServicesEnabled
		{
			get { return isLocationServicesEnabled; }
			set { isLocationServicesEnabled = value; OnPropertyChanged(); }
		}



		public UserLocationViewModel()
		{
			ShowSettingsCommand = new Command(async () => await ShowSettings());
			ClosePopUpCommand = new Command(async () => await ClosePopUp());
			CancelButtonCommand = new Command(async () => await CancelButton());
		}


		public async Task ShowSettings()
		{
			try
			{


				//request user to allow access to location 
				await RequestLocationPermision();

			}
			catch (Exception ex)
			{

				SendErrorMessageToAppCenter(ex, "Enable Location");
			}
		}

		public async Task ClosePopUp()
		{
			try
			{

				DependencyService.Get<FindHospitalViewModel>().ProceedWithoutEnablingLocationPermission = true;

				//await App.Current.MainPage.Navigation.PopModalAsync();
				await Shell.Current.Navigation.PopModalAsync();



				if (DependencyService.Get<FindHospitalViewModel>().RefreshHospitalViewModelCommand.CanExecute(null))
				{
					DependencyService.Get<FindHospitalViewModel>().RefreshHospitalViewModelCommand.Execute(null);
				}

			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Enable Location");
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

				SendErrorMessageToAppCenter(ex, "Enable Location");
			}
		}
		public async Task RequestLocationPermision()
		{
			try
			{
				var isLocationDataAllowed = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

				var locationInUseAllowed = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();


				if (locationInUseAllowed == PermissionStatus.Denied)
				{
					isLocationServicesEnabled = true;
				}
				else
				{
					isLocationServicesEnabled = false;
				}

				if (isLocationDataAllowed == PermissionStatus.Granted)
				{
					await ClosePopUp();
				}


			}
			catch (Exception ex)
			{
				SendErrorMessageToAppCenter(ex, "Find Hospital");
			}
		}


	}
}
