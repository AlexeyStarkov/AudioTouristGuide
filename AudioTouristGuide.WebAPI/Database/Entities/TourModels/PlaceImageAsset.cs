using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AudioTouristGuide.WebAPI.Database.Entities.TourModels
{
    public class PlaceImageAsset : AssetBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PlaceImageAssetId { get; set; }
        public TimeSpan PointOfDisplayingStart { get; set; }

        public long PlaceId { get; set; }
        public Place Place { get; set; }
    }
}
