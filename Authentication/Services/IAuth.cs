using Auth.Dtos;
using Auth.Model;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Services
{
	public interface IAuth
	{
		Task<List<User>>? SignUp(SignupDto request);
		 string LogIn(LoginDto login);
	}
}
