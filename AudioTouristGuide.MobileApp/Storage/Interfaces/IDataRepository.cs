using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AudioTouristGuide.MobileApp.Storage.Interfaces
{
    public interface IDataRepository
    {
        long Add<T>(T item) where T : IStorageItem;
        T GetById<T>(long id) where T : IStorageItem;
        T FirstOrDefault<T>() where T : IStorageItem;
        T FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : IStorageItem;
        IEnumerable<T> GetAll<T>() where T : IStorageItem;
        IEnumerable<T> GetAll<T>(Expression<Func<T, bool>> predicate) where T : IStorageItem;
        bool Update<T>(T item) where T : IStorageItem;
        bool Delete<T>(long id) where T : IStorageItem;
        int Delete<T>(Expression<Func<T, bool>> predicate) where T : IStorageItem;
        bool DeleteAll<T>() where T : IStorageItem;
    }
}
