using System;
using System.IO;

namespace AudioTouristGuide.MobileApp.Models
{
    public class ATGAudioAssetUIModel
    {
        public long AudioAssetId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Stream AudioTrack { get; set; }
    }
}
