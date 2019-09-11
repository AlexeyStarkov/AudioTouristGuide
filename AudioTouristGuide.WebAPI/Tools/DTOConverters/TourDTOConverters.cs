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

        public DTOTourModel DbTourToDTOModel(Tour dbTour)
        {

            return new DTOTourModel(
                dbTour.TourId,
                dbTour.Name,
                dbTour.Description,
                dbTour.EstimatedDuration,
                dbTour.CountryName,
                dbTour.DataSize,
                _blobStorageService.GetFileUrl(dbTour.AssetsContainerName, dbTour.LogoFileName),
                dbTour.TourPlaces?.Select(x => new DTOPlaceModel(
                    x.Place.PlaceId,
                    x.Place.Name,
                    x.Place.DisplayName,
                    x.Place.Description,
                    x.Place.Latitude,
                    x.Place.Longitude,
                    x.Place.DataSize,
                    new DTOAudioAssetModel(x.Place.AudioAsset.AudioAssetId, x.Place.AudioAsset.Name, x.Place.AudioAsset.Description, _blobStorageService.GetFileUrl(x.Place.AudioAsset.AssetContainerName, x.Place.AudioAsset.AssetFileName)),
                    new List<DTOImageAssetModel>(x.Place.ImageAssets.Select(y => new DTOImageAssetModel(y.ImageAssetId, y.Name, y.Description, _blobStorageService.GetFileUrl(y.AssetContainerName, y.AssetFileName), y.PointOfDisplayingStart))))),
                dbTour.GrossPrice);
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
                _blobStorageService.GetFileUrl(dbTour.AssetsContainerName, dbTour.LogoFileName),
                dbTour.TourPlaces?.Select(x => new DTOPlaceModel(
                    x.Place.PlaceId,
                    x.Place.Name,
                    x.Place.DisplayName,
                    x.Place.Description,
                    x.Place.Latitude,
                    x.Place.Longitude,
                    x.Place.DataSize,
                    new DTOAudioAssetModel(x.Place.AudioAsset.AudioAssetId, x.Place.AudioAsset.Name, x.Place.AudioAsset.Description, _blobStorageService.GetFileUrl(x.Place.AudioAsset.AssetContainerName, x.Place.AudioAsset.AssetFileName)),
                    new List<DTOImageAssetModel>(x.Place.ImageAssets.Select(y => new DTOImageAssetModel(y.ImageAssetId, y.Name, y.Description, _blobStorageService.GetFileUrl(y.AssetContainerName, y.AssetFileName), y.PointOfDisplayingStart))))),
                dbTour.GrossPrice));
        }
    }
}
