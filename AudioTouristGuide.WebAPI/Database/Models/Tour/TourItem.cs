using System;
namespace AudioTouristGuide.WebAPI.Database.Models.Tour
{
    public class TourItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public long TourId { get; set; }
        public Tour Tour { get; set; }
    }
}
