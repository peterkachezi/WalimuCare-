using System;
using System.Collections.Generic;
using System.Text;

namespace WalimuCare.Models
{
	public class PolicyLimit
	{
		public int memberWiseLimitID { get; set; }
		public string memberID { get; set; }
		public string memberName { get; set; }
		public string departmentName { get; set; }
		public string jobGroupName { get; set; }
		public string schemeName { get; set; }
		public int limitAmount { get; set; }
		public int utilizeAmount { get; set; }
		public int availableAmount { get; set; }
		public int minPercent { get; set; }
		public string mvcNo { get; set; }
	}
}

