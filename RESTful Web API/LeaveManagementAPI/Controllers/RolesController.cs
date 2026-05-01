using LeaveManagementAPI.Data;
using LeaveManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {

        private readonly AppDbContext _db;

        public RolesController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/roles
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var roles = await _db.Roles.ToListAsync();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error getting roles", error = ex.Message });
            }
        }

        // GET: api/roles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var role = await _db.Roles.FindAsync(id);
                if (role == null)
                    return NotFound(new { message = "Role not found" });

                return Ok(role);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error getting role", error = ex.Message });
            }
        }

        // POST: api/roles
        [HttpPost]
        public async Task<IActionResult> Create(Role role)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                bool exists = await _db.Roles.AnyAsync(r => r.Name == role.Name);
                if (exists)
                    return BadRequest(new { message = "Role already exists" });

                _db.Roles.Add(role);
                await _db.SaveChangesAsync();

                return Created("", role);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating role", error = ex.Message });
            }
        }

        // PUT: api/roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Role role)
        {
            try
            {

                var existing = await _db.Roles.FindAsync(id);
                if (existing == null)
                    return NotFound(new { message = "Role not found" });

                existing.Name = role.Name;
                await _db.SaveChangesAsync();

                return Ok(existing);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating role", error = ex.Message });
            }
        }

        // DELETE: api/roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var role = await _db.Roles.FindAsync(id);
                if (role == null)
                    return NotFound(new { message = "Role not found" });

                _db.Roles.Remove(role);
                await _db.SaveChangesAsync();

                return Ok(new { message = "Role deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting role", error = ex.Message });
            }
        }
    }
}
