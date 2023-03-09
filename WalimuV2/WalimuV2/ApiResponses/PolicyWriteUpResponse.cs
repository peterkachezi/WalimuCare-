using System;
using System.Collections.Generic;
using System.Text;

namespace WalimuV2.ApiResponses
{
	public class PolicyWriteUpResponse
	{
		public string BenefitName { get; set; }
		public string Title { get; set; }
		public List<string> Details { get; set; }
		public bool IsVisible { get; set; }
	}
}
