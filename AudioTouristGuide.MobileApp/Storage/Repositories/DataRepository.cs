using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AudioTouristGuide.MobileApp.Storage.Interfaces;
using LiteDB;

namespace AudioTouristGuide.MobileApp.Storage.Repositories
{
    public class DataRepository : RepositoryBase, IDataRepository
    {
        public long Add<T>(T item) where T : IStorageItem
        {
            return LiteRepository.Insert(item);
        }

        public T FirstOrDefault<T>() where T : IStorageItem
        {
            return LiteRepository.FirstOrDefault<T>();
        }

        public T FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : IStorageItem
        {
            return LiteRepository.FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetAll<T>() where T : IStorageItem
        {
            return LiteRepository.Database.GetCollection<T>().FindAll();
        }

        public IEnumerable<T> GetAll<T>(Expression<Func<T, bool>> predicate) where T : IStorageItem
        {
            return LiteRepository.Query<T>().Where(predicate).ToEnumerable();
        }

        public T GetById<T>(long id) where T : IStorageItem
        {
            return LiteRepository.SingleById<T>(id);
        }

        public bool Update<T>(T item) where T : IStorageItem
        {
            return LiteRepository.Update(item);
        }

        public bool Delete<T>(long id) where T : IStorageItem
        {
            return LiteRepository.Delete<T>(id);
        }

        public int Delete<T>(Expression<Func<T, bool>> predicate) where T : IStorageItem
        {
            return LiteRepository.Delete(predicate);
        }

        public bool DeleteAll<T>() where T : IStorageItem
        {
            return LiteRepository.Database.DropCollection(nameof(T));
        }

        
    }
}
