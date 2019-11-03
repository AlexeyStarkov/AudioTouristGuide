using System.IO;
using AudioTouristGuide.MobileApp.Storage.Interfaces;

namespace AudioTouristGuide.MobileApp.Storage.Repositories
{
    public class FileRepository : RepositoryBase, IFileRepository
    {
        public string Add(string id, string filePath)
        {
            var liteFileInfo = LiteRepository.FileStorage.Upload(id, filePath);
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
