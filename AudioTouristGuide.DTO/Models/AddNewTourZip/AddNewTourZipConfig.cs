using System;
using System.Collections.Generic;

namespace AudioTouristGuide.DTO.Models.AddNewTourZip
{
    public class AddNewTourZipConfig
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan EstimatedDuration { get; set; }
        public string CountryName { get; set; }
        public decimal? GrossPrice { get; set; }
        public string LogoFileName { get; set; }

        public IEnumerable<AddNewTourZipPlace> Places { get; set; }
    }
}