using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PractiseProject.Models
{
    //Courses Table - id(int, PK) - name(varchar(50)) - duration(varchar(50)) - fees(decimal (8,2)) 
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;


        [Required, MaxLength(50)]
        public string Duration { get; set; } = string.Empty;

        [Required, MaxLength(8)]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Fees { get; set; }

    }
}
