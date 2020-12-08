using ContactAPI.Models;
using System.Collections.Generic;
using static ContactAPI.Models.VM.ContactVM;

namespace ContactAPI.Manager
{
	public interface IContact
	{
		int CreateContact(AddContactInput input);

		void EditContact(EditContactInput input);

		Contact GetContactById(int id);

		void ChangeStatus(int cid);

		List<Contact> GetContacts(bool? status);
	}
}
