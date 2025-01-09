using System.ComponentModel.DataAnnotations;

namespace ExamProgram.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        [MaxLength(3)]
        public string SubjectCode { get; set; }

        [Required]
        [MaxLength(30)]
        public string SubjectName { get; set; }

        [Required]
        public int Class { get; set; }

        [Required]
        [MaxLength(20)]
        public string TeacherFirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string TeacherLastName { get; set; }
    }
}
