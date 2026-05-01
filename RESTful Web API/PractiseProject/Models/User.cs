using Microsoft.EntityFrameworkCore;
using PractiseProject.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;


//Users Table - id (int, PK) - email (varchar(50)) - password (varchar(50)) -profileimg (varchar(max)) | null

namespace PractiseProject.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50), EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(50)]
        public string Password { get; set; }

        public string? ProfileImg { get; set; } = null;
    }
}
