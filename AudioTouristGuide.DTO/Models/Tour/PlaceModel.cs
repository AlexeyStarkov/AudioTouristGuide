using System;
using System.Collections.Generic;

namespace AudioTouristGuide.DTO.Models.Tour
{
    public class PlaceModel
    {
        public long PlaceId { get; }
        public string Name { get; }
        public string DisplayName { get; }
        public string Description { get; }
        public double Latitude { get; }
        public double Longitude { get; }
        public long DataSize { get; }

        public AudioAssetModel AudioAsset { get; }
        public IEnumerable<ImageAssetModel> ImageAssets { get; }

        public PlaceModel(long placeId, string name, string displayName, string description, double latitude, double longitude, long dataSize, AudioAssetModel audioAsset, IEnumerable<ImageAssetModel> imageAssets)
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
