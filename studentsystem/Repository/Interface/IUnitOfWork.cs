namespace studentsystem.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        ICourseRepository CourseRepository { get; }
        IEnrollmentRepository EnrollmentRepository { get; }
        IStudentRepository StudentRepository { get; }
    }
}
