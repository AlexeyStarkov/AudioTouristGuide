namespace AudioTouristGuide.WebAPI.Storage.Models
{
    public class FileUploadResult
    {
        public bool HasSuccess { get; }
        public string FileName { get; }
        public string ContainerName { get; }

        public FileUploadResult(bool hasSuccess, string fileName, string containerName)
        {
            HasSuccess = hasSuccess;
            FileName = fileName;
            ContainerName = containerName;
        }
    }
}
