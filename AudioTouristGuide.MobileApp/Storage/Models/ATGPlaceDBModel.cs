using System.Collections.Generic;
using System.Linq;
using AudioTouristGuide.DTO.Models.Tour;
using Xamarin.Essentials;

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
        public int DataSize { get; }

        public ATGAudioAssetDBModel AudioAsset { get; }
        public IEnumerable<ATGImageAssetDBModel> ImageAssets { get; }

        public ATGPlaceDBModel(DTOPlaceModel dtoPlaceModel)
        {
            PlaceId = dtoPlaceModel.PlaceId;
            Name = dtoPlaceModel.Name;
            DisplayName = dtoPlaceModel.DisplayName;
            Description = dtoPlaceModel.Description;
            Latitude = dtoPlaceModel.Latitude;
            Longitude = dtoPlaceModel.Longitude;
            DataSize = (int)(dtoPlaceModel.DataSize / 1048576); //bytes to mb convertation
            AudioAsset = new ATGAudioAssetDBModel(dtoPlaceModel.AudioAsset);
            ImageAssets = dtoPlaceModel.ImageAssets.Select(x => new ATGImageAssetDBModel(x));
        }
    }
}
