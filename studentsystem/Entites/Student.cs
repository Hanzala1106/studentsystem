using System.ComponentModel.DataAnnotations;

namespace studentsystem.Entites
{
    public class Student
    {

        [Key]
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public DateOnly EnrollmentDate { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();


    }
}
