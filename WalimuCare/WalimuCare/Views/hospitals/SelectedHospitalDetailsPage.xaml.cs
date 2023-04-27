using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalimuCare.Models;
using WalimuCare.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace WalimuCare.Views.hospitals
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectedHospitalDetailsPage : ContentPage
	{

		public SelectedHospitalDetailsPage()
		{
			InitializeComponent();
			BindingContext = DependencyService.Get<FindHospitalViewModel>();

		}

		protected override void OnAppearing()
		{
			base.OnAppearing();


			try
			{

				var data = DependencyService.Get<FindHospitalViewModel>().SelectedHospitalDetails;


				var hospital = DependencyService.Get<FindHospitalViewModel>().OriginalLstHospitals.Where(p => p.name == data.HospitalName).FirstOrDefault();

				lblDescriptionOfLocation.CustomText = data.DescriptionOfLocation ?? "N/A";
				lblWorkingHours.CustomText = data.WorkingHours ?? "N/A";
				lblPhoneNumber.CustomText = data.PhoneNumber ?? "N/A";
				lblEmail.CustomText = data.Email ?? "N/A";
				lblWebsite.CustomText = data.Website ?? "N/A";
				lblHospitalName.CustomText = data.HospitalName ?? "N/A";
				lblType.CustomText = data.HospitalType ?? "N/A";


				ShowHospitalOnMap(hospital);

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}


		private void ShowHospitalOnMap(Hospital hospital)
		{
			try
			{
				double latitude = 0;
				double longitude = 0;

				if (hospital.latitude != null && hospital.latitude.Trim() != "")
				{
					latitude = Convert.ToDouble(hospital.latitude);
				}

				if (hospital.longitude != null && hospital.longitude.Trim() != "")
				{
					longitude = Convert.ToDouble(hospital.longitude);
				}

				map.Pins.Clear();

				Position position = new Position(latitude, longitude);

				Pin pin = new Pin()
				{
					Type = PinType.Place,
					Label = hospital.name,
					Address = "",
					Position = position,
					Rotation = 33.3f,
					Tag = hospital.name,

				};

				map.Pins.Add(pin);

				map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(1)), true);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

			}
		}

	}
}