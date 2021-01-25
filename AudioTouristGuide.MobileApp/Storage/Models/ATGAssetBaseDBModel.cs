using System;

namespace AudioTouristGuide.MobileApp.Models.BaseClasses
{
    public abstract class ATGAssetBaseDBModel
    {
        public string Name { get; }
        public string Description { get; }
        public string AssetFileUrl { get; }

        public DateTime? LastUpdate { get; set; }
        public string AssetLocalStorageId { get; set; }

        //protected ATGAssetBaseDBModel(DTOAssetBaseModel dtoAssetBaseModel)
        //{
        //    Name = dtoAssetBaseModel.Name;
        //    Description = dtoAssetBaseModel.Description;
        //    AssetFileUrl = dtoAssetBaseModel.AssetFileUrl;
        //}

        protected ATGAssetBaseDBModel() { }
    }
}
