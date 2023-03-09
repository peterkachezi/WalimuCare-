using System;
using System.Collections.Generic;
using System.Text;

namespace WalimuV2.ApiResponses
{

		public class Rootobject
		{
			public string access_token { get; set; }
			public string token_type { get; set; }
			public string user_Id { get; set; }
			public string member_Id { get; set; }
			public string user_name { get; set; }
			public int expires_in { get; set; }
			public int creation_Time { get; set; }
			public int expiration_Time { get; set; }
			public string firstName { get; set; }
			public string middleName { get; set; }
			public string lastName { get; set; }
			public string jobGroup { get; set; }
			public string gender { get; set; }
			public DateTime dateOfBirth { get; set; }
			public string phoneNumber { get; set; }
			public int accountStatus { get; set; }
			public int schemeStatus { get; set; }
		}




}
