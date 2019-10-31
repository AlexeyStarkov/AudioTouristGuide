using System.IO;

namespace AudioTouristGuide.MobileApp.Storage.Interfaces
{
    public interface IFileRepository
    {
        string Add(MemoryStream stream, string filename);
        MemoryStream GetFileById(string id);
        bool Delete(string id);
    }
}
