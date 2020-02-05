using System;
using System.Collections.Generic;
using AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels;

namespace AudioTouristGuide.WebAPI.Database.Entities.TourModels
{
    public class TourDbModel : DbModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan EstimatedDuration { get; set; }
        public string CountryName { get; set; }
        public string Settlement { get; set; }
        public long DataSize { get; set; }
        public decimal? GrossPrice { get; set; }

        public ImageAssetDbModel LogoImage { get; set; }

        public ICollection<TourPlaceDbModel> TourPlaceDbModels { get; set; }
        public ICollection<MemberPurchasedTourDbModel> MemberPurchasedTourDbModels { get; set; }
        public ICollection<MemberFavoriteTourDbModel> MemberFavoriteTourDbModels { get; set; }
        public ICollection<MemberDesiredTourDbModel> MemberDesiredTourDbModels { get; set; }
    }
}
