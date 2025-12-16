using LeaveMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeaveMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypeController : ControllerBase
    {
        public readonly LeaveMSContext _context;

        public LeaveTypeController(LeaveMSContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetLeaveTypes()
        {
            var leaveTypes = _context.LeaveTypes.ToList();
            return Ok(leaveTypes);
        }

        [HttpGet("{id}")]
        public IActionResult GetLeaveType(int id)
        {
            var leaveType = _context.LeaveTypes.Find(id);
            if (leaveType == null)
            {
                return NotFound();
            }
            return Ok(leaveType);
        }

        [HttpPost]
        public IActionResult CreateLeaveType(LeaveType leaveType)
        {
            _context.LeaveTypes.Add(leaveType);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLeaveType(int id, LeaveType leaveType)
        {
            var existingLeaveType = _context.LeaveTypes.Find(id);
            if (existingLeaveType == null)
            {
                return NotFound();
            }
            existingLeaveType.LeaveName = leaveType.LeaveName;
            existingLeaveType.Remarks = leaveType.Remarks;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLeaveType(int id)
        {
            var leaveType = _context.LeaveTypes.Find(id);
            if (leaveType == null)
            {
                return NotFound();
            }
            _context.LeaveTypes.Remove(leaveType);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
