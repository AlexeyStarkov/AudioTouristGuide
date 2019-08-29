using System;
using System.Linq;
using System.Linq.Expressions;
using AudioTouristGuide.WebAPI.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AudioTouristGuide.WebAPI.Database.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DatabaseContext DBContext { get; set; }

        protected RepositoryBase(DatabaseContext dbContext)
        {
            DBContext = dbContext;
        }

        public IQueryable<T> FindAll()
        {
            return DBContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return DBContext.Set<T>().Where(expression).AsNoTracking();
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
    }
}