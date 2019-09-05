using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AudioTouristGuide.WebAPI.Services.Interfaces;
using AudioTouristGuide.WebAPI.Tools;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Options;

namespace AudioTouristGuide.WebAPI.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly AzureBlobStorageConfig _blobStorageConfig;

        public BlobStorageService(IOptions<AzureBlobStorageConfig> blobStorageConfig)
        {
            _blobStorageConfig = blobStorageConfig.Value;
        }

        public async Task<bool> UploadFileAsync(string containerName, string filePath)
        {
            if (string.IsNullOrEmpty(containerName) || string.IsNullOrEmpty(filePath))
                return false;

            if (CloudStorageAccount.TryParse(_blobStorageConfig.ConnectionString, out CloudStorageAccount storageAccount))
            {
                var blobClient = storageAccount.CreateCloudBlobClient();
                if (blobClient == null)
                    return false;

                var container = blobClient.GetContainerReference(containerName.ToLower());
                
                if (await container.CreateIfNotExistsAsync())
                {
                    await container.SetPermissionsAsync(new BlobContainerPermissions() { PublicAccess = BlobContainerPublicAccessType.Blob });

                    string fileName = filePath.Split('/').Last();
                    var cloudBlockBlob = container.GetBlockBlobReference(fileName);
                    if (cloudBlockBlob == null)
                        return false;

                    await cloudBlockBlob.UploadFromFileAsync(filePath);
                }
                return false;
            }
            return false;
        }

        public string GetFileUrlAsync(string containerName, string fileName)
        {
            if (string.IsNullOrEmpty(containerName) || string.IsNullOrEmpty(fileName))
                return null;

            if (CloudStorageAccount.TryParse(_blobStorageConfig.ConnectionString, out CloudStorageAccount storageAccount))
            {
                var blobClient = storageAccount.CreateCloudBlobClient();
                if (blobClient == null)
                    return null;
                var container = blobClient.GetContainerReference(containerName.ToLower());

                var cloudBlockBlob = container.GetBlockBlobReference(fileName);
                return cloudBlockBlob.SnapshotQualifiedUri.AbsoluteUri;
            }
            return null;
        }
    }
}
