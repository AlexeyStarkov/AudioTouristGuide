using AudioTouristGuide.WebAPI.Database.Entities.MemberModels;
using AudioTouristGuide.WebAPI.Database.Entities.TourModels;

namespace AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels
{
    public class MemberPurchasedTourDbModel
    {
        public long MemberDbModelId { get; set; }
        public MemberDbModel MemberDbModel { get; set; }

        public long TourDbModelId { get; set; }
        public TourDbModel TourDbModel { get; set; }
    }
}
