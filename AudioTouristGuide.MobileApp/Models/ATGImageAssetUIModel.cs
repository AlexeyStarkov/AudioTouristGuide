using System;
using FFImageLoading.Work;

namespace AudioTouristGuide.MobileApp.Models
{
    public class ATGImageAssetUIModel
    {
        public long ImageAssetId { get; set; }
        public TimeSpan PointOfDisplayingStart { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ImageSource Image { get; set; }

    }
}
