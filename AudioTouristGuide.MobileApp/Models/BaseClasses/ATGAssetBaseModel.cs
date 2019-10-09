using AudioTouristGuide.DTO.Models.Tour;

namespace AudioTouristGuide.MobileApp.Models.BaseClasses
{
    public abstract class ATGAssetBaseModel
    {
        public string Name { get; }
        public string Description { get; }
        public string AssetFileUrl { get; }

        public ATGAssetBaseModel(DTOAssetBaseModel dtoAssetBaseModel)
        {
            Name = dtoAssetBaseModel.Name;
            Description = dtoAssetBaseModel.Description;
            AssetFileUrl = dtoAssetBaseModel.AssetFileUrl;
        }
    }
}
