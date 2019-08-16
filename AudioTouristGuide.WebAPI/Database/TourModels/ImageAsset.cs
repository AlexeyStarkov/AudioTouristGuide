using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AudioTouristGuide.WebAPI.Database.TourModels
{
    public class ImageAsset : AssetBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ImageAssetId { get; set; }
        public TimeSpan PointOfDisplayingStart { get; set; }

        public long PlaceId { get; set; }
        public Place Place { get; set; }
    }
}
