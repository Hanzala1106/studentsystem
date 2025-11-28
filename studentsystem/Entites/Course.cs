using System.ComponentModel.DataAnnotations;

namespace studentsystem.Entites
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        [Required]
        public string CourseCode { get; set; }

        public int Credits { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }

}
