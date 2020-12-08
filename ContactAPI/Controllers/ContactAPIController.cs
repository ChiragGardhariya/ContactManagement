using ContactAPI.Manager;
using System.Web.Http;
using System.Web.Http.Cors;
using static ContactAPI.Models.VM.ContactVM;

namespace ContactAPI.Controllers
{
	//[EnableCors(origins: "http://localhost:59452", headers: "*", methods: "*")]
	public class ContactAPIController : ApiController
	{
		private readonly IContact _IContact;
		public ContactAPIController(IContact IContact)
		{
			this._IContact = IContact;
		}

		#region Create and edit contacts
		[Route("api/Contacts/Add")]
		[HttpPost]
		public IHttpActionResult CreateConact(AddContactInput input)
		{
			int id = _IContact.CreateContact(input);

			AddContactOutput response = new AddContactOutput();
			response.Id = id;

			return Ok(response);
		}

		[Route("api/Contacts/Update")]
		[HttpPut]
		public IHttpActionResult EditConact(EditContactInput input)
		{
			_IContact.EditContact(input);
			return Ok();
		}
		#endregion

		#region Get
		[Route("api/Contacts/GetAll")]
		[HttpGet]
		public IHttpActionResult GetContacts(bool? status)
		{
			return Ok(_IContact.GetContacts(status));
		}

		[Route("api/Contacts/GetById")]
		[HttpGet]
		public IHttpActionResult GetContactById(int id)
		{
			return Ok(_IContact.GetContactById(id));
		}
		#endregion


		#region Change status
		[Route("api/Contacts/ChangeStatus")]
		[HttpPut]
		public IHttpActionResult UpdateStatus(int cid)
		{
			_IContact.ChangeStatus(cid);
			return Ok();
		} 
		#endregion


	}
}
