using System;
namespace AudioTouristGuide.DTO.Models.Tour
{
    public class DTOImageAssetModel : DTOAssetBaseModel
    {
        public long ImageAssetId { get; }
        public TimeSpan PointOfDisplayingStart { get; }

        public DTOImageAssetModel(long imageAssetId, string name, string description, string assetFileUrl, TimeSpan pointOfDisplayingStart) : base(name, description, assetFileUrl)
        {
            ImageAssetId = imageAssetId;
            PointOfDisplayingStart = pointOfDisplayingStart;
        }
    }
}
