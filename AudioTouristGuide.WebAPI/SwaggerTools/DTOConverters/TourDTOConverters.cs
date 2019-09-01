using AudioTouristGuide.DTO.Models.Tour;
using AudioTouristGuide.WebAPI.Database.Entities.TourModels;
using System.Collections.Generic;
using System.Linq;

namespace AudioTouristGuide.WebAPI.SwaggerTools.DTOConverters
{
    public static class TourDTOConverters
    {
        public static TourModel DbTourToDTO(Tour dbTour)
        {
            return new TourModel(
                dbTour.TourId,
                dbTour.Name,
                dbTour.Description,
                dbTour.EstimatedDuration,
                dbTour.CountryName,
                dbTour.DataSize,
                dbTour.LogoUrl,
                dbTour.TourPlaces?.Select(x => new PlaceModel(
                    x.Place.PlaceId,
                    x.Place.Name,
                    x.Place.DisplayName,
                    x.Place.Description,
                    x.Place.Latitude,
                    x.Place.Longitude,
                    x.Place.DataSize,
                    new AudioAssetModel(x.Place.AudioAsset.AudioAssetId, x.Place.AudioAsset.Name, x.Place.AudioAsset.Description, x.Place.AudioAsset.AssetFileUrl),
                    new List<ImageAssetModel>(x.Place.ImageAssets.Select(y => new ImageAssetModel(y.ImageAssetId, y.Name, y.Description, y.AssetFileUrl, y.PointOfDisplayingStart))))),
                dbTour.GrossPrice);
        }

        public static IEnumerable<TourModel> DbTourCollectionToDTO(IEnumerable<Tour> dbTours)
        {
            return dbTours.Select(dbTour => new TourModel(
                dbTour.TourId,
                dbTour.Name,
                dbTour.Description,
                dbTour.EstimatedDuration,
                dbTour.CountryName,
                dbTour.DataSize,
                dbTour.LogoUrl,
                dbTour.TourPlaces?.Select(x => new PlaceModel(
                    x.Place.PlaceId,
                    x.Place.Name,
                    x.Place.DisplayName,
                    x.Place.Description,
                    x.Place.Latitude,
                    x.Place.Longitude,
                    x.Place.DataSize,
                    new AudioAssetModel(x.Place.AudioAsset.AudioAssetId, x.Place.AudioAsset.Name, x.Place.AudioAsset.Description, x.Place.AudioAsset.AssetFileUrl),
                    new List<ImageAssetModel>(x.Place.ImageAssets.Select(y => new ImageAssetModel(y.ImageAssetId, y.Name, y.Description, y.AssetFileUrl, y.PointOfDisplayingStart))))),
                dbTour.GrossPrice));
        }
    }
}
