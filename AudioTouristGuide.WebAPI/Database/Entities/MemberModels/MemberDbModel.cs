using System;
using System.Collections.Generic;
using AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels;
using AudioTouristGuide.WebAPI.Database.Enums;

namespace AudioTouristGuide.WebAPI.Database.Entities.MemberModels
{
    public class MemberDbModel : DbModelBase
    {
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

        public ICollection<MemberPurchasedTourDbModel> MemberPurchasedTourDbModels { get; set; }
        public ICollection<MemberFavoriteTourDbModel> MemberFavoriteTourDbModels { get; set; }
        public ICollection<MemberDesiredTourDbModel> MemberDesiredTourDbModels { get; set; }
    }
}
