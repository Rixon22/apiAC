using System.Security.Cryptography;
using API.Data;
using API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    // https://localhost:5001/api/account
    public class AccountController(AppDbContext context) : BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterRequest request)
        {
            if (await EmailExists(request.Email))
                return BadRequest("Email is already in use");

            using var hmac = new HMACSHA512();

            var user = new Entities.AppUser
            {
                Email = request.Email.ToLower(),
                DisplayName = request.DisplayName,
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(request.Password)),
                PasswordSalt = hmac.Key
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            var user = await context.Users.SingleOrDefaultAsync(x => x.Email == request.Email.ToLower());

            if (user == null)
                return Unauthorized("Invalid email or password");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(request.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                    return Unauthorized("Invalid email or password");
            }

            return Ok(user);
        }

        private async Task<bool> EmailExists(string email)
        {
            return await context.Users.AnyAsync(x => x.Email == email.ToLower());
        }
    }
}
