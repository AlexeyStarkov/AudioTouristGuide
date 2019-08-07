using System.Collections.Generic;
using AudioTouristGuide.WebAPI.Database.JoinTablesModels;

namespace AudioTouristGuide.WebAPI.Database.TourModels
{
    public class Place
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<TourPlace> TourPlaces { get; set; }
    }
}
