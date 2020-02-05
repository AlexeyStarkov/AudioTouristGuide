using System;
using System.Collections.Generic;
using System.Linq;
using AudioTouristGuide.DTO.Models.Tour;
using AudioTouristGuide.MobileApp.Storage.Interfaces;
using LiteDB;

namespace AudioTouristGuide.MobileApp.Storage.Models
{
    public class ATGTourDetailedDBModel : IStorageItem
    {
        [BsonId]
        public long ID { get; set; }
        public long TourId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan EstimatedDuration { get; set; }
        public string CountryName { get; set; }
        public string Settlement { get; set; }
        public long DataSize { get; set; }
        public decimal GrossPrice { get; set; }
        public ATGImageAssetDBModel CoverImageAsset { get; set; }

        public IEnumerable<ATGPlaceDBModel> Places { get; set; }

        public ATGTourDetailedDBModel(DTOTourDetailedModel dtoTourModel)
        {
            TourId = dtoTourModel.TourId;
            Name = dtoTourModel.Name;
            Description = dtoTourModel.Description;
            EstimatedDuration = dtoTourModel.EstimatedDurationTicks;
            CountryName = dtoTourModel.CountryName;
            DataSize = dtoTourModel.DataSize;
            GrossPrice = dtoTourModel.GrossPrice.GetValueOrDefault(0);
            CoverImageAsset = new ATGImageAssetDBModel(dtoTourModel.TourLogo);
            Places = dtoTourModel.Places.Select(x => new ATGPlaceDBModel(x));
            Settlement = dtoTourModel.Settlement;
        }

        public ATGTourDetailedDBModel() { }
    }
}
