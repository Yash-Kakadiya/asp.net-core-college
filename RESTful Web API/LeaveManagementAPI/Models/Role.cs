using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagementAPI.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}