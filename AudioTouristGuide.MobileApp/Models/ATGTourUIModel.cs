using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace AudioTouristGuide.MobileApp.Models
{
    public class ATGTourUIModel: BindableObject
    {
        public long TourId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan EstimatedDuration { get; set; }
        public string CountryName { get; set; }
        public string Settlement { get; set; }
        public ImageSource LogoImage { get; set; }
        public int PlacesCount { get; set; }
        public decimal GrossPrice { get; set; }
        public int DataSize { get; set; }
        public IEnumerable<ATGTourPlaceUIModel> Spots { get; set; }

        public bool HasUpdate => true || Spots.Any(x => x.HasUpdate);
        public int UpdateDataSize => Spots.Where(x => x.HasUpdate).Sum(x => x.DataSize);
        public bool IsFree => GrossPrice == 0;
    }
}
