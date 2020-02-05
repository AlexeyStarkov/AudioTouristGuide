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
            var containerFilesCount = Directory.GetFiles(containerPath).Count();

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
                Directory.Delete(Path.Combine(_storagePath, containerName), true);
            });
        }

        public async Task<FileSavingResult> SaveFileAsync(string containerName, string filePath, string fileName)
        {
            try
            {
                await Task.Run(() =>
                {
                    Directory.CreateDirectory(Path.Combine(_storagePath, containerName));
                    File.Copy(filePath, Path.Combine(_storagePath, containerName, fileName), true);
                });

                return new FileSavingResult(true, fileName, containerName);
            }
            catch (Exception ex)
            {
                return new FileSavingResult(false, fileName, containerName);
            }
        }
    }
}
