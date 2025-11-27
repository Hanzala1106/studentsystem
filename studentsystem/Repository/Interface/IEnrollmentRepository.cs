using studentsystem.Entites;

namespace studentsystem.Repository.Interface
{
    public interface IEnrollmentRepository : IRepository<Enrollments, int>
    {
        Task<int> GetTotalEnrollmentsAsync();
        Task<List<object>> GetTopStudentsAsync();
        Task<List<object>> GetPopularCoursesAsync();
        Task<List<object>> GetCourseAvgGradesAsync();
        Task<List<object>> GetGradeDistributionAsync();
        Task<bool> IsDuplicateEnrollmentAsync(int studentId, int courseId);

    }
}
