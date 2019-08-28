namespace AudioTouristGuide.DTO.Models.Tour
{
    public class AudioAssetModel : AssetBaseModel
    {
        public long AudioAssetId { get; }

        public AudioAssetModel(long audioAssetId, string name, string description, string assetFileUrl) : base(name, description, assetFileUrl)
        {
            AudioAssetId = audioAssetId;
        }
    }
}
