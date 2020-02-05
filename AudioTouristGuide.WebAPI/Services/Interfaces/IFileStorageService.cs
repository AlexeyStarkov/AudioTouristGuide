using System.Threading.Tasks;
using AudioTouristGuide.WebAPI.Storage.Models;

namespace AudioTouristGuide.WebAPI.Services.Interfaces
{
    public interface IFileStorageService
    {
        Task<FileUploadResult> SaveFileAsync(string containerName, string filePath, string fileName);
        string GetFileUrl(string containerName, string fileName);
        Task RemoveFileContainerAsync(string containerName);
        FileContainerInfo GetFileContainerInfo(string containerName);
    }
}
