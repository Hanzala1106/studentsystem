using Microsoft.AspNetCore.Identity;
using studentsystem.Data;
using studentsystem.Repository.Interface;

namespace studentsystem.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IHttpContextAccessor _httpAccessor;

        public UnitOfWork(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpAccessor = httpContextAccessor;
        }

        public IStudentRepository StudentRepository => _studentRepository ?? new StudentRepository(_context, _httpAccessor);
        public ICourseRepository CourseRepository => _courseRepository ?? new CourseRepository(_context, _httpAccessor);
        public IEnrollmentRepository EnrollmentRepository => _enrollmentRepository ?? new EnrollmentRepository(_context, _httpAccessor);
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
