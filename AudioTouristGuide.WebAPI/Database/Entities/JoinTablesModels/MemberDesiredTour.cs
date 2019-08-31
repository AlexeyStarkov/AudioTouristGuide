using AudioTouristGuide.WebAPI.Database.Entities.MemberModels;
using AudioTouristGuide.WebAPI.Database.Entities.TourModels;

namespace AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels
{
    public class MemberDesiredTour
    {
        public long MemberId { get; set; }
        public Member Member { get; set; }

        public long TourId { get; set; }
        public Tour Tour { get; set; }
    }
}
