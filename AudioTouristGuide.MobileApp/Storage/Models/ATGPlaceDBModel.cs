using System.Collections.Generic;
using System.Linq;
using AudioTouristGuide.DTO.Models.Tour;
using AudioTouristGuide.MobileApp.Tools;

namespace AudioTouristGuide.MobileApp.Storage.Models
{
    public class ATGPlaceDBModel
    {
        public long PlaceId { get; }
        public string Name { get; }
        public string DisplayName { get; }
        public string Description { get; }
        public double Latitude { get; }
        public double Longitude { get; }
        public double DataSize { get; }

        public ATGAudioAssetDBModel AudioAsset { get; }
        public IEnumerable<ATGPlaceImageAssetDBModel> PlaceImageAssets { get; }

        public ATGPlaceDBModel(DTOPlaceModel dtoPlaceModel)
        {
            PlaceId = dtoPlaceModel.PlaceId;
            Name = dtoPlaceModel.Name;
            DisplayName = dtoPlaceModel.DisplayName;
            Description = dtoPlaceModel.Description;
            Latitude = dtoPlaceModel.Latitude;
            Longitude = dtoPlaceModel.Longitude;
            DataSize = ATGConverters.BytesToMegabytes(dtoPlaceModel.DataSize);
            AudioAsset = new ATGAudioAssetDBModel(dtoPlaceModel.AudioAsset);
            PlaceImageAssets = dtoPlaceModel.ImageAssets.Select(x => new ATGPlaceImageAssetDBModel(x));
        }

        public ATGPlaceDBModel() { }
    }
}
