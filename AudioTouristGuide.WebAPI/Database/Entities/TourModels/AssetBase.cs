namespace AudioTouristGuide.WebAPI.Database.Entities.TourModels
{
    public abstract class AssetBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string AssetFileUrl { get; set; }
    }
}
