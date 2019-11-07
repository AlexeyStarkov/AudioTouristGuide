using System;

namespace AudioTouristGuide.DTO.Models.Tour
{
    public abstract class DTOAssetBaseModel
    {
        public string Name { get; }
        public string Description { get; }
        public string AssetFileUrl { get; }
        public DateTime LastUpdate { get; }

        protected DTOAssetBaseModel(string name, string description, string assetFileUrl, DateTime lastUpdate)
        {
            Name = name;
            Description = description;
            AssetFileUrl = assetFileUrl;
            LastUpdate = lastUpdate;
        }
    }
}
