using AudioTouristGuide.Back4AppApiService.Models.BaseObjects;
using AudioTouristGuide.Back4AppApiService.Models.Members;
using System;

namespace AudioTouristGuide.Back4AppApiService.Models.Tours
{
    public class TourModel : ParseEntityModelBase
    {
        public string Name { get; }
        public string DisplayName { get; }
        public AuthorModel Author { get; }
        public LocationModel Location { get; }
        public TimeSpan EstimatedDuration { get; }
        public decimal? GrossPrice { get; }

        public TourModel(string id, DateTime lastChange, string name, string displayName, AuthorModel author, LocationModel location, TimeSpan estimatedDuration, decimal? grossPrice)
            : base(id, lastChange)
        {
            Name = name;
            DisplayName = displayName;
            Author = author;
            Location = location;
            EstimatedDuration = estimatedDuration;
            GrossPrice = grossPrice;
        }
    }
}
