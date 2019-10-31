using System;
using System.IO;
using AudioTouristGuide.MobileApp.Storage.Interfaces;

namespace AudioTouristGuide.MobileApp.Storage.Repositories
{
    public class FileRepository : RepositoryBase, IFileRepository
    {
        public string Add(MemoryStream stream, string filename)
        {
            var liteFileInfo = LiteRepository.FileStorage.Upload(Guid.NewGuid().ToString(), filename, stream);
            return liteFileInfo.Id;
        }

        public MemoryStream GetFileById(string id)
        {
            var stream = new MemoryStream();
            LiteRepository.FileStorage.Download(id, stream);
            return stream;
        }

        public bool Delete(string id)
        {
            return LiteRepository.FileStorage.Delete(id);
        }
    }
}
