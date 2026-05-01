using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketManagementSystem.Data;
using TicketManagementSystem.DTOs.Common;
using TicketManagementSystem.DTOs.User.Request;
using TicketManagementSystem.DTOs.User.Response;
using TicketManagementSystem.Helpers;
using TicketManagementSystem.Models;

namespace TicketManagementSystem.Controllers
{
    [ApiController]
    [Route("users")]
    [Authorize(Roles = "MANAGER")]
    public class UsersController : ControllerBase
    {

        private readonly TicketDbContext _context;

        public UsersController(TicketDbContext context)
        {
            _context = context;
        }

        // post user
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest req)
        {
            try
            {
                // check duplicate email
                var emailExists = await _context.Users
                    .AnyAsync(u => u.Email.ToLower() == req.Email.ToLower());
                if (emailExists)
                    return BadRequest(ApiResponse<object>.ErrorResponse("Email already exists"));

                // validate role
                var validRoles = new[] { "MANAGER", "SUPPORT", "USER" };
                if (!validRoles.Contains(req.Role.ToUpper()))
                    return BadRequest(ApiResponse<object>.ErrorResponse("Role must be MANAGER, SUPPORT, or USER"));

                // find role from db
                var role = await _context.Roles
                    .FirstOrDefaultAsync(r => r.Name == req.Role.ToUpper());
                if (role == null)
                    return NotFound(ApiResponse<object>.ErrorResponse($"Role '{req.Role}' not found"));

                var user = new User
                {
                    Name = req.Name,
                    Email = req.Email,
                    PasswordHash = PasswordHasher.Hash(req.Password),
                    RoleId = role.Id
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var savedUser = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Id == user.Id);

                return StatusCode(201, ApiResponse<UserResponse>.SuccessResponse(
                    new UserResponse
                    {
                        Id = savedUser.Id,
                        Name = savedUser.Name,
                        Email = savedUser.Email,
                        Role = new RoleResponse
                        {
                            Id = savedUser.Role.Id,
                            Name = savedUser.Role.Name
                        },
                        CreatedAt = savedUser.CreatedAt
                    }, "User created successfully"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An error occurred while creating user"));
            }
        }

        // get all
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _context.Users
                    .Include(u => u.Role)
                    .OrderBy(u => u.Name)
                    .ToListAsync();

                var response = users.Select(u => new UserResponse
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Role = new RoleResponse
                    {
                        Id = u.Role.Id,
                        Name = u.Role.Name
                    },
                    CreatedAt = u.CreatedAt
                }).ToList();
                return Ok(ApiResponse<List<UserResponse>>.SuccessResponse(response, "Users getting successfully"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse("Error getting users"));
            }
        }
    }
}
