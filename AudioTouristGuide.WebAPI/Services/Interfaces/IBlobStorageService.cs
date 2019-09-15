using System.Threading.Tasks;
using AudioTouristGuide.WebAPI.Storage.Models;

namespace AudioTouristGuide.WebAPI.Services.Interfaces
{
    public interface IBlobStorageService
    {
        Task<FileUploadResult> UploadFileAsync(string containerName, string filePath, string fileName);
        string GetFileTokenizedUrl(string containerName, string fileName);
        Task RemoveContainerAsync(string containerName);
        BlobContainerInfo GetBlobContainerInfo(string containerName);
    }
}
