using System.ComponentModel.DataAnnotations;

namespace ExamProgram.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        public int Class { get; set; }
    }
}
