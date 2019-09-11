namespace AudioTouristGuide.WebAPI.Storage.Models
{
    public class BlobContainerInfo
    {
        public int FileCount { get; set; }
        public int DirectoryCount { get; set; }
        public long TotalBytes { get; set; }
    }
}
