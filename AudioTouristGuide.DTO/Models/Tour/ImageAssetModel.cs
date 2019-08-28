using System;
namespace AudioTouristGuide.DTO.Models.Tour
{
    public class ImageAssetModel : AssetBaseModel
    {
        public long ImageAssetId { get; }
        public TimeSpan PointOfDisplayingStart { get; }

        public ImageAssetModel(long imageAssetId, string name, string description, string assetFileUrl, TimeSpan pointOfDisplayingStart) : base(name, description, assetFileUrl)
        {
            ImageAssetId = imageAssetId;
            PointOfDisplayingStart = pointOfDisplayingStart;
        }
    }
}
