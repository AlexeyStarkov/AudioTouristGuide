using System;
using System.Collections.Generic;
using AudioTouristGuide.DTO.Enums;

namespace AudioTouristGuide.WebAPI.Database.Models.Tour
{
    public class Tour
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan EstimatedDuration { get; set; }
        public Countries Country { get; set; }
        public long Size { get; set; }
        public decimal GrossPrice { get; set; }
        public string LogoUrl { get; set; }

        public IList<TourItem> TourItems { get; set; }
    }
}
