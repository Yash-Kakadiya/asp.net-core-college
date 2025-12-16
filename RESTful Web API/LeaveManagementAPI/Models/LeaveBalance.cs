using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagementAPI.Models
{
    public class LeaveBalance
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

        [Required]
        public int TotalDays { get; set; }

        [Required]
        public int UsedDays { get; set; }

        [Required]
        public int RemainingDays { get; set; }

        [Required]
        public int Year { get; set; }
    }
}