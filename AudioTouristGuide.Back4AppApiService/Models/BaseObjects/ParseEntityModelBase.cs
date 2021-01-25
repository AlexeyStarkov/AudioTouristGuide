using System;

namespace AudioTouristGuide.Back4AppApiService.Models.BaseObjects
{
    public abstract class ParseEntityModelBase
    {
        public string Id { get; }
        public DateTime LastChange { get; }

        public ParseEntityModelBase(string id, DateTime lastChange)
        {
            Id = id;
            LastChange = lastChange;
        }
    }
}
