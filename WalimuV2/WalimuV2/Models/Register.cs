using System;
using System.Collections.Generic;
using System.Text;

namespace WalimuV2.Models
{
	public class Register
	{
		public Guid Id { get; set; }
		public string MemberNumber { get; set; }
		public string PhoneNumber { get; set; }
		public string Password { get; set; }
		public byte? Status { get; set; }
		public byte IsAccountCreated { get; set; }
		public byte IsAccountExist { get; set; }
		public byte IsMemberExist { get; set; }
		public DateTime CreateDate { get; set; }
	}
}
