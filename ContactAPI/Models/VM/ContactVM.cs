using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactAPI.Models.VM
{
	public class ContactVM
	{
		public class AddContactInput
		{
			public string FirstName { get; set; }

			public string LastName { get; set; }

			public string Email { get; set; }

			public string PhoneNumber { get; set; }
		}

		public class AddContactOutput
		{
			public int Id { get; set; }
		}

		public class EditContactInput: AddContactInput
		{
			public int Id { get; set; }
			public bool Status { get; set; }
		}
	}
}