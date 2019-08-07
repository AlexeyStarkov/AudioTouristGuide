using AudioTouristGuide.WebAPI.Database.MemberModels;
using AudioTouristGuide.WebAPI.Database.TourModels;

namespace AudioTouristGuide.WebAPI.Database.JoinTablesModels
{
    public class MemberFavoriteTour
    {
        public long MemberId { get; set; }
        public Member Member { get; set; }

        public long TourId { get; set; }
        public Tour Tour { get; set; }
    }
}
