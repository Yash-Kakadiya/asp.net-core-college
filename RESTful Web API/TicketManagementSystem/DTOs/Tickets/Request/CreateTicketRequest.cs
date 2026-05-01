using System.ComponentModel.DataAnnotations;

namespace TicketManagementSystem.DTOs.Tickets.Request
{
    public class CreateTicketRequest
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title should at least 5 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(10, ErrorMessage = "Description should at least 10 characters")]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Priority { get; set; } = string.Empty;
    }
}
