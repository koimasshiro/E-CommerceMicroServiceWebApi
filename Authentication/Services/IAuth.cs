using Auth.Dtos;
using Auth.Model;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Services
{
	public interface IAuth
	{
		Task<List<User>?> SignUp(SignupDto request);
		Task<ActionResult<string>?> LogIn(LoginDto login);
		Task<List<User>>? ResetPassword(int id, ResetPasswordDto request);
	}
}
