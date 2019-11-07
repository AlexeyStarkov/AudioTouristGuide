using AudioTouristGuide.WebAPI.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AudioTouristGuide.WebAPI.Database.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DatabaseContext DBContext { get; set; }

        protected RepositoryBase(DatabaseContext dbContext)
        {
            DBContext = dbContext;
        }

        public void Create(T entity)
        {
            DBContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            DBContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            DBContext.Set<T>().Remove(entity);
        }

        public async Task<int> SaveChangesAsync() => await DBContext.SaveChangesAsync();

        public abstract Task<T> GetByIdAsync(long id);

        public abstract Task<IEnumerable<T>> GetAllAsync();

        public abstract Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);
    }
}