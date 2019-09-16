using System;
using System.Linq;
using System.Threading.Tasks;
using AudioTouristGuide.WebAPI.Services.Interfaces;
using AudioTouristGuide.WebAPI.Storage.Models;
using AudioTouristGuide.WebAPI.Tools;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Options;

namespace AudioTouristGuide.WebAPI.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly AzureBlobStorageConfig _blobStorageConfig;
        private readonly CloudBlobClient _cloudBlobClient;

        public BlobStorageService(IOptions<AzureBlobStorageConfig> blobStorageConfig)
        {
            _blobStorageConfig = blobStorageConfig.Value;
            CloudStorageAccount.TryParse(_blobStorageConfig.ConnectionString, out CloudStorageAccount storageAccount);
            _cloudBlobClient = storageAccount?.CreateCloudBlobClient();

        }

        public async Task<FileUploadResult> UploadFileAsync(string containerName, string filePath, string fileName)
        {
            if (string.IsNullOrEmpty(containerName) || string.IsNullOrEmpty(filePath) || _cloudBlobClient == null)
                return new FileUploadResult(false, fileName, containerName);

            var lowerCaseContainerName = containerName.ToLower();

            var container = _cloudBlobClient.GetContainerReference(lowerCaseContainerName);
            await container.CreateIfNotExistsAsync();

            await container.SetPermissionsAsync(new BlobContainerPermissions() { PublicAccess = BlobContainerPublicAccessType.Off });

            var cloudBlockBlob = container.GetBlockBlobReference(fileName);
            if (cloudBlockBlob == null)
                return new FileUploadResult(false, fileName, lowerCaseContainerName);

            await cloudBlockBlob.UploadFromFileAsync(filePath);
            return new FileUploadResult(true, fileName, lowerCaseContainerName);
        }

        public string GetFileTokenizedUrl(string containerName, string fileName)
        {
            if (string.IsNullOrEmpty(containerName) || string.IsNullOrEmpty(fileName) || _cloudBlobClient == null)
                return null;

            var container = _cloudBlobClient.GetContainerReference(containerName.ToLower());

            var cloudBlockBlob = container.GetBlockBlobReference(fileName);

            var storedPolicy = new SharedAccessBlobPolicy()
            {
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(2),
                Permissions = SharedAccessBlobPermissions.Read
            };
            string accessSignature = cloudBlockBlob.GetSharedAccessSignature(storedPolicy);

            return cloudBlockBlob.SnapshotQualifiedUri.AbsoluteUri + accessSignature;
        }

        public async Task RemoveContainerAsync(string containerName)
        {
            if (!string.IsNullOrEmpty(containerName) || _cloudBlobClient == null)
            {
                var container = _cloudBlobClient.GetContainerReference(containerName.ToLower());
                await container.DeleteAsync();
            }
        }

        public BlobContainerInfo GetBlobContainerInfo(string containerName)
        {
            if (string.IsNullOrEmpty(containerName) || _cloudBlobClient == null)
                return null;

            var container = _cloudBlobClient.GetContainerReference(containerName.ToLower());
            var containerInfo = new BlobContainerInfo()
            {
                FileCount = 0,
                DirectoryCount = 0,
                TotalBytes = 0
            };

            foreach (var blobItem in container.ListBlobs(null, true, BlobListingDetails.None))
            {
                if (blobItem is CloudBlockBlob)
                {
                    CloudBlockBlob blob = blobItem as CloudBlockBlob;
                    containerInfo.FileCount++;
                    containerInfo.TotalBytes += blob.Properties.Length;
                }
                else if (blobItem is CloudPageBlob)
                {
                    CloudPageBlob pageBlob = blobItem as CloudPageBlob;

                    containerInfo.FileCount++;
                    containerInfo.TotalBytes += pageBlob.Properties.Length;
                }
                else if (blobItem is CloudBlobDirectory)
                {
                    CloudBlobDirectory directory = blobItem as CloudBlobDirectory;

                    containerInfo.DirectoryCount++;
                }
            }
            return containerInfo;
        }
    }
}
