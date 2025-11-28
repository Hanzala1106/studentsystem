using System.ComponentModel.DataAnnotations;

namespace studentsystem.Entites
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int CourseId { get; set; }
        public string? Grade { get; set; }
        public DateOnly EnrollmentDate { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }


    }
}
