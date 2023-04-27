using System;
using System.Collections.Generic;
using System.Text;

namespace WalimuCare.Models
{
	public class County
	{
		public int? pkid { get; set; }
		public int? code { get; set; }
		public string county { get; set; }
		public string formerProvince { get; set; }
		public int? regionId { get; set; }
		public bool? isActive { get; set; }
		public object[] tClinic { get; set; }
	}
}
