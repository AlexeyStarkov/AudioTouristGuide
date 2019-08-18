namespace AudioTouristGuide.WebAPI.Database.TourModels
{
    public abstract class AssetBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string AssetFileUrl { get; set; }
    }
}
