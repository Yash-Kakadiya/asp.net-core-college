using System.ComponentModel.DataAnnotations;

namespace TicketManagementSystem.DTOs.Tickets.Request
{
    public class AssignTicketRequest
    {
        [Required]
        public int UserId { get; set; }
    }
}
