using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Dtos
{
	public class ResetPasswordDto
	{
		public string? NewPassword { get; set; }
		public string? ConfirmPassword { get; set;}
	}
}
