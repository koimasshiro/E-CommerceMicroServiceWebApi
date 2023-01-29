using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Dtos
{
	public class SignupDto
	{
		[Required(ErrorMessage = "First Name is required.")]
		[MaxLength(30, ErrorMessage = "Maximum length is 30 characters.")]
		[MinLength(2, ErrorMessage = "Minimum length is 2 characters.")]
		public string FirstName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Last Name is required.")]
		[MaxLength(30, ErrorMessage = "Maximum length is 30 characters.")]
		public string LastName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Email is required")]
		public string? Email { get; set; } = null;

		public string? Password { get; set; }
	}
}
