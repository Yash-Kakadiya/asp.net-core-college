using System.ComponentModel.DataAnnotations;

namespace TicketManagementSystem.DTOs.Tickets.Request
{
    public class UpdateStatusRequest
    {
        [Required]
        public string Status { get; set; } = string.Empty;
    }
}
