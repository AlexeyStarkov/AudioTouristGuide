using System;

namespace AudioTouristGuide.WebAPI.Database.Entities.TourModels
{
    public abstract class AssetDbModelBase : DbModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string AssetContainerName { get; set; }
        public string AssetFileName { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
