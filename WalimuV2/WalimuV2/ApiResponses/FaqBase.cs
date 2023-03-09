using System;
using System.Collections.Generic;
using System.Text;
using WalimuV2.Models;

namespace WalimuV2.ApiResponses
{
	public class FaqBase
	{
		public string id { get; set; }
		public string category { get; set; }
		public List<FAQ> faqs { get; set; }
	}
}
