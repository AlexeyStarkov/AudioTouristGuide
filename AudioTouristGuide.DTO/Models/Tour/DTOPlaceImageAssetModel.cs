using System;
namespace AudioTouristGuide.DTO.Models.Tour
{
    public class DTOPlaceImageAssetModel : DTOAssetBaseModel
    {
        public long PlaceImageAssetId { get; }
        public long PointOfDisplayingStartTicks { get; }

        public DTOPlaceImageAssetModel(long placeImageAssetId, string name, string description, string assetFileUrl, TimeSpan pointOfDisplayingStart, DateTime lastUpdate) : base(name, description, assetFileUrl, lastUpdate)
        {
            PlaceImageAssetId = placeImageAssetId;
            PointOfDisplayingStartTicks = pointOfDisplayingStart.Ticks;
        }
    }
}
