namespace AudioTouristGuide.DTO.Models.Tour
{
    public class DTOAudioAssetModel : DTOAssetBaseModel
    {
        public long AudioAssetId { get; }

        public DTOAudioAssetModel(long audioAssetId, string name, string description, string assetFileUrl) : base(name, description, assetFileUrl)
        {
            AudioAssetId = audioAssetId;
        }
    }
}
