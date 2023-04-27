using System;
using System.Collections.Generic;
using System.Text;

namespace WalimuCare.ApiResponses
{
	public class Clinic
	{
		public int pkId { get; set; }
		public string clinicName { get; set; }
		public string longitude { get; set; }
		public string latitude { get; set; }
	}
}
