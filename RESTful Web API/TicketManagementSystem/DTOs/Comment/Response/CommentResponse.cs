using TicketManagementSystem.DTOs.User.Response;

namespace TicketManagementSystem.DTOs.Comment.Response
{
    public class CommentResponse
    {
        public int Id { get; set; }
        public string Comment { get; set; } = string.Empty;
        public UserResponse User { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
