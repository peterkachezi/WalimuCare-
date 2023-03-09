using System;
using System.Collections.Generic;
using System.Text;

namespace WalimuV2.ApiResponses
{
	public class HospitalDetails
	{
		public string Category { get; set; }
		public string WorkingHours { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string Website { get; set; }
		public string DescriptionOfLocation { get; set; }
		public string HospitalName { get; set; }
		public string HospitalType { get; set; }

		public List<string> ServicesOffered { get; set; }

		public string PageTitle = "Selected Hospital Details";
	}
}
