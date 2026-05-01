//Students Table - id (int, PK) - firstName (varchar(50)) - lastName (varchar(50)) - email (varchar(50), unique) - mobile (varchar(50)) - course (varchar(50)) 

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace PractiseProject.Models
{
    [Index("Email", IsUnique = true)]
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required, MaxLength(50), EmailAddress]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Mobile { get; set; }

        [MaxLength(50)]
        public string Course { get; set; }

    }
}
