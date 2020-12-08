using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagement
{
	public static class ExtensionMethod
	{
		public  static string ToBoolString(this bool status)
		{
			return status ? "Active" : "Inactive";
		}
	}
}
