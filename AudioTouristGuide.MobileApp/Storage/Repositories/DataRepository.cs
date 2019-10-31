using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AudioTouristGuide.MobileApp.Storage.Interfaces;
using LiteDB;
using Xamarin.Essentials;

namespace AudioTouristGuide.MobileApp.Storage.Repositories
{
    public class DataRepository : IRepository
    {
        private const string DbFileName = "storage.db";
        private const string DbPassword = "$U3R'V67g^@`$Bv#";
        private LiteRepository _liteRepository;

        private bool _disposing;

        public DataRepository()
        {
            var dbFilePath = $"{FileSystem.AppDataDirectory}/{DbFileName}";
            _liteRepository = new LiteRepository(new ConnectionString($"Filename={dbFilePath};Password={DbPassword}"));
        }

        public long Add<T>(T item) where T : IStorageItem
        {
            return _liteRepository.Insert(item);
        }

        public T FirstOrDefault<T>() where T : IStorageItem
        {
            return _liteRepository.FirstOrDefault<T>();
        }

        public T FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : IStorageItem
        {
            return _liteRepository.FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetAll<T>() where T : IStorageItem
        {
            return _liteRepository.Database.GetCollection<T>().FindAll();
        }

        public IEnumerable<T> GetAll<T>(Expression<Func<T, bool>> predicate) where T : IStorageItem
        {
            return _liteRepository.Query<T>().Where(predicate).ToEnumerable();
        }

        public T GetById<T>(long id) where T : IStorageItem
        {
            return _liteRepository.SingleById<T>(id);
        }

        public bool Delete<T>(long id) where T : IStorageItem
        {
            return _liteRepository.Delete<T>(id);
        }

        public int Delete<T>(Expression<Func<T, bool>> predicate) where T : IStorageItem
        {
            return _liteRepository.Delete(predicate);
        }

        public bool DeleteAll<T>() where T : IStorageItem
        {
            return _liteRepository.Database.DropCollection(nameof(T));
        }

        public void Dispose()
        {
            if (!_disposing && _liteRepository != null)
            {
                _disposing = true;
                _liteRepository.Dispose();
                _liteRepository = null;
            }
        }
    }
}
