using studentsystem.Entites;

namespace studentsystem.Repository.Interface
{
    public interface ICourseRepository : IRepository<Course, int>
    {
        Task<bool> IsCourseCodeExist(string course, int? excludeId = null);
    }
}
