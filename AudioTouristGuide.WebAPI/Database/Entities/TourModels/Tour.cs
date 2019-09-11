using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels;

namespace AudioTouristGuide.WebAPI.Database.Entities.TourModels
{
    public class Tour
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TourId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan EstimatedDuration { get; set; }
        public string CountryName { get; set; }
        public long DataSize { get; set; }
        public decimal? GrossPrice { get; set; }

        public string AssetsContainerName { get; set; }
        public string LogoFileName { get; set; }

        public ICollection<TourPlace> TourPlaces { get; set; }
        public ICollection<MemberPurchasedTour> MemberPurchasedTours { get; set; }
        public ICollection<MemberFavoriteTour> MemberFavoriteTours { get; set; }
        public ICollection<MemberDesiredTour> MemberDesiredTours { get; set; }
    }
}
