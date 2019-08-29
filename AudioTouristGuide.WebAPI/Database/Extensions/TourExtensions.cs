using System.Collections.Generic;
using System.Linq;
using AudioTouristGuide.DTO.Models.Tour;
using AudioTouristGuide.WebAPI.Database.Entities.TourModels;

namespace AudioTouristGuide.WebAPI.Database.Extensions
{
    public static class TourExtensions
    {
        public static TourModel ToDTO(this Tour self)
        {
            return new TourModel(
                self.TourId,
                self.Name,
                self.Description,
                self.EstimatedDuration,
                self.CountryName,
                self.DataSize,
                self.LogoUrl,
                self.TourPlaces?.Select(x => new PlaceModel(
                    x.Place.PlaceId,
                    x.Place.Name,
                    x.Place.DisplayName,
                    x.Place.Description,
                    x.Place.Latitude,
                    x.Place.Longitude,
                    x.Place.DataSize,
                    new AudioAssetModel(x.Place.AudioAsset.AudioAssetId, x.Place.AudioAsset.Name, x.Place.AudioAsset.Description, x.Place.AudioAsset.AssetFileUrl),
                    new List<ImageAssetModel>(x.Place.ImageAssets.Select(y => new ImageAssetModel(y.ImageAssetId, y.Name, y.Description, y.AssetFileUrl, y.PointOfDisplayingStart))))),
                self.GrossPrice);
        }
    }
}
