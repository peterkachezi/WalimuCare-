using System;
using System.Collections.Generic;
using System.Text;

namespace WalimuCare.Models
{
	public class MinetMember
	{
		public int id { get; set; }
		public string principal_id { get; set; }
		public string member_id { get; set; }
		public string first_name { get; set; }
		public string last_name { get; set; }
		public int gender { get; set; }
		public string date_of_birth { get; set; }
		public string document_number { get; set; }
		public string mobile_phone_number { get; set; }
		public string email { get; set; }
		public string relation { get; set; }
		public int category_id { get; set; }
		public string job_group { get; set; }
		public int status { get; set; }
		public int scheme_id { get; set; }
	}

	public class Data
	{
		public MinetMember minetMember { get; set; }
		public List<DependantObject> dependents { get; set; }
	}

	public class VerifyMember
	{
		public bool success { get; set; }
		public Data data { get; set; }
		public string message { get; set; }
	}

	public class DependantObject
	{
		public MinetMember minetMember { get; set; }
	}
}
