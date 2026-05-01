using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiseProject.Data;
using PractiseProject.Models;

namespace PractiseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {

        private readonly AppDbContext _db;

        public CoursesController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/courses
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var courses = await _db.Courses.ToListAsync();

                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error getting courses", error = ex.Message });
            }
        }

        // GET by Id: api/courses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var course = await _db.Courses.FindAsync(id);
                if (course == null)
                    return NotFound(new { message = "Course not found" });

                return Ok(course);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating course", error = ex.Message });
            }
        }

        // POST: api/courses
        [HttpPost]
        public async Task<IActionResult> Register(Course course)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                _db.Courses.Add(course);
                await _db.SaveChangesAsync();

                return Created("Course Created", new { course.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error registering course", error = ex.Message });
            }
        }

        // PUT: api/courses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Course course)
        {
            try
            {
                var existing = await _db.Courses.FindAsync(course, id);
                if (existing == null)
                    return NotFound(new { message = "Course not found" });

                existing.Name = course.Name;
                existing.Duration = course.Duration;
                existing.Fees = course.Fees;

                await _db.SaveChangesAsync();
                return Ok(new { message = "Course updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating course", error = ex.Message });
            }
        }

        // DELETE: api/courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existing = await _db.Courses.FindAsync(id);
                if (existing == null)
                    return NotFound(new { message = "Course not found" });
                _db.Courses.Remove(existing);
                await _db.SaveChangesAsync();
                return Ok(new { message = "Course deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting course", error = ex.Message });
            }
        }
    }
}
