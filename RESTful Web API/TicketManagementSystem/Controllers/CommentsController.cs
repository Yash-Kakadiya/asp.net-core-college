using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TicketManagementSystem.Data;
using TicketManagementSystem.DTOs.Comment.Request;
using TicketManagementSystem.DTOs.Comment.Response;
using TicketManagementSystem.DTOs.Common;
using TicketManagementSystem.DTOs.User.Response;
using TicketManagementSystem.Models;

namespace TicketManagementSystem.Controllers
{
    [ApiController]
    [Route("comments")]
    [Authorize]
    public class CommentsController : ControllerBase
    {

        private readonly TicketDbContext _context;

        public CommentsController(TicketDbContext context)
        {
            _context = context;
        }

        //jwt claims
        private int GetCurrentUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        private string GetCurrentUserRole() => User.FindFirst(ClaimTypes.Role).Value;

        //post comment /tickets/{id}/comments
        [HttpPost("/tickets/{id}/comments")]
        public async Task<IActionResult> AddComment(int id, [FromBody] CommentRequest req)
        {
            try
            {
                var userId = GetCurrentUserId();

                var userRole = GetCurrentUserRole();

                var ticket = await _context.Tickets.FindAsync(id);

                if (ticket == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse($"Ticket {id} not found"));
                }

                if (userRole == "SUPPORT" && ticket.AssignedTo != userId)
                {

                    return StatusCode(403, ApiResponse<object>.ErrorResponse("UNAUTHORIZED"));
                }
                if (userRole == "USER" && ticket.CreatedBy != userId)
                {
                    return StatusCode(403, ApiResponse<object>.ErrorResponse("UNAUTHORIZED"));
                }

                var comment = new TicketComment
                {
                    TicketId = id,
                    UserId = userId,
                    Comment = req.Comment
                };
                _context.TicketComments.Add(comment);

                await _context.SaveChangesAsync();

                var saved = await GetCommentWithUser(comment.Id);
                return StatusCode(201, ApiResponse<CommentResponse>.SuccessResponse(
                    MapCommentToResponse(saved!), "Comment added successfully"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse("Error adding comment"));
            }
        }


        // get comments /tickets/{id}/comments
        [HttpGet("/tickets/{id}/comments")]
        public async Task<IActionResult> GetComments(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var userRole = GetCurrentUserRole();
                var ticket = await _context.Tickets.FindAsync(id);
                if (ticket == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse($"Ticket{id} not found"));

                }
                if (userRole == "SUPPORT" && ticket.AssignedTo != userId)
                {
                    return StatusCode(403, ApiResponse<object>.ErrorResponse("UNAUTHORIZED"));
                }
                if (userRole == "USER" && ticket.CreatedBy != userId)
                {
                    return StatusCode(403, ApiResponse<object>.ErrorResponse("UNAUTHORIZED"));
                }

                var comments = await _context.TicketComments
                    .Include(c => c.User).ThenInclude(u => u.Role)
                    .Where(c => c.TicketId == id)
                    .OrderBy(c => c.CreatedAt)
                    .ToListAsync();

                var response = comments.Select(c => MapCommentToResponse(c)).ToList();
                return Ok(ApiResponse<List<CommentResponse>>.SuccessResponse(response, "Comments getted successfully"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse("Error while getting comments"));
            }

        }

        //patch comment /comments/{id}
        [HttpPatch("/comments/{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] CommentRequest req)
        {
            try
            {
                var userId = GetCurrentUserId();
                var userRole = GetCurrentUserRole();
                var comment = await GetCommentWithUser(id);
                if (comment == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse($"Comment {id} not found"));
                }
                if (userRole != "MANAGER" && comment.UserId != userId)
                {
                    return StatusCode(403, ApiResponse<object>.ErrorResponse("UNAUTHORIZED"));
                }

                comment.Comment = req.Comment;
                await _context.SaveChangesAsync();

                var updated = await GetCommentWithUser(id);
                return Ok(ApiResponse<CommentResponse>.SuccessResponse(
                    MapCommentToResponse(updated!), "Comment updated successfully"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse("Error updating comment"));
            }
        }

        // delete comment /comments/{id} 
        [HttpDelete("comments/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var userRole = GetCurrentUserRole();

                var comment = await _context.TicketComments.FindAsync(id);
                if (comment == null)
                {

                    return NotFound(ApiResponse<object>.ErrorResponse($"Comment {id} not found"));
                }

                if (userRole != "MANAGER" && comment.UserId != userId)
                {

                    return StatusCode(403, ApiResponse<object>.ErrorResponse("You can only delete your own comments"));
                }

                _context.TicketComments.Remove(comment);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse("Error deleting comment"));
            }
        }
        private async Task<TicketComment?> GetCommentWithUser(int id)
        {
            return await _context.TicketComments
                .Include(c => c.User).ThenInclude(u => u.Role)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        private static CommentResponse MapCommentToResponse(TicketComment comment)
        {
            return new CommentResponse
            {
                Id = comment.Id,
                Comment = comment.Comment,
                User = new UserResponse
                {
                    Id = comment.User.Id,
                    Name = comment.User.Name,
                    Email = comment.User.Email,
                    Role = new RoleResponse
                    {
                        Id = comment.User.Role.Id,
                        Name = comment.User.Role.Name
                    },
                    CreatedAt = comment.User.CreatedAt
                },
                CreatedAt = comment.CreatedAt
            };
        }
    }
}