using System.ComponentModel.DataAnnotations;

namespace Auth.Model
{
	public class ResetPassword
	{
		[Required]
		public string Email { get; set; }

		[Required, MinLength(6, ErrorMessage = "Please enter at least 6 characters")]
		public string Password { get; set; }

		[Required, Compare("Password")]
		public string ConfirmPassword { get; set; }
	}
}
