namespace AudioTouristGuide.WebAPI.Storage.Models
{
    public class FileUploadResult
    {
        public bool HasSuccess { get; }
        public string FileName { get; }

        public FileUploadResult(bool hasSuccess, string fileName)
        {
            HasSuccess = hasSuccess;
            FileName = fileName;
        }
    }
}
