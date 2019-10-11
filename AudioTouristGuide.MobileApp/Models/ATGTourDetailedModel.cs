using System;
using System.Collections.Generic;
using System.Linq;
using AudioTouristGuide.DTO.Models.Tour;
using Xamarin.Forms;

namespace AudioTouristGuide.MobileApp.Models
{
    public class ATGTourDetailedModel
    {
        public long TourId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan EstimatedDuration { get; set; }
        public string CountryName { get; set; }
        public int DataSize { get; set; }
        public decimal GrossPrice { get; set; }
        public ImageSource LogoImage { get; set; }

        public IEnumerable<ATGPlaceModel> Places { get; set; }

        public bool HasUpdate => true || Places.Any(x => x.HasUpdate);
        public int UpdateDataSize => Places.Where(x => x.HasUpdate).Sum(x => x.DataSize);

        public ATGTourDetailedModel(DTOTourDetailedModel dtoTourModel)
        {
            TourId = dtoTourModel.TourId;
            Name = dtoTourModel.Name;
            Description = dtoTourModel.Description;
            EstimatedDuration = dtoTourModel.EstimatedDuration;
            CountryName = dtoTourModel.CountryName;
            DataSize = (int)(dtoTourModel.DataSize / 1048576); //bytes to mb convertation
            GrossPrice = dtoTourModel.GrossPrice.GetValueOrDefault(0);
            LogoImage = ImageSource.FromUri(new Uri(dtoTourModel.LogoUrl));
            Places = dtoTourModel.Places.Select(x => new ATGPlaceModel(x));
        }
    }
}
