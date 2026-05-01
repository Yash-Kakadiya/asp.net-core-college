using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PractiseProject.Data;
using PractiseProject.Interfaces;
using PractiseProject.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PractiseProject.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Generate JWT Token
        public string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");

            // Create secret key
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"])
            );

            // Signing credentials uses Hash-based Message Authentication Code
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Add claims (user info inside token)
            var claims = new[]
            {
            new Claim("UserId", user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("ProfileImg", user.ProfileImg)

        };

            // Token expiry time
            var expiryMinutes = Convert.ToDouble(jwtSettings["TokenExpiryMinutes"]);

            // Create token
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiryMinutes),
                signingCredentials: creds
            );

            // Return token string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Login Logic
        public async Task<object> LoginAsync(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user == null)
                return null;

            var token = GenerateJwtToken(user);

            return new
            {
                token,
                user = new
                {
                    user.Id,
                    user.Email,
                    user.ProfileImg
                }
            };
        }
    }
}
