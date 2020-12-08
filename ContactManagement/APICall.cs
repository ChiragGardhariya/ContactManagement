using ContactAPI.Models;
using ContactAPI.Models.VM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ContactManagement
{
	public class APICall
	{
		private readonly HttpClient _httpClient;
		private static Uri BaseEndpoint { get; set; }

		public APICall(Uri baseEndPoint)
		{
			if (baseEndPoint == null)
			{
				throw new ArgumentNullException("baseEndpoint");
			}
			BaseEndpoint = baseEndPoint;

			_httpClient = new HttpClient();

		}
		internal async Task<T> GetAsync<T>(Uri requestUrl)
		{
			var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
			response.EnsureSuccessStatusCode();
			var data = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(data);
		}

		private async Task<T> PostAsync<T>(Uri requestUrl, T content)
		{

			var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
			response.EnsureSuccessStatusCode();
			var data = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(data);
		}

		private async Task<T1> PostAsync<T1, T2>(Uri requestUrl, T2 content)
		{
			var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T2>(content));

			if (response.IsSuccessStatusCode)
			{
				var data = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<T1>(data);


			}
			else
			{
				var responsedata = await response.Content.ReadAsStringAsync();
				var Exception = JsonConvert.DeserializeObject<ContactManagement.Models.ExceptionModel>(responsedata);
				string msg = Exception.ExceptionMessage;

				throw new Exception(msg);

			}
			//response.EnsureSuccessStatusCode();

		}

		private async Task PutAsync<T2>(Uri requestUrl, T2 content)
		{
			var response = await _httpClient.PutAsync(requestUrl.ToString(), CreateHttpContent<T2>(content));

			if (response.IsSuccessStatusCode)
			{
				await response.Content.ReadAsStringAsync();


			}
			else
			{
				var responsedata = await response.Content.ReadAsStringAsync();
				var Exception = JsonConvert.DeserializeObject<ContactManagement.Models.ExceptionModel>(responsedata);
				string msg = Exception.ExceptionMessage;

				throw new Exception(msg);

			}
			//response.EnsureSuccessStatusCode();

		}


		private HttpContent CreateHttpContent<T>(T content)
		{
			var json = JsonConvert.SerializeObject(content);
			return new StringContent(json, Encoding.UTF8, "application/json");
		}

		internal static Uri CreateRequestUri(string relativePath, string queryString = "")
		{
			var endPoint = new Uri(BaseEndpoint, relativePath);
			var uriBuilder = new UriBuilder(endPoint);
			uriBuilder.Query = queryString;
			return uriBuilder.Uri;
		}

		internal async Task<List<Contact>> GetContacts(bool? status)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
			  "Contacts/GetAll"), "status=" + Convert.ToString(status));
			return await GetAsync<List<Contact>>(requestUrl);
		}

		internal async Task<Contact> GetContact(int id)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
			  "Contacts/GetById"), "id=" + id);
			return await GetAsync<Contact>(requestUrl);
		}

		internal async Task<ContactVM.AddContactOutput> SaveContact(Models.ContactVM contact)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
			   "Contacts/add"));
			return await PostAsync<ContactVM.AddContactOutput, Models.ContactVM>(requestUrl, contact);
		}

		internal async Task UpdateContact(Models.ContactVM contact)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
			   "Contacts/Update"));
			await PutAsync<Models.ContactVM>(requestUrl, contact);
		}

		internal async Task InactiveContact(int id)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
			   "Contacts/ChangeStatus"), "cid=" + id);
			await PutAsync<int>(requestUrl, id);
		}


	}
}
