using System.Collections.Generic;

namespace AudioTouristGuide.DTO.Models.Tour
{
    public class DTOPlaceModel
    {
        public long PlaceId { get; }
        public string Name { get; }
        public string DisplayName { get; }
        public string Description { get; }
        public double Latitude { get; }
        public double Longitude { get; }
        public long DataSize { get; }

        public DTOAudioAssetModel AudioAsset { get; }
        public IEnumerable<DTOImageAssetModel> ImageAssets { get; }

        public DTOPlaceModel(long placeId, string name, string displayName, string description, double latitude, double longitude, long dataSize, DTOAudioAssetModel audioAsset, IEnumerable<DTOImageAssetModel> imageAssets)
        {
            PlaceId = placeId;
            Name = name;
            DisplayName = displayName;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
            DataSize = dataSize;
            AudioAsset = audioAsset;
            ImageAssets = imageAssets;
        }
    }
}
