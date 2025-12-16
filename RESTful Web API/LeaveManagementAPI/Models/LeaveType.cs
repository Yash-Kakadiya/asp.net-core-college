using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagementAPI.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class LeaveType
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
        public ICollection<LeaveBalance> LeaveBalances { get; set; } = new List<LeaveBalance>();
    }
}
