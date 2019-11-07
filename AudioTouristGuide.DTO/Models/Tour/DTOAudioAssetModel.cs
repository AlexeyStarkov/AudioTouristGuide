using System;

namespace AudioTouristGuide.DTO.Models.Tour
{
    public class DTOAudioAssetModel : DTOAssetBaseModel
    {
        public long AudioAssetId { get; }

        public DTOAudioAssetModel(long audioAssetId, string name, string description, string assetFileUrl, DateTime lastUpdate) : base(name, description, assetFileUrl, lastUpdate)
        {
            AudioAssetId = audioAssetId;
        }
    }
}
