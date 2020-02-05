using AudioTouristGuide.DTO.Models.Tour;
using AudioTouristGuide.WebAPI.Database.Entities.TourModels;
using AudioTouristGuide.WebAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AudioTouristGuide.WebAPI.SwaggerTools.DTOConverters
{
    public class TourDTOConverters
    {
        private readonly IFileStorageService _fileStorageService;

        public TourDTOConverters(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }

        public DTOTourDetailedModel DbTourToDTOModel(Tour dbTour)
        {

            return new DTOTourDetailedModel(
                dbTour.TourId,
                dbTour.Name,
                dbTour.Description,
                dbTour.EstimatedDuration,
                dbTour.Settlement,
                dbTour.CountryName,
                dbTour.DataSize,
                new DTOImageAssetModel(dbTour.LogoImage.ImageAssetId, dbTour.LogoImage.Name, dbTour.LogoImage.Description, _fileStorageService.GetFileUrl(dbTour.LogoImage.AssetContainerName, dbTour.LogoImage.AssetFileName), dbTour.LogoImage.LastUpdate),
                dbTour.TourPlaces?.Select(x => new DTOPlaceModel(
                    x.Place.PlaceId,
                    x.Place.Name,
                    x.Place.DisplayName,
                    x.Place.Description,
                    x.Place.Latitude,
                    x.Place.Longitude,
                    x.Place.DataSize,
                    new DTOAudioAssetModel(x.Place.AudioAsset.AudioAssetId, x.Place.AudioAsset.Name, x.Place.AudioAsset.Description, _fileStorageService.GetFileUrl(x.Place.AudioAsset.AssetContainerName, x.Place.AudioAsset.AssetFileName), x.Place.AudioAsset.LastUpdate),
                    new List<DTOPlaceImageAssetModel>(x.Place.PlaceImageAssets.Select(y => new DTOPlaceImageAssetModel(y.PlaceImageAssetId, y.Name, y.Description, _fileStorageService.GetFileUrl(y.AssetContainerName, y.AssetFileName), y.PointOfDisplayingStart, y.LastUpdate))))),
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
                dbTour.Settlement,
                dbTour.DataSize,
                new DTOImageAssetModel(dbTour.LogoImage.ImageAssetId, dbTour.LogoImage.Name, dbTour.LogoImage.Description, _fileStorageService.GetFileUrl(dbTour.LogoImage.AssetContainerName, dbTour.LogoImage.AssetFileName), dbTour.LogoImage.LastUpdate),
                dbTour.TourPlaces?.Select(x => new DTOPlaceModel(
                    x.Place.PlaceId,
                    x.Place.Name,
                    x.Place.DisplayName,
                    x.Place.Description,
                    x.Place.Latitude,
                    x.Place.Longitude,
                    x.Place.DataSize,
                    new DTOAudioAssetModel(x.Place.AudioAsset.AudioAssetId, x.Place.AudioAsset.Name, x.Place.AudioAsset.Description, _fileStorageService.GetFileUrl(x.Place.AudioAsset.AssetContainerName, x.Place.AudioAsset.AssetFileName), x.Place.AudioAsset.LastUpdate),
                    new List<DTOPlaceImageAssetModel>(x.Place.PlaceImageAssets.Select(y => new DTOPlaceImageAssetModel(y.PlaceImageAssetId, y.Name, y.Description, _fileStorageService.GetFileUrl(y.AssetContainerName, y.AssetFileName), y.PointOfDisplayingStart, y.LastUpdate))))),
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
                _fileStorageService.GetFileUrl(dbTour.LogoImage.AssetContainerName, dbTour.LogoImage.AssetFileName),
                dbTour.TourPlaces != null ? dbTour.TourPlaces.Count : 0,
                dbTour.GrossPrice));
        }
    }
}
