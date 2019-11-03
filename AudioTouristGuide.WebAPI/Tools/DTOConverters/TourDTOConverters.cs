using AudioTouristGuide.DTO.Models.Tour;
using AudioTouristGuide.WebAPI.Database.Entities.TourModels;
using AudioTouristGuide.WebAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AudioTouristGuide.WebAPI.SwaggerTools.DTOConverters
{
    public class TourDTOConverters
    {
        private readonly IBlobStorageService _blobStorageService;

        public TourDTOConverters(IBlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        public DTOTourDetailedModel DbTourToDTOModel(Tour dbTour)
        {

            return new DTOTourDetailedModel(
                dbTour.TourId,
                dbTour.Name,
                dbTour.Description,
                dbTour.EstimatedDuration,
                dbTour.CountryName,
                dbTour.DataSize,
                new DTOImageAssetModel(dbTour.LogoImage.ImageAssetId, dbTour.LogoImage.Name, dbTour.LogoImage.Description, _blobStorageService.GetFileTokenizedUrl(dbTour.LogoImage.AssetContainerName, dbTour.LogoImage.AssetFileName), dbTour.LogoImage.PointOfDisplayingStart, dbTour.LogoImage.LastUpdate),
                dbTour.TourPlaces?.Select(x => new DTOPlaceModel(
                    x.Place.PlaceId,
                    x.Place.Name,
                    x.Place.DisplayName,
                    x.Place.Description,
                    x.Place.Latitude,
                    x.Place.Longitude,
                    x.Place.DataSize,
                    new DTOAudioAssetModel(x.Place.AudioAsset.AudioAssetId, x.Place.AudioAsset.Name, x.Place.AudioAsset.Description, _blobStorageService.GetFileTokenizedUrl(x.Place.AudioAsset.AssetContainerName, x.Place.AudioAsset.AssetFileName), x.Place.AudioAsset.LastUpdate),
                    new List<DTOImageAssetModel>(x.Place.ImageAssets.Select(y => new DTOImageAssetModel(y.ImageAssetId, y.Name, y.Description, _blobStorageService.GetFileTokenizedUrl(y.AssetContainerName, y.AssetFileName), y.PointOfDisplayingStart, y.LastUpdate))))),
                dbTour.GrossPrice);
        }

        public IEnumerable<DTOTourDetailedModel> DbTourDetailedCollectionToDTO(IEnumerable<Tour> dbTours)
        {
            return dbTours.Select(dbTour => new DTOTourDetailedModel(
                dbTour.TourId,
                dbTour.Name,
                dbTour.Description,
                dbTour.EstimatedDuration,
                dbTour.CountryName,
                dbTour.DataSize,
                new DTOImageAssetModel(dbTour.LogoImage.ImageAssetId, dbTour.LogoImage.Name, dbTour.LogoImage.Description, _blobStorageService.GetFileTokenizedUrl(dbTour.LogoImage.AssetContainerName, dbTour.LogoImage.AssetFileName), dbTour.LogoImage.PointOfDisplayingStart, dbTour.LogoImage.LastUpdate),
                dbTour.TourPlaces?.Select(x => new DTOPlaceModel(
                    x.Place.PlaceId,
                    x.Place.Name,
                    x.Place.DisplayName,
                    x.Place.Description,
                    x.Place.Latitude,
                    x.Place.Longitude,
                    x.Place.DataSize,
                    new DTOAudioAssetModel(x.Place.AudioAsset.AudioAssetId, x.Place.AudioAsset.Name, x.Place.AudioAsset.Description, _blobStorageService.GetFileTokenizedUrl(x.Place.AudioAsset.AssetContainerName, x.Place.AudioAsset.AssetFileName), x.Place.AudioAsset.LastUpdate),
                    new List<DTOImageAssetModel>(x.Place.ImageAssets.Select(y => new DTOImageAssetModel(y.ImageAssetId, y.Name, y.Description, _blobStorageService.GetFileTokenizedUrl(y.AssetContainerName, y.AssetFileName), y.PointOfDisplayingStart, y.LastUpdate))))),
                dbTour.GrossPrice));
        }

        public IEnumerable<DTOTourModel> DbTourCollectionToDTO(IEnumerable<Tour> dbTours)
        {
            return dbTours.Select(dbTour => new DTOTourModel(
                dbTour.TourId,
                dbTour.Name,
                dbTour.Description,
                dbTour.EstimatedDuration,
                dbTour.CountryName,
                dbTour.DataSize,
                _blobStorageService.GetFileTokenizedUrl(dbTour.LogoImage.AssetContainerName, dbTour.LogoImage.AssetFileName),
                dbTour.TourPlaces != null ? dbTour.TourPlaces.Count : 0,
                dbTour.GrossPrice));
        }
    }
}
