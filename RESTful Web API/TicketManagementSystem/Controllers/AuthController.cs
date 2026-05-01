using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketManagementSystem.Data;
using TicketManagementSystem.DTOs.Auth.Request;
using TicketManagementSystem.DTOs.Auth.Response;
using TicketManagementSystem.DTOs.Common;
using TicketManagementSystem.Helpers;

namespace TicketManagementSystem.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly TicketDbContext _context;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthController(TicketDbContext context, JwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email.ToLower() == request.Email.ToLower());

                if (user == null || !PasswordHasher.Verify(request.Password, user.PasswordHash))
                {
                    return Unauthorized(ApiResponse<object>.ErrorResponse("Invalid email or password"));
                }
                var token = _jwtTokenGenerator.GenerateToken(user);

                return Ok(ApiResponse<LoginResponse>.SuccessResponse(new LoginResponse
                {
                    Token = token
                }, "Login Successful"
                ));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse("Server Error during login"));
            }
        }
    }
}
