using Auth.Dtos;
using Auth.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Auth.Services
{
	public class AuthService : IAuth
	{
		private readonly UserContext _context;

		public AuthService(UserContext context) 
		{
			_context = context;
		}



		public async Task<List<User>>? SignUp(SignupDto request)
		{
			User user = new();
			CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

			user.FirstName = request.FirstName;
			user.LastName = request.LastName;
			user.Email = request.Email;
			user.PasswordHash = passwordHash;
			user.PasswordSalt = passwordSalt;

			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return await _context.Users.ToListAsync();
		}

		public string LogIn(LoginDto login)
		{
			var user = _context.Users.Find(login.Email);
			if (user == null)
			{
				return "User not found";
			}

			if (!VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
			{
				return "Incorrect password!!";
			}
			return "Yay!, You've been logged in succesfully!!";

		}

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
