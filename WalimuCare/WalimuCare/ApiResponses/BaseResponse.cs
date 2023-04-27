using System;
using System.Collections.Generic;
using System.Text;

namespace WalimuCare.ApiResponses
{
	public class BaseResponse<T>
	{
		public bool success { get; set; }
		public T data { get; set; }
		public string message { get; set; }
	}
}
