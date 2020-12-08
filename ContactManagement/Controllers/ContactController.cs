using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ContactManagement.Factory;
using ContactManagement.Models;
using ContactManagement.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ContactManagement.Controllers
{
	public class ContactController : Controller
	{
		private readonly IOptions<APIConfig> _APIConfig;
		private readonly IMapper _mapper;
		public ContactController(IOptions<APIConfig> config, IMapper mapper)
		{
			_APIConfig = config;
			_mapper = mapper;
			ApplicationSettings.WebApiUrl = _APIConfig.Value.WebApiBaseUrl;


		}

		public IActionResult List()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> List(IFormCollection collection)
		{
			string strstatus = collection["Status"];

			bool? status = !String.IsNullOrEmpty(strstatus) ? Convert.ToBoolean(strstatus) : (bool?)null;

			var contacts = await APIClientFactory.Instance.GetContacts(status);
			return PartialView("_List", contacts);

		}

		public async Task<IActionResult> Manage(int id = 0)
		{

			ContactAPI.Models.Contact contact = id == 0 ? new ContactAPI.Models.Contact() : await APIClientFactory.Instance.GetContact(id);

			var contactvm = _mapper.Map<ContactVM>(contact);

			return View(contactvm);
		}
		[HttpPost]
		public async Task<IActionResult> Manage(ContactVM data)
		{
			if (ModelState.IsValid)
			{
				try
				{
					if (data.Id == 0)
					{
						var result = await APIClientFactory.Instance.SaveContact(data);
					}
					else
					{
						await APIClientFactory.Instance.UpdateContact(data);
					}
					return Json(new { status = "done" });

				}
				catch (Exception ex)
				{
					return Json(new { status = "fail", ex.Message });
				}

			}
			return Json(new { status = "fail" });
			//return View();
		}

		public async Task Delete(int id)
		{
			await APIClientFactory.Instance.InactiveContact(id);
			
		}
	}
}
