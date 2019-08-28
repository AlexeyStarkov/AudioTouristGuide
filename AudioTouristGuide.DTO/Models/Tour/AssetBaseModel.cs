namespace AudioTouristGuide.DTO.Models.Tour
{
    public abstract class AssetBaseModel
    {
        public string Name { get; }
        public string Description { get; }
        public string AssetFileUrl { get; }

        protected AssetBaseModel(string name, string description, string assetFileUrl)
        {
            Name = name;
            Description = description;
            AssetFileUrl = assetFileUrl;
        }
    }
}
