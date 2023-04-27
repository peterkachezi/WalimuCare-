using System;
using System.Collections.Generic;
using System.Text;

namespace WalimuCare.Models
{
	public class CallBackrequests
	{

		public string MemberNumber { get; set; }
		public string PhoneNumber { get; set; }
		public string MemberName { get; set; }
		public string Remarks { get; set; }
		public DateTime CreateDate { get; set; }
		public byte Status { get; set; }
        public string StatusDescription
        {
            get
            {
                if (Status == 0)
                {
                    return "Open";
                }
                else if (Status == 1)
                {
                    return "Closed";
                }

                else if (Status == 2)
                {
                    return "Rejected";
                }

                else
                {
                    return "Open";
                }
            }
        }

        public string StatusDescriptionColor
        {
            get
            {
                if (Status == 0)
                {
                    return "Orange";
                }
                else if (Status == 1)
                {
                    return "Green";
                }

                else if (Status == 2)
                {
                    return "#EA212A";
                }
              
                else
                {
                    return "#EA212A";
                }
            }
        }


        //public string cbrDate
        //{
        //	get
        //	{
        //		return RequestDate.ToString("dd-MMM-yyyy");
        //	}
        //}
    }
}
