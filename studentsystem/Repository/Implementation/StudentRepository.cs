using Microsoft.EntityFrameworkCore;
using studentsystem.Data;
using studentsystem.Entites;
using studentsystem.Repository.Interface;

namespace studentsystem.Repository.Implementation
{
    public class StudentRepository : Repository<Student, int>, IStudentRepository
    {
        public StudentRepository(AppDbContext context, IHttpContextAccessor httpContext) : base(context, httpContext)
        {
        }
        private AppDbContext AppDbContext => Context as AppDbContext;
        public Task<bool> IsEmailExist(string email)
        {
            return AppDbContext.Student
                .AnyAsync(s => s.Email == email);
        }
        public async Task<List<Student>> GetRecentAsync(DateOnly fromDate)
        {
            var recentStudents = await AppDbContext.Student
                .Where(s => s.EnrollmentDate >= fromDate)
                .OrderByDescending(s => s.EnrollmentDate)
                .Take(5)
                .ToListAsync();
            return recentStudents;
        }


    }
}
