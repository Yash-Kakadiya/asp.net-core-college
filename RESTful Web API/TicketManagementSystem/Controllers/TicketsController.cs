using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TicketManagementSystem.Data;
using TicketManagementSystem.DTOs.Common;
using TicketManagementSystem.DTOs.Tickets.Request;
using TicketManagementSystem.DTOs.Tickets.Response;
using TicketManagementSystem.DTOs.User.Response;
using TicketManagementSystem.Models;

namespace TicketManagementSystem.Controllers
{
    [ApiController]
    [Route("tickets")]
    [Authorize]
    public class TicketsController : ControllerBase
    {

        private readonly TicketDbContext _context;

        public TicketsController(TicketDbContext context)
        {
            _context = context;
        }

        //jwt claims
        private int GetCurrentUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        private string GetCurrentUserRole() => User.FindFirst(ClaimTypes.Role).Value;

        //post ticket
        [HttpPost]
        [Authorize(Roles = "USER, MANAGER")]
        public async Task<IActionResult> Create([FromBody] CreateTicketRequest req)
        {
            try
            {
                var validPriorities = new[] { "LOW", "MEDIUM", "HIGH" };

                if (!validPriorities.Contains(req.Priority.ToUpper()))
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Invalid Priority"));
                }

                var ticket = new Ticket
                {
                    Title = req.Title,
                    Description = req.Description,
                    Priority = req.Priority.ToUpper(),
                    Status = "OPEN",
                    CreatedBy = GetCurrentUserId(),
                };
                _context.Tickets.Add(ticket);
                await _context.SaveChangesAsync();

                var savedTicket = await GetTicketWithIncludes(ticket.Id);

                return StatusCode(201, ApiResponse<TicketResponse>.SuccessResponse(await MapTicketResponse(savedTicket), "Ticket Sreated Successfully"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse("Error in creating ticket"));
            }
        }

        //get all tickets
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var userId = GetCurrentUserId();
                var userRole = GetCurrentUserRole();

                List<Ticket> tickets;

                if (userRole == "USER")
                {
                    tickets = await _context.Tickets.Where(t => t.CreatedBy == userId).Include(t => t.CreatedByNavigation).ThenInclude(u => u.Role).Include(t => t.AssignedToNavigation).ThenInclude(u => u.Role).OrderByDescending(t => t.CreatedAt).ToListAsync();
                }
                else if (userRole == "SUPPORT")
                {
                    tickets = await _context.Tickets.Where(t => t.AssignedTo == userId).Include(t => t.CreatedByNavigation).ThenInclude(u => u.Role).Include(t => t.AssignedToNavigation).ThenInclude(u => u.Role).OrderByDescending(t => t.CreatedAt).ToListAsync();
                }
                else
                {
                    tickets = await _context.Tickets.Include(t => t.CreatedByNavigation).ThenInclude(u => u.Role).Include(t => t.AssignedToNavigation).ThenInclude(u => u.Role).OrderByDescending(t => t.CreatedAt).ToListAsync();

                }
                //var res = tickets.Select(t => MapTicketResponse(t)).ToList()
                var res = new List<TicketResponse>();
                foreach (var t in tickets)
                {
                    res.Add(await MapTicketResponse(t));
                }

                return Ok(ApiResponse<List<TicketResponse>>.SuccessResponse(res, "Tickets fetched successfully"));

            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse("Error getting tickets"));
            }
        }

        //patch ticket assign /tickets/id/assign
        [HttpPatch("{id}/assign")]
        [Authorize(Roles = "MANAGER,SUPPORT")]
        public async Task<IActionResult> Assign(int id, [FromBody] AssignTicketRequest req)
        {
            try
            {
                var ticket = await GetTicketWithIncludes(id);

                if (ticket == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse($"Ticket with ID {id} not found"));
                }

                var assigned = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Id == req.UserId);

                if (assigned == null)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse($"user with id {req.UserId} not found"));
                }

                if (assigned.Role.Name == "USER")
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("UNAUTHORIZED: Tickets cannot be assigned to Users with role USER"));
                }

                ticket.AssignedTo = req.UserId;
                await _context.SaveChangesAsync();

                var updated = await GetTicketWithIncludes(id);

                return Ok(ApiResponse<TicketResponse>.SuccessResponse(
                    await MapTicketResponse(updated!), "Ticket assigned successfully"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse("Error updating ticket"));
            }
        }

        //patch ticket status /tickets/{id}/status
        [HttpPatch("{id}/status")]
        [Authorize(Roles = "MANAGER,SUPPORT")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusRequest req)
        {
            try
            {
                var ticket = await GetTicketWithIncludes(id);

                if (ticket == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse($"Ticket {id} not found"));
                }

                var validStatus = new[] { "OPEN", "IN_PROGRESS", "RESOLVED", "CLOSED" };

                if (!validStatus.Contains(req.Status.ToUpper()))
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Invalid Status"));
                }

                var statusOrder = new Dictionary<string, int>
            {
                { "OPEN", 1 }, { "IN_PROGRESS", 2 }, { "RESOLVED", 3 }, { "CLOSED", 4 }
            };

                if (statusOrder[req.Status.ToUpper()] <= statusOrder[ticket.Status])
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Status can't move backward"));
                }

                ticket.Status = req.Status.ToUpper();

                await _context.SaveChangesAsync();

                var updated = await GetTicketWithIncludes(id);

                return Ok(ApiResponse<TicketResponse>.SuccessResponse(
                    await MapTicketResponse(updated!), "Ticket status updated successfully"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse("Error updating ticket status"));
            }
        }


        // delete ticket /tickets/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "MANAGER")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var ticket = await _context.Tickets.FindAsync(id);

                if (ticket == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse($"Ticket {id} not found"));
                }
                _context.Tickets.Remove(ticket);

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse("Error while deleting ticket"));
            }
        }

        private async Task<Ticket?> GetTicketWithIncludes(int id)
        {
            var ticket = await _context.Tickets.Include(t => t.CreatedByNavigation).ThenInclude(u => u.Role).Include(t => t.AssignedToNavigation).ThenInclude(u => u.Role).FirstOrDefaultAsync(t => t.Id == id);
            return ticket;
        }
        private async Task<TicketResponse> MapTicketResponse(Ticket t)
        {
            return new TicketResponse
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                CreatedBy = new UserResponse
                {
                    Id = t.CreatedByNavigation.Id,
                    Name = t.CreatedByNavigation.Name,
                    Email = t.CreatedByNavigation.Email,
                    Role = new RoleResponse
                    {
                        Id = t.CreatedByNavigation.Role.Id,
                        Name = t.CreatedByNavigation.Role.Name
                    },
                    CreatedAt = t.CreatedByNavigation.CreatedAt
                },
                AssignedTo = t.AssignedToNavigation == null ? null : new UserResponse
                {
                    Id = t.AssignedToNavigation.Id,
                    Name = t.AssignedToNavigation.Name,
                    Email = t.AssignedToNavigation.Email,
                    Role = new RoleResponse
                    {
                        Id = t.AssignedToNavigation.Role.Id,
                        Name = t.AssignedToNavigation.Role.Name
                    },
                    CreatedAt = t.AssignedToNavigation.CreatedAt
                },
                CreatedAt = t.CreatedAt
            };
        }
    }
}
