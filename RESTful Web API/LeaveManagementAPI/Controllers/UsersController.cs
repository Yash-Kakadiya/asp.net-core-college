using LeaveManagementAPI.Data;
using LeaveManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;

        public UsersController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _db.Users
                    .Include(u => u.Role)
                    .Select(u => new
                    {
                        u.Id,
                        u.Name,
                        u.Email,
                        Role = u.Role.Name
                    })
                    .ToListAsync();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error getting users", error = ex.Message });
            }
        }

        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (string.IsNullOrWhiteSpace(user.Password) || user.Password.Length < 6)
                    return BadRequest(new { message = "Password must be at least 6 characters" });

                bool emailExists = await _db.Users.AnyAsync(u => u.Email == user.Email);
                if (emailExists)
                    return BadRequest(new { message = "Email already registered" });

                _db.Users.Add(user);
                await _db.SaveChangesAsync();

                return Created("", new { user.Id, user.Name, user.Email });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error registering user", error = ex.Message });
            }
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            try
            {
                var existing = await _db.Users.FindAsync(id);
                if (existing == null)
                    return NotFound(new { message = "User not found" });

                existing.Name = user.Name;
                existing.Email = user.Email;
                existing.Password = user.Password;
                existing.RoleId = user.RoleId;

                await _db.SaveChangesAsync();
                return Ok(new { message = "User updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating user", error = ex.Message });
            }
        }
    }
}
