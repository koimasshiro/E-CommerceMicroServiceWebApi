using Auth.Dtos;
using Auth.Model;
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.Functions;
using Org.BouncyCastle.Asn1.Ocsp;
using ProductAPI.Model;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;

namespace Auth.Services
{
	public class AuthService : IAuth
	{
		public static User user = new();
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

		public async Task<string> LogIn(LoginDto login)
		{
			//User user = new();

			var findUser = _context.Users.FirstOrDefault(u => u.Email == login.Email);

			if (findUser == null)
				return "User not found";

			if (!VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
			{
				return "Incorrect password!!";
			}

			return "Login Successful";
		}

		public async Task<string> ResetPassword(ResetPassword request)
		{
			var findUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

			if (findUser == null)
				return "User not found";

			CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);


			user.PasswordHash = passwordHash;
			user.PasswordSalt = passwordSalt;

			await _context.SaveChangesAsync();

			return "Password changed successfully!";
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
			using (HMACSHA512 hmac = new HMACSHA512(passwordSalt))
			{
				var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				return ComputedHash == passwordHash;
			}
		}
	}
}
