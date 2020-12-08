using ContactManagement.Utility;
using System;

namespace ContactManagement.Factory
{
	public static class APIClientFactory
	{

		public static Uri apiUri;

		private static Lazy<APICall> restCall = new Lazy<APICall>(() => new APICall(apiUri), System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

		static APIClientFactory()
		{
			apiUri = new Uri(ApplicationSettings.WebApiUrl);
		}

		public static APICall Instance
		{
			get
			{
				return restCall.Value;
			}
		}
	}
}
