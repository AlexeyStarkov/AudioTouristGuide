using System;

namespace AudioTouristGuide.WebAPI.Database.Entities.TourModels
{
    public class PlaceImageAssetDbModel : AssetDbModelBase
    {
        public TimeSpan PointOfDisplayingStart { get; set; }

        public long PlaceDbModelId { get; set; }
        public PlaceDbModel PlaceDbModel { get; set; }
    }
}
