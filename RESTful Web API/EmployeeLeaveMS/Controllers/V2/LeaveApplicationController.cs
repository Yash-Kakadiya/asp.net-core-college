using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeLeaveMS.Data;
using EmployeeLeaveMS.Model;
using ApiVersionAttribute = Asp.Versioning.ApiVersionAttribute;

namespace EmployeeLeaveMS.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LeaveApplicationController : ControllerBase
    {
        private readonly AppDbContext _db;

        public LeaveApplicationController(AppDbContext db)
        {
            _db = db;
        }

        // GET api/v2/leave/employee/{employeeId}
        [HttpGet("employee/{employeeId}")]
        public IActionResult GetLeavesByEmployeeId(int employeeId)
        {
            var leaves = _db.LeaveApplications.Where(l => l.EmployeeId == employeeId).ToList();
            return Ok(leaves);
        }

        // GET api/v2/leaves
        [HttpGet]
        public IActionResult GetLeaves()
        {
            var leaves = _db.LeaveApplications.ToList();
            return Ok(leaves);
        }

        // GET api/v2/leave/{id}
        [HttpGet("{id}")]
        public IActionResult GetLeaveById(int id)
        {
            var leave = _db.LeaveApplications.Find(id);
            if (leave == null)
            {
                return NotFound();
            }
            return Ok(leave);
        }

        // POST api/v2/leave
        [HttpPost]
        public IActionResult ApplyLeave([FromBody] ApplyForLeaveDto leaveApplication)
        {
            if (leaveApplication == null)
            {
                return BadRequest("Invalid leave application data.");
            }
            // FromDate must be earlier than ToDate 
            if (leaveApplication.FromDate >= leaveApplication.ToDate)
            {
                return BadRequest("From Date must be earlier than To Date.");
            }
            //Total leave duration must not exceed 10 days
            var totalDays = (leaveApplication.ToDate - leaveApplication.FromDate).TotalDays;
            if (totalDays > 10)
            {
                return BadRequest("Total leave duration must not exceed 10 days.");
            }
            var leaveApplicationEntity = new LeaveApplication
            {
                EmployeeId = leaveApplication.EmployeeId,
                EmployeeName = leaveApplication.EmployeeName,
                LeaveType = leaveApplication.LeaveType,
                FromDate = leaveApplication.FromDate,
                ToDate = leaveApplication.ToDate,
                Reason = leaveApplication.Reason
            };
            _db.LeaveApplications.Add(leaveApplicationEntity);
            _db.SaveChanges();
            return CreatedAtAction(nameof(GetLeaves), new { id = leaveApplicationEntity.LeaveId }, leaveApplicationEntity);
        }

        // DELETE api/v2/leave/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteLeave(int id)
        {
            var leave = _db.LeaveApplications.Find(id);
            if (leave == null)
            {
                return NotFound();
            }
            _db.LeaveApplications.Remove(leave);
            _db.SaveChanges();
            return NoContent();
        }

        // PUT api/v2/leave/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateLeave(int id, [FromBody] LeaveApplication leaveApplication)
        {
            if (leaveApplication == null || leaveApplication.LeaveId != id)
            {
                return BadRequest("Invalid leave application data.");
            }
            var existingLeave = _db.LeaveApplications.Find(id);
            if (existingLeave == null)
            {
                return NotFound();
            }
            existingLeave.EmployeeId = leaveApplication.EmployeeId;
            existingLeave.FromDate = leaveApplication.FromDate;
            existingLeave.ToDate = leaveApplication.ToDate;
            existingLeave.Reason = leaveApplication.Reason;
            _db.SaveChanges();
            return NoContent();
        }

        // GET api/v2/leave/available
        [HttpGet("available")]
        public IActionResult GetAvailableLeaves()
        {
            var availableLeaves = _db.LeaveApplications
                .Where(l => l.ToDate >= DateTime.Now)
                .ToList();

            return Ok(availableLeaves);
        }
    }
}