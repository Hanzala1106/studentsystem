using Microsoft.EntityFrameworkCore;
using studentsystem.Data;
using studentsystem.Entites;
using studentsystem.Repository.Interface;

namespace studentsystem.Repository.Implementation
{
    public class StudentRepository : Repository<Students, int>, IStudentRepository
    {
        public StudentRepository(AppDbContext context, IHttpContextAccessor httpContext) : base(context, httpContext)
        {
        }
        private AppDbContext AppDbContext => Context as AppDbContext;
        public Task<bool> IsEmailExist(string email, int? excludeId = null)
        {
            return AppDbContext.Students
                .AnyAsync(s => s.Email == email && (excludeId == null || s.StudentId != excludeId));
        }


    }
}
