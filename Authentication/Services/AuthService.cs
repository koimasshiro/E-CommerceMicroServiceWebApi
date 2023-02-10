using Auth.Dtos;
using Auth.Model;
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using ProductAPI.Model;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;

namespace Auth.Services
{
	public class AuthService : IAuth
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly UserContext _context;

		public AuthService(IHttpClientFactory httpClientFactory ,UserContext context)
		{
			_context = context;
			_httpClientFactory = httpClientFactory;
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

		public async Task<ActionResult<string>> LogIn(LoginDto login)
		{
			User user = new();

			var findUser = _context.Users.Find(login.Email);

			if (findUser == null)
				return "User not found";

			if (!VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
			{
				return null!;
			}

			return "Login Successful";
		}

		//Verify password
		private static bool VerifyPasswordHash(string? password, byte[] passwordHash, byte[] passwordSalt)
		{
			using HMACSHA512 hmac = new(passwordSalt);
			var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			return ComputedHash == passwordHash;
		}

		public async Task<List<User>?> ResetPassword(int id, ResetPasswordDto request)
		{
			var user = await _context.Users.FindAsync(id);

			CreatePasswordHash(request.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);

			if (user == null)
				return null;

			if (request.NewPassword != request.ConfirmPassword)
				return null;

			user.PasswordHash = passwordHash;
			user.PasswordSalt = passwordSalt;

			await _context.SaveChangesAsync();

			return await _context.Users.ToListAsync();
		}

		private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using HMACSHA512 hmac = new();
			passwordSalt = hmac.Key;
			passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
		}
	}
}
