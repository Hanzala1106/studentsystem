using Microsoft.EntityFrameworkCore;
using studentsystem.Repository.Interface;
using System.Linq.Expressions;
using System.Reflection;

namespace studentsystem.Repository.Implementation
{
    public class Repository<TEntity, TPrimaryKeyType> : IRepository<TEntity, TPrimaryKeyType> where TEntity : class
    {

        protected readonly DbContext Context;
        protected readonly IHttpContextAccessor _httpContext;
        public Repository(DbContext context, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            _httpContext = httpContextAccessor;
        }

        public void Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
        }
        public async Task<List<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public ValueTask<TEntity> GetByIdAsync(TPrimaryKeyType id)
        {
            return Context.Set<TEntity>().FindAsync(id);
        }
        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }
        public async Task Save()
        {
            Context.SaveChangesAsync();
        }
        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().AddAsync(entity);
        }
        public async Task<int> Count()
        {
            return await Context.Set<TEntity>().CountAsync();
        }

    }
}
