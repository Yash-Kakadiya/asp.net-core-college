using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagementAPI.Models
{
    public class LeaveRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required]
        [ForeignKey("LeaveType")]
        public int LeaveTypeId { get; set; }
        public LeaveType? LeaveType { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public int Days { get; set; }

        [MaxLength(1000)]
        public string? Reason { get; set; }

        [Required, MaxLength(20)]
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected
    }
}