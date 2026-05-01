using EventManagementApiExam.Data;
using EventManagementApiExam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


//API Documentation
//1. Register for an Event
//Endpoint: POST / api / events / register
//Validations:
// ParticipantName must contain minimum 3 characters
// Email must be in a valid email format
// EventName is required
// Age must be between 18 and 30
// RegistrationDate must not be a future date
//2. Do Create all necessary APIs with proper validations.
//Example:
//Get All Events [Endpoint: GET /api/Events]
//Delete Event[Endpoint: DELETE / api / DeleteEvent / 101]
//Update Event[Endpoint: PUT / api / UpdateEvent / 102]
namespace EventManagementApiExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventRegistrationsController : ControllerBase
    {
        private readonly EventManagementDbContext _context;

        public EventRegistrationsController(EventManagementDbContext context)
        {
            _context = context;
        }

        // POST /api/events/register
        [HttpPost("register")]
        public IActionResult RegisterEvent([FromBody] EventRegistration reg)
        {

            _context.EventRegistrations.Add(reg);
            _context.SaveChanges();

            return Ok(reg);
        }

        //GET /api/Events
        [HttpGet]
        public IActionResult GetAllEvents()
        {
            var e = _context.EventRegistrations.ToList();

            return Ok(e);
        }

        //DELETE /api/DeleteEvent/101
        [HttpDelete("DeleteEvent/{id:int}")]
        public IActionResult DeleteEvent(int id)
        {
            var e = _context.EventRegistrations.Find(id);
            if (e == null)
            {
                return NotFound();
            }

            _context.EventRegistrations.Remove(e);
            _context.SaveChanges();

            return NoContent();
        }

        //PUT /api/UpdateEvent/102
        [HttpPut("UpdateEvent/{id:int}")]
        public IActionResult UpdateEvent(int id, [FromBody] EventRegistration newR)
        {

            var oldR = _context.EventRegistrations.Find(id);

            if (oldR == null)
            {
                return NotFound();
            }


            oldR.ParticipantName = newR.ParticipantName;
            oldR.Email = newR.Email;
            oldR.EventName = newR.EventName;
            oldR.Age = newR.Age;
            oldR.RegistrationDate = newR.RegistrationDate;

            _context.SaveChanges();

            return Ok(oldR);
        }
    }
}

