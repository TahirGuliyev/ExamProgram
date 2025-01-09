using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamProgram.Models
{
    public class Exam
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public int? SubjectId { get; set; } 

        [Required]
        public int? StudentId { get; set; } 

        [Required]
        public DateTime ExamDate { get; set; }

        [Required]
        [Range(0, 9)]
        public int Grade { get; set; }

        [ForeignKey("SubjectId")]
        public virtual Subject? Subject { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student? Student { get; set; }
    }
}
