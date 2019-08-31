using AudioTouristGuide.WebAPI.Database.Entities.TourModels;

namespace AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels
{
    public class TourPlace
    {
        public long PlaceId { get; set; }
        public Place Place { get; set; }

        public long TourId { get; set; }
        public Tour Tour { get; set; }
    }
}
