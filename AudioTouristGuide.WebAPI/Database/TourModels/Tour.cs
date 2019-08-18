using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AudioTouristGuide.DTO.Enums;
using AudioTouristGuide.WebAPI.Database.JoinTablesModels;

namespace AudioTouristGuide.WebAPI.Database.TourModels
{
    public class Tour
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TourId { get; set; }
        public string AssetsFolderGuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan EstimatedDuration { get; set; }
        public string CountryName { get; set; }
        public long DataSize { get; set; }
        public decimal? GrossPrice { get; set; }
        public string LogoUrl { get; set; }

        public ICollection<TourPlace> TourPlaces { get; set; }
        public ICollection<MemberPurchasedTour> MemberPurchasedTours { get; set; }
        public ICollection<MemberFavoriteTour> MemberFavoriteTours { get; set; }
        public ICollection<MemberDesiredTour> MemberDesiredTours { get; set; }
    }
}
