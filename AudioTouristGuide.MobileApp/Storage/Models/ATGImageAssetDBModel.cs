using AudioTouristGuide.DTO.Models.Tour;
using AudioTouristGuide.MobileApp.Models.BaseClasses;

namespace AudioTouristGuide.MobileApp.Storage.Models
{
    public class ATGImageAssetDBModel : ATGAssetBaseDBModel
    {
        public long ImageAssetId { get; }

        public ATGImageAssetDBModel(DTOImageAssetModel dtoImageAssetModel) : base(dtoImageAssetModel)
        {
            ImageAssetId = dtoImageAssetModel.ImageAssetId;
        }
    }
}
