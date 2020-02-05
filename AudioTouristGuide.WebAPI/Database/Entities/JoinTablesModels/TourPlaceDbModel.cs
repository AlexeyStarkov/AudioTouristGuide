using AudioTouristGuide.WebAPI.Database.Entities.TourModels;

namespace AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels
{
    public class TourPlaceDbModel
    {
        public long PlaceDbModelId { get; set; }
        public PlaceDbModel PlaceDbModel { get; set; }

        public long TourDbModelId { get; set; }
        public TourDbModel TourDbModel { get; set; }
    }
}
