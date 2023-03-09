using System;
using System.Collections.Generic;
using System.Text;

namespace WalimuV2.Models
{
	public class Login
	{
		public Guid? Id { get; set; }
		public string MemberNumber { get; set; }
		public string Password { get; set; }
	}
}
