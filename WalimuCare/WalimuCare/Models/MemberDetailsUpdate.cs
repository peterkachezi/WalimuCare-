using System;
using System.Collections.Generic;
using System.Text;

namespace WalimuCare.Models
{
	public class MemberDetailsUpdate
	{
		public Guid MemberId { get; set; }
		public string Field { get; set; }
		public string Change { get; set; }
	}
}
