using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AudioTouristGuide.WebAPI.Services.Interfaces;
using AudioTouristGuide.WebAPI.Storage.Models;
using AudioTouristGuide.WebAPI.Tools;

namespace AudioTouristGuide.WebAPI.Services
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _storagePath;

        public LocalFileStorageService()
        {
            _storagePath = ApplicationConstants.FileStorageDirectoryPath;
        }

        public FileContainerInfo GetFileContainerInfo(string containerName)
        {
            var containerPath = Path.Combine(_storagePath, containerName);
            var containerDirectory = new DirectoryInfo(containerPath);
            var containerSize = containerDirectory.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);
            var containerDirectoriesCount = Directory.GetDirectories(containerPath).Count();
            var containerFilesCount = Directory.GetFiles(containerName).Count();

            return new FileContainerInfo()
            {
                FileCount = containerFilesCount,
                DirectoryCount = containerDirectoriesCount,
                TotalBytes = containerSize
            };
        }
        
        public string GetFileUrl(string containerName, string fileName)
        {
            return Path.Combine(_storagePath, containerName, fileName);
        }

        public async Task RemoveFileContainerAsync(string containerName)
        {
            await Task.Run(() =>
            {
                Directory.Delete(Path.Combine(_storagePath, containerName));
            });
        }

        public async Task<FileUploadResult> SaveFileAsync(string containerName, string filePath, string fileName)
        {
            try
            {
                var newFilePath = Path.Combine(_storagePath, containerName, fileName);
                await Task.Run(() =>
                {
                    File.Copy(filePath, newFilePath, true);
                });

                return new FileUploadResult(true, fileName, containerName);
            }
            catch (Exception ex)
            {
                return new FileUploadResult(false, fileName, containerName);
            }
        }
    }
}
