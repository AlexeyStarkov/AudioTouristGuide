using System;
namespace AudioTouristGuide.DTO.Models.Tour
{
    public class DTOImageAssetModel : DTOAssetBaseModel
    {
        public long ImageAssetId { get; }

        public DTOImageAssetModel(long imageAssetId, string name, string description, string assetFileUrl, DateTime lastUpdate) : base(name, description, assetFileUrl, lastUpdate)
        {
            ImageAssetId = imageAssetId;
            
        }
    }
}
