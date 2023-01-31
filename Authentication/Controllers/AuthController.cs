using Auth.Dtos;
using Auth.Model;
using Auth.Services;
using Microsoft.AspNetCore.Http;
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

		public AuthController(IAuth auth)
		{
			_auth = auth;
		}

		[HttpPost("SignUp")]
		public async Task<ActionResult<List<User>>> SignUp(SignupDto request)
		{
			var result = await _auth.SignUp(request);
			return Ok(result);
		}

		[HttpPost("Login")]
		public ActionResult<string> LogIn(LoginDto login)
		{
			
			return Ok("Yaay!! You've been logged in successfully!!😄"); 
	
		}

		//Create password Hash
		//private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		//{
		//	using HMACSHA512 hmac = new();
		//	passwordSalt = hmac.Key;
		//	passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
		//}

		////Verify password
		//private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		//{
		//	using HMACSHA512 hmac = new(passwordSalt);
		//	var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
		//	return ComputedHash == passwordHash;
		//}
	}
}
