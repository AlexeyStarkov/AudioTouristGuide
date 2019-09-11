namespace AudioTouristGuide.WebAPI.Database.Entities.TourModels
{
    public abstract class AssetBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string AssetContainerName { get; set; }
        public string AssetFileName { get; set; }
    }
}
