using System;
using AudioTouristGuide.DTO.Models.Tour;
using AudioTouristGuide.MobileApp.Models.BaseClasses;

namespace AudioTouristGuide.MobileApp.Storage.Models
{
    public class ATGPlaceImageAssetDBModel : ATGAssetBaseDBModel
    {
        public long PlaceImageAssetId { get; }
        public TimeSpan? PointOfDisplayingStart { get; }

        public ATGPlaceImageAssetDBModel(DTOPlaceImageAssetModel dtoImageAssetModel) : base(dtoImageAssetModel)
        {
            PlaceImageAssetId = dtoImageAssetModel.PlaceImageAssetId;
            PointOfDisplayingStart = dtoImageAssetModel.PointOfDisplayingStart;
        }
    }
}
