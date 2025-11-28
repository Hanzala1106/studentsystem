using Microsoft.EntityFrameworkCore;
using studentsystem.Data;
using studentsystem.Entites;
using studentsystem.Repository.Interface;

namespace studentsystem.Repository.Implementation
{
    public class CourseRepository : Repository<Course, int>, ICourseRepository
    {
        public CourseRepository(AppDbContext context, IHttpContextAccessor httpContext) : base(context, httpContext)
        {
        }
        private AppDbContext AppDbContext => Context as AppDbContext;
        public Task<bool> IsCourseCodeExist(string course, int? excludeId = null)
        {
            return AppDbContext.Course
                .AnyAsync(s => s.CourseCode == course && (excludeId == null || s.CourseId != excludeId));
        }
    }
}
