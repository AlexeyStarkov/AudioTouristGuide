using AudioTouristGuide.WebAPI.Database.MemberModels;
using AudioTouristGuide.WebAPI.Database.TourModels;

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
