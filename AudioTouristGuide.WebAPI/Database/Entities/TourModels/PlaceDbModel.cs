using System.Collections.Generic;
using AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels;

namespace AudioTouristGuide.WebAPI.Database.Entities.TourModels
{
    public class PlaceDbModel : DbModelBase
    {
        public string AssetsContainerName { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public long DataSize { get; set; }

        public AudioAssetDbModel AudioAssetDbModel { get; set; }

        public ICollection<PlaceImageAssetDbModel> PlaceImageAssetDbModels { get; set; }
        public ICollection<TourPlaceDbModel> TourPlaces { get; set; }
    }
}
