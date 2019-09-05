using System;
using System.Threading.Tasks;

namespace AudioTouristGuide.WebAPI.Services.Interfaces
{
    public interface IBlobStorageService
    {
        Task<bool> UploadFileAsync(string containerName, string filePath);
        string GetFileUrlAsync(string containerName, string fileName);
    }
}
