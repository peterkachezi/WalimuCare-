using System;
using System.Collections.Generic;
using System.Text;
using WalimuCare.Models;

namespace WalimuCare.ApiResponses
{
	public class FaqBase
	{
		public string id { get; set; }
		public string category { get; set; }
		public List<FAQ> faqs { get; set; }
	}
}
