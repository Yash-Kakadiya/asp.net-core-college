using TicketManagementSystem.DTOs.User.Response;

namespace TicketManagementSystem.DTOs.Tickets.Response
{
    public class TicketResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public UserResponse CreatedBy { get; set; } = null;
        public UserResponse? AssignedTo { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
