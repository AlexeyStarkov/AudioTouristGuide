using System.Collections.Generic;
using System.Linq;
using AudioTouristGuide.DTO.Models.Tour;

namespace AudioTouristGuide.MobileApp.Models
{
    public class ATGPlaceModel
    {
        public long PlaceId { get; }
        public string Name { get; }
        public string DisplayName { get; }
        public string Description { get; }
        public double Latitude { get; }
        public double Longitude { get; }
        public int DataSize { get; }

        public ATGAudioAssetModel AudioAsset { get; }
        public IEnumerable<ATGImageAssetModel> ImageAssets { get; }

        public bool HasUpdate => true;

        public ATGPlaceModel(DTOPlaceModel dtoPlaceModel)
        {
            PlaceId = dtoPlaceModel.PlaceId;
            Name = dtoPlaceModel.Name;
            DisplayName = dtoPlaceModel.DisplayName;
            Description = dtoPlaceModel.Description;
            Latitude = dtoPlaceModel.Latitude;
            Longitude = dtoPlaceModel.Longitude;
            DataSize = (int)(dtoPlaceModel.DataSize / 1048576); //bytes to mb convertation
            AudioAsset = new ATGAudioAssetModel(dtoPlaceModel.AudioAsset);
            ImageAssets = dtoPlaceModel.ImageAssets.Select(x => new ATGImageAssetModel(x));
        }
    }
}
