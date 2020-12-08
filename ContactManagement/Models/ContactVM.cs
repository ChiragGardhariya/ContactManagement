using System.ComponentModel.DataAnnotations;

namespace ContactManagement.Models
{
	public class ContactVM
	{
		//[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Please enter first name")]
		[MaxLength(50, ErrorMessage = "Max 50 characters")]
		[Display(Name ="First Name*")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Please enter last name")]
		[MaxLength(50, ErrorMessage = "Max 50 characters")]
		[Display(Name = "Last Name*")]
		public string LastName { get; set; }

		[DataType(DataType.EmailAddress, ErrorMessage = "Please enter valid email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Please enter mobile number")]
		[MaxLength(10, ErrorMessage = "10 digit mobile number only")]
		[MinLength(10, ErrorMessage = "10 digit mobile number only")]
		[Display(Name = "Mobile*")]
		public string PhoneNumber { get; set; }

		public bool Status { get; set; }
	}
}
