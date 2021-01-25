using AudioTouristGuide.MobileApp.Models.BaseClasses;

namespace AudioTouristGuide.MobileApp.Storage.Models
{
    public class ATGAudioAssetDBModel : ATGAssetBaseDBModel
    {
        public long AudioAssetId { get; }

        //public ATGAudioAssetDBModel(DTOAudioAssetModel dtoAudioAssetModel) : base(dtoAudioAssetModel)
        //{
        //    AudioAssetId = dtoAudioAssetModel.AudioAssetId;
        //}

        public ATGAudioAssetDBModel() { }
    }
}
