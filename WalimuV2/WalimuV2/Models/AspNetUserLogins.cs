﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WalimuV2.Models
{
	public partial class AspNetUserLogins
	{
		public string LoginProvider { get; set; }
		public string ProviderKey { get; set; }
		public string UserId { get; set; }
		public virtual AspNetUsers User { get; set; }
	}
}
