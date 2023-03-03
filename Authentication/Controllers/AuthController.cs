using Auth.Dtos;
using Auth.Model;
using Auth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Auth.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{

		private readonly IAuth _auth;
		public AuthController(IAuth auth, UserContext context)
		{
			_auth = auth;
		}

		[HttpPost("Signup")]
		public async Task<ActionResult<List<User>>> SignUp(SignupDto request)
		{
			var result = await _auth.SignUp(request);
			return Ok(result);
		}

		[HttpPost("Login")]
		public async Task<string> LogIn( LoginDto login)
		{
			var result = await _auth.LogIn(login);

			return result;
		}

		//reset password
		[HttpPost("Reset-Password")]
		public async Task<string> ResetPassword(ResetPassword request)
		{
			var result = await _auth.ResetPassword(request);

			return result;
			
		}
	}
}
