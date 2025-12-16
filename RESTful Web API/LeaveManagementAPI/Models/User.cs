using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace LeaveManagementAPI.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(100), EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(255)]
        public string Password { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [MaxLength(20)]
        public string? Gender { get; set; }

        [Required]
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role? Role { get; set; }

        public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
        public ICollection<LeaveBalance> LeaveBalances { get; set; } = new List<LeaveBalance>();
    }
}
