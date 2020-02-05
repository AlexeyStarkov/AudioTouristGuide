using System;
using System.Collections.Generic;

namespace AudioTouristGuide.DTO.Models.Tour
{
    public class DTOTourDetailedModel
    {
        public long TourId { get; }
        public string Name { get; }
        public string Description { get; }
        public long EstimatedDurationTicks { get; }
        public string CountryName { get; }
        public string Settlement { get; }
        public long DataSize { get; }
        public decimal? GrossPrice { get; }
        public DTOImageAssetModel TourLogo { get; set; }

        public IEnumerable<DTOPlaceModel> Places { get; }

        public DTOTourDetailedModel(long tourId, string name, string description, TimeSpan estimatedDuration, string countryName, string settlement, long dataSize, DTOImageAssetModel tourLogo, IEnumerable<DTOPlaceModel> places, decimal? grossPrice = null)
        {
            TourId = tourId;
            Name = name;
            Description = description;
            EstimatedDurationTicks = estimatedDuration.Ticks;
            CountryName = countryName;
            DataSize = dataSize;
            GrossPrice = grossPrice;
            TourLogo = tourLogo;
            Places = places;
            Settlement = settlement;
        }
    }
}