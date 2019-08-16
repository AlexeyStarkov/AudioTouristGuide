﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AudioTouristGuide.WebAPI.Database.Enums;
using AudioTouristGuide.WebAPI.Database.JoinTablesModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace AudioTouristGuide.WebAPI.Database.MemberModels
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long MemberId { get; set; }
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

        [JsonIgnore]
        public ICollection<MemberPurchasedTour> MemberPurchasedTours { get; set; }
        [JsonIgnore]
        public ICollection<MemberFavoriteTour> MemberFavoriteTours { get; set; }
        [JsonIgnore]
        public ICollection<MemberDesiredTour> MemberDesiredTour { get; set; }
    }
}
