using System;
using System.Collections.Generic;
using System.Text;

namespace WalimuV2.Models
{
	public class Hospital
	{
		public int pkid { get; set; }
		public string name { get; set; }
		public int region { get; set; }
		public int county { get; set; }
		public int subCounty { get; set; }
		public string address { get; set; }
		public string longitude { get; set; }
		public string latitude { get; set; }
		public object websiteUrl { get; set; }
		public object openingHours { get; set; }
		public string category { get; set; }
		public object countyNavigation { get; set; }
		public object regionNavigation { get; set; }
		public object subCountyNavigation { get; set; }
		public object[] tClinicEmail { get; set; }
		public object[] tClinicPhone { get; set; }
		public object[] tClinicService { get; set; }
		public object[] tClinicVisuals { get; set; }
		public double DistanceFromLocation { get; set; }
		public string Type { get; set; }

	}
}
