using System;
using System.Collections.Generic;
using AudioTouristGuide.DTO.Enums;
using AudioTouristGuide.WebAPI.Database.JoinTablesModels;

namespace AudioTouristGuide.WebAPI.Database.TourModels
{
    public class Tour
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan EstimatedDuration { get; set; }
        public Countries Country { get; set; }
        public long Size { get; set; }
        public decimal GrossPrice { get; set; }
        public string LogoUrl { get; set; }

        public IList<TourPlace> TourPlaces { get; set; }
        public IList<MemberPurchasedTour> MemberPurchasedTours { get; set; }
        public IList<MemberFavoriteTour> MemberFavoriteTours { get; set; }
        public IList<MemberDesiredTour> MemberDesiredTours { get; set; }
    }
}
