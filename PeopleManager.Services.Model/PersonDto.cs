using System.ComponentModel.DataAnnotations;

namespace PeopleManager.Services.Model
{
	public class PersonDto
	{
		public int Id { get; set; }

		[Required]
		[Display(Name = "First name")]
		public string FirstName { get; set; }

		[Required]
		[Display(Name = "Last name")]
		public string LastName { get; set; }

		[Range(0, 99)]
		public int? Age { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}
