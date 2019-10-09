using System;
using AudioTouristGuide.DTO.Models.Tour;
using AudioTouristGuide.MobileApp.Models.BaseClasses;

namespace AudioTouristGuide.MobileApp.Models
{
    public class ATGImageAssetModel : ATGAssetBaseModel
    {
        public long ImageAssetId { get; }
        public TimeSpan PointOfDisplayingStart { get; }

        public ATGImageAssetModel(DTOImageAssetModel dtoImageAssetModel) : base(dtoImageAssetModel)
        {
            ImageAssetId = dtoImageAssetModel.ImageAssetId;
            PointOfDisplayingStart = dtoImageAssetModel.PointOfDisplayingStart;
        }
    }
}
