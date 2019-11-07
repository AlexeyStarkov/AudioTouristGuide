using System;
using System.Collections.Generic;

namespace AudioTouristGuide.MobileApp.Models
{
    public class ATGTourPlaceUIModel
    {
        public long PlaceId { get; }
        public string Name { get; }
        public string DisplayName { get; }
        public string Description { get; }
        public double Latitude { get; }
        public double Longitude { get; }
        public int DataSize { get; }

        public ATGAudioAssetUIModel AudioAsset { get; }
        public IEnumerable<ATGImageAssetUIModel> ImageAssets { get; }

        public bool HasUpdate { get; set; }
    }
}
