using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels;

namespace AudioTouristGuide.WebAPI.Database.Entities.TourModels
{
    public class Place
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PlaceId { get; set; }
        public string AssetsContainerName { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public long DataSize { get; set; }

        public AudioAsset AudioAsset { get; set; }

        public ICollection<PlaceImageAsset> PlaceImageAssets { get; set; }
        public ICollection<TourPlace> TourPlaces { get; set; }
    }
}
