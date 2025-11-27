using Microsoft.EntityFrameworkCore;
using studentsystem.Data;
using studentsystem.Entites;
using studentsystem.Repository.Interface;

namespace studentsystem.Repository.Implementation
{
    public class EnrollmentRepository : Repository<Enrollments, int>, IEnrollmentRepository
    {
        public EnrollmentRepository(AppDbContext context, IHttpContextAccessor httpContext) : base(context, httpContext)
        {
        }
        private AppDbContext AppDbContext => Context as AppDbContext;

        public Task<int> GetTotalEnrollmentsAsync()
        {
            return AppDbContext.Enrollments.CountAsync();
        }

        public async Task<List<object>> GetTopStudentsAsync()
        {
            return await AppDbContext.Enrollments
               .GroupBy(e => e.StudentId)
               .Select(g => new { StudentId = g.Key, Count = g.Count() })
               .OrderByDescending(x => x.Count)
               .Take(5)
               .Join(AppDbContext.Students,
                     g => g.StudentId,
                     s => s.StudentId,
                     (g, s) => new
                     {
                         s.StudentId,
                         Name = s.FirstName + " " + s.LastName,
                         g.Count
                     })
               .ToListAsync<object>();
        }

        public async Task<List<object>> GetPopularCoursesAsync()
        {
            return await AppDbContext.Enrollments
               .GroupBy(e => e.CourseId)
               .Select(g => new { CourseId = g.Key, Count = g.Count() })
               .Join(AppDbContext.Courses,
                     g => g.CourseId,
                     c => c.CourseId,
                     (g, c) => new
                     {
                         c.CourseId,
                         c.CourseName,
                         EnrollmentCount = g.Count
                     })
               .OrderByDescending(x => x.EnrollmentCount)
               .Take(5)
               .ToListAsync<object>();
        }

        public async Task<List<object>> GetCourseAvgGradesAsync()
        {
            return await AppDbContext.Enrollments
                .Where(e => e.Grade != null)
                .GroupBy(e => e.CourseId)
                .Select(g => new
                {
                    CourseId = g.Key,
                    AvgGrade = g.Average(e =>
                        e.Grade == "A" ? 4 :
                        e.Grade == "B" ? 3 :
                        e.Grade == "C" ? 2 :
                        e.Grade == "D" ? 1 : 0)
                })
                .OrderByDescending(x => x.AvgGrade)
                .ToListAsync<object>();
        }

        public async Task<List<object>> GetGradeDistributionAsync()
        {
            return await AppDbContext.Enrollments
                .Where(e => e.Grade != null)
                .GroupBy(e => e.Grade)
                .Select(g => new { Grade = g.Key, Count = g.Count() })
                .ToListAsync<object>();
        }
        public Task<bool> IsDuplicateEnrollmentAsync(int studentId, int courseId)
        {
            return AppDbContext.Enrollments
                .AnyAsync(e => e.StudentId == studentId && e.CourseId == courseId);
        }
    }
}
