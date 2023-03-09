using System;
using System.Collections.Generic;
using System.Text;

namespace WalimuV2.Interfaces
{
	public interface ILocSettings
	{
		void OpenSettings();
		bool isGpsAvailable();
	}
}
