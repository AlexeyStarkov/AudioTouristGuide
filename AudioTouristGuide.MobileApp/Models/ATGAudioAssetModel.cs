using AudioTouristGuide.DTO.Models.Tour;
using AudioTouristGuide.MobileApp.Models.BaseClasses;

namespace AudioTouristGuide.MobileApp.Models
{
    public class ATGAudioAssetModel : ATGAssetBaseModel
    {
        public long AudioAssetId { get; }

        public ATGAudioAssetModel(DTOAudioAssetModel dtoAudioAssetModel) : base(dtoAudioAssetModel)
        {
            AudioAssetId = dtoAudioAssetModel.AudioAssetId;
        }
    }
}
