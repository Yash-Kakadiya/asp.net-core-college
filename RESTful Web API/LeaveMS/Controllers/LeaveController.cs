using LeaveMS.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
//create table Leave (
//	LeaveId INT PRimary key Identity(1,1),
//    LeaveId INT NOT NULL Foreign Key References Leave(LeaveId),
//    TotalLeaves INT NOT NULL,
//    Year INT
//)
namespace LeaveMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly LeaveMSContext _context;
        public LeaveController(LeaveMSContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetLeaves()
        {
            var leaves = _context.Leaves.ToList();
            return Ok(leaves);
        }

        [HttpGet("{id}")]
        public IActionResult GetLeave(int id)
        {
            var leave = _context.Leaves.Find(id);
            if (leave == null)
            {
                return NotFound();
            }
            return Ok(leave);
        }

        [HttpPost]
        public IActionResult CreateLeave(Leave leave)
        {
            _context.Leaves.Add(leave);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLeave(int id, Leave leave)
        {
            var existingLeave = _context.Leaves.Find(id);
            if (existingLeave == null)
            {
                return NotFound();
            }
            existingLeave.LeaveId = leave.LeaveId;
            existingLeave.LeaveTypeId = leave.LeaveTypeId;
            existingLeave.TotalLeaves = leave.TotalLeaves;
            existingLeave.Year = leave.Year;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLeave(int id)
        {
            var leave = _context.Leaves.Find(id);
            if (leave == null)
            {
                return NotFound();
            }
            _context.Leaves.Remove(leave);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
