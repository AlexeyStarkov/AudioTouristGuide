using System;
using System.IO;

namespace AudioTouristGuide.MobileApp.Storage.Interfaces
{
    public interface IFileRepository
    {
        string Add(string id, string filePath);
        MemoryStream GetFileById(string id);
        bool Delete(string id);
    }
}
