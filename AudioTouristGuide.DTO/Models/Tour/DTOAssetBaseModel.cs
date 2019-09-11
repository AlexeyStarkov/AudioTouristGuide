namespace AudioTouristGuide.DTO.Models.Tour
{
    public abstract class DTOAssetBaseModel
    {
        public string Name { get; }
        public string Description { get; }
        public string AssetFileUrl { get; }

        protected DTOAssetBaseModel(string name, string description, string assetFileUrl)
        {
            Name = name;
            Description = description;
            AssetFileUrl = assetFileUrl;
        }
    }
}
