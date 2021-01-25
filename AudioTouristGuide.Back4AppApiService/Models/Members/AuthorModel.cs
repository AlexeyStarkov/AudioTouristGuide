using AudioTouristGuide.Back4AppApiService.Models.BaseObjects;
using System;

namespace AudioTouristGuide.Back4AppApiService.Models.Members
{
    public class AuthorModel : ParseEntityModelBase
    {
        public AuthorModel(string id, DateTime lastChange) : base(id, lastChange)
        {
        }
    }
}
