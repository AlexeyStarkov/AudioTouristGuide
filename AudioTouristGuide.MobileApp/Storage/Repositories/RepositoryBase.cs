using System;
using LiteDB;
using Xamarin.Essentials;

namespace AudioTouristGuide.MobileApp.Storage.Repositories
{
    public abstract class RepositoryBase : IDisposable
    {
        private const string DbFileName = "storage.db";
        private const string DbPassword = "$U3R'V67g^@`$Bv#";
        protected LiteRepository LiteRepository;

        private bool _disposing;

        protected RepositoryBase()
        {
            var dbFilePath = $"{FileSystem.AppDataDirectory}/{DbFileName}";
            LiteRepository = new LiteRepository(new ConnectionString($"Filename={dbFilePath};Password={DbPassword}"));
        }

        public void Dispose()
        {
            if (!_disposing && LiteRepository != null)
            {
                _disposing = true;
                LiteRepository.Dispose();
                LiteRepository = null;
            }
        }
    }
}
