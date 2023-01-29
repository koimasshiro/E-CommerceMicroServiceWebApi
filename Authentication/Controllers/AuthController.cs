using Auth.Dtos;
using Auth.Model;
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
		public static User user= new User();

		private readonly UserContext _context;

		public AuthController(UserContext context)
		{
			_context = context;
		}

		[HttpPost("SignUp")]
		public async Task<ActionResult<List<User>>> SignUp(SignupDto signup)
		{
			CreatePasswordHash(signup.Password, out byte[] passwordHash, out byte[] passwordSalt);

			user.FirstName= signup.FirstName;
			user.LastName= signup.LastName;
			user.Email= signup.Email;
			user.PasswordHash = passwordHash;
			user.PasswordSalt = passwordSalt;

			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return Ok(await _context.Users.ToListAsync());
		}

		[HttpPost("Login")]
		public ActionResult<string> LogIn(LoginDto login)
		{
			if (user.Email != login.Email)
			{
				return BadRequest("User not found");
			}

			if (!VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
			{
				return BadRequest("Incorrect password!!");
			}
			return Ok("Login Successful"); 
	
		}

		//Create password Hash
		private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using HMACSHA512 hmac = new();
			passwordSalt = hmac.Key;
			passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
		}

		//Verify password
		private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using HMACSHA512 hmac = new(passwordSalt);
			var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			return ComputedHash == passwordHash;
		}
	}
}
