using System;
using System.Collections.Generic;
using System.Text;

namespace WalimuCare.Interfaces
{
	public interface ILocSettings
	{
		void OpenSettings();
		bool isGpsAvailable();
	}
}
