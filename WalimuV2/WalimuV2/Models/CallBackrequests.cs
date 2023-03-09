using System;
using System.Collections.Generic;
using System.Text;

namespace WalimuV2.Models
{
	public class CallBackrequests
	{

		public string MemberNumber { get; set; }
		public string PhoneNumber { get; set; }
		public string MemberName { get; set; }
		public string Remarks { get; set; }
		public DateTime CreateDate { get; set; }

		//public string cbrDate
		//{
		//	get
		//	{
		//		return RequestDate.ToString("dd-MMM-yyyy");
		//	}
		//}
	}
}
