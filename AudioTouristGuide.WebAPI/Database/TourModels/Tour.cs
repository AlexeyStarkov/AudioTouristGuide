using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AudioTouristGuide.DTO.Enums;
using AudioTouristGuide.WebAPI.Database.JoinTablesModels;
using Newtonsoft.Json;

namespace AudioTouristGuide.WebAPI.Database.TourModels
{
    public class Tour
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TourId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan EstimatedDuration { get; set; }
        public Countries Country { get; set; }
        public long Size { get; set; }
        public decimal GrossPrice { get; set; }
        public string LogoUrl { get; set; }

        public ICollection<TourPlace> TourPlaces { get; set; }

        [JsonIgnore]
        public ICollection<MemberPurchasedTour> MemberPurchasedTours { get; set; }
        [JsonIgnore]
        public ICollection<MemberFavoriteTour> MemberFavoriteTours { get; set; }
        [JsonIgnore]
        public ICollection<MemberDesiredTour> MemberDesiredTours { get; set; }
    }
}
