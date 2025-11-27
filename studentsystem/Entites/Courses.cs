using System.ComponentModel.DataAnnotations;

namespace studentsystem.Entites
{
    public class Courses
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        [Required]
        public string CourseCode { get; set; }

        public int Credits { get; set; }
        public ICollection<Enrollments> Enrollments { get; set; } = new List<Enrollments>();
    }

}
