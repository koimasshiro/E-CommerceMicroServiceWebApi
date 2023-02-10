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
		private readonly UserContext _context;
		public AuthController(IAuth auth, UserContext context)
		{
			_auth = auth;
			_context = context;
		}

		[HttpPost("Signup")]
		public async Task<ActionResult<List<User>>> SignUp(SignupDto request)
		{
			var result = await _auth.SignUp(request);
			return Ok(result);
		}

		[HttpPost("Login")]
		public async Task<ActionResult<dynamic>> LogIn( LoginDto login)
		{
			var result = await _auth.LogIn(login);

			return Ok(result);
		}

		//reset password
		[HttpPost]
		public async Task<List<User>> ResetPassword(int id, ResetPasswordDto request)
		{
			var result = await _auth.ResetPassword(id, request);

			return result;
			
		}
	}
}
