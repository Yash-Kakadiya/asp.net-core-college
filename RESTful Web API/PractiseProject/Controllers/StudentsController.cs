using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiseProject.Data;
using PractiseProject.Models;

namespace PractiseProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public StudentsController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/students
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var students = await _db.Students.ToListAsync();

                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error getting students", error = ex.Message });
            }
        }

        // GET by Id: api/students/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var student = await _db.Students.FindAsync(id);
                if (student == null)
                    return NotFound(new { message = "Student not found" });

                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating student", error = ex.Message });
            }
        }

        // POST: api/students
        [HttpPost]
        public async Task<IActionResult> Register(Student student)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                bool emailExists = await _db.Students.AnyAsync(s => s.Email == student.Email);
                if (emailExists)
                    return BadRequest(new { message = "Email already registered" });

                _db.Students.Add(student);
                await _db.SaveChangesAsync();

                return Created("Student Created", new { student.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error registering student", error = ex.Message });
            }
        }

        // PUT: api/students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Student student)
        {
            try
            {
                var existing = await _db.Students.FindAsync(student, id);
                if (existing == null)
                    return NotFound(new { message = "Student not found" });

                existing.FirstName = student.FirstName;
                existing.LastName = student.LastName;
                existing.Email = student.Email;
                existing.Mobile = student.Mobile;
                existing.Course = student.Course;

                await _db.SaveChangesAsync();
                return Ok(new { message = "Student updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating student", error = ex.Message });
            }
        }

        // DELETE: api/students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existing = await _db.Students.FindAsync(id);
                if (existing == null)
                    return NotFound(new { message = "Student not found" });
                _db.Students.Remove(existing);
                await _db.SaveChangesAsync();
                return Ok(new { message = "Student deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting student", error = ex.Message });
            }
        }
    }
}