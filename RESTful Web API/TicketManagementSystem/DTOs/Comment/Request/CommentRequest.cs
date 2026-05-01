using System.ComponentModel.DataAnnotations;

namespace TicketManagementSystem.DTOs.Comment.Request
{
    public class CommentRequest
    {
        [Required]
        public string Comment { get; set; } = string.Empty;
    }
}
