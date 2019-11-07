using System;
namespace AudioTouristGuide.DTO.Models.Tour
{
    public class DTOTourModel
    {
        public long TourId { get; }
        public string Name { get; }
        public string Description { get; }
        public TimeSpan EstimatedDuration { get; }
        public string CountryName { get; }
        public long DataSize { get; }
        public decimal? GrossPrice { get; }
        public string LogoUrl { get; }
        public int NumberOfPlaces { get; }

        public DTOTourModel(long tourId, string name, string description, TimeSpan estimatedDuration, string countryName, long dataSize, string logoUrl, int numberOfPlaces, decimal? grossPrice = null)
        {
            TourId = tourId;
            Name = name;
            Description = description;
            EstimatedDuration = estimatedDuration;
            CountryName = countryName;
            DataSize = dataSize;
            GrossPrice = grossPrice;
            LogoUrl = logoUrl;
            NumberOfPlaces = numberOfPlaces;
        }
    }
}
