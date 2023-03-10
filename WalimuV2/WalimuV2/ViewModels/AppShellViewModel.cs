

using Xamarin.Essentials;

namespace WalimuV2.ViewModels
{
	public class AppShellViewModel
	{

		static string firstName = Preferences.Get("firstName", string.Empty);

		static string lastName = Preferences.Get("lastName", string.Empty);
		public string ProfileName { get; set; } = firstName + " " + lastName;

	}
}
