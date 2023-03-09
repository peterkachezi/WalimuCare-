using System;

namespace WalimuV2.Models
{
	public class ListOfVisit
	{
		public int auto_id { get; set; }
		public string name { get; set; }
		public string mobile { get; set; }
		public long mvcNumber { get; set; }
		public string department { get; set; }
		public string approvalStatus { get; set; }
		public string hospitalName { get; set; }
		public string remarks { get; set; }
		public DateTime mvcDate { get; set; }
		public float? totalApprovedAmount { get; set; }
		public int totalRequestAmount { get; set; }
		public string NewMvcDate
		{
			get
			{
				return mvcDate.ToString("dddd, dd MMMM yyyy");


			}
		}

		public string StatusDescription
		{
			get
			{
				var status = approvalStatus;

				if (status == string.Empty)
				{
					var data = "In Progress";

					return data;
				}
				var k = status;

				return k;
			}
		}
	}
}
