using studentsystem.Entites;

namespace studentsystem.Repository.Interface
{
    public interface IStudentRepository : IRepository<Student, int>
    {
        Task<bool> IsEmailExist(string email);
        Task<List<Student>> GetRecentAsync(DateOnly fromDate);
    }
}
