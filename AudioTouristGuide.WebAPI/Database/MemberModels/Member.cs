using System;
using System.Collections.Generic;
using AudioTouristGuide.WebAPI.Database.Enums;
using AudioTouristGuide.WebAPI.Database.JoinTablesModels;

namespace AudioTouristGuide.WebAPI.Database.MemberModels
{
    public class Member
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarImageUrl { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string MobilePhoneNumber { get; set; }
        public Sex Sex { get; set; }
        public double BonusPoints { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime CereationTimeStamp { get; set; }
        public bool IsEmailValidated { get; set; }
        public bool IsMobilePhoneNumberValidated { get; set; }

        public IList<MemberPurchasedTour> MemberPurchasedTours { get; set; }
        public IList<MemberFavoriteTour> MemberFavoriteTours { get; set; }
        public IList<MemberDesiredTour> MemberDesiredTour { get; set; }
    }
}
