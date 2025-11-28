using System.Linq.Expressions;

namespace studentsystem.Repository.Interface
{
    public interface IRepository<TEntity, in TPrimaryKeyType> where TEntity : class
    {
        ValueTask<TEntity> GetByIdAsync(TPrimaryKeyType id);
        Task<List<TEntity>> GetAllAsync();
        void Update(TEntity entity);
        void Remove(TEntity entity);
        Task<int> Save();
        void Add(TEntity entity);
        Task<int> Count();


    }
}
