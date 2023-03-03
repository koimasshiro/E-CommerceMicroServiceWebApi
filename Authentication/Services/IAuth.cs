using Auth.Dtos;
using Auth.Model;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Services
{
	public interface IAuth
	{
		Task<List<User>?> SignUp(SignupDto request);
		Task<string> LogIn(LoginDto login);
		Task<string> ResetPassword(ResetPassword request);
	}
}
