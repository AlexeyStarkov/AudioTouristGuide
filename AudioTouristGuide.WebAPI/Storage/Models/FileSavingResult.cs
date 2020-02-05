namespace AudioTouristGuide.WebAPI.Storage.Models
{
    public class FileSavingResult
    {
        public bool HasSuccess { get; }
        public string FileName { get; }
        public string ContainerName { get; }

        public FileSavingResult(bool hasSuccess, string fileName, string containerName)
        {
            HasSuccess = hasSuccess;
            FileName = fileName;
            ContainerName = containerName;
        }
    }
}
