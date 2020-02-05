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

        public DTOTourDetailedModel DbTourToDTOModel(TourDbModel dbTour)
        {

            return new DTOTourDetailedModel(
                dbTour.Id,
                dbTour.Name,
                dbTour.Description,
                dbTour.EstimatedDuration,
                dbTour.Settlement,
                dbTour.CountryName,
                dbTour.DataSize,
                new DTOImageAssetModel(dbTour.LogoImage.Id, dbTour.LogoImage.Name, dbTour.LogoImage.Description, _fileStorageService.GetFileUrl(dbTour.LogoImage.AssetContainerName, dbTour.LogoImage.AssetFileName), dbTour.LogoImage.LastUpdate),
                dbTour.TourPlaceDbModels?.Select(x => new DTOPlaceModel(
                    x.PlaceDbModel.Id,
                    x.PlaceDbModel.Name,
                    x.PlaceDbModel.DisplayName,
                    x.PlaceDbModel.Description,
                    x.PlaceDbModel.Latitude,
                    x.PlaceDbModel.Longitude,
                    x.PlaceDbModel.DataSize,
                    new DTOAudioAssetModel(x.PlaceDbModel.AudioAssetDbModel.Id, x.PlaceDbModel.AudioAssetDbModel.Name, x.PlaceDbModel.AudioAssetDbModel.Description, _fileStorageService.GetFileUrl(x.PlaceDbModel.AudioAssetDbModel.AssetContainerName, x.PlaceDbModel.AudioAssetDbModel.AssetFileName), x.PlaceDbModel.AudioAssetDbModel.LastUpdate),
                    new List<DTOPlaceImageAssetModel>(x.PlaceDbModel.PlaceImageAssetDbModels.Select(y => new DTOPlaceImageAssetModel(y.Id, y.Name, y.Description, _fileStorageService.GetFileUrl(y.AssetContainerName, y.AssetFileName), y.PointOfDisplayingStart, y.LastUpdate))))),
                dbTour.GrossPrice);
        }

        public IEnumerable<DTOTourDetailedModel> DbTourDetailedCollectionToDTO(IEnumerable<TourDbModel> dbTours)
        {
            return dbTours.Select(dbTour => new DTOTourDetailedModel(
                dbTour.Id,
                dbTour.Name,
                dbTour.Description,
                dbTour.EstimatedDuration,
                dbTour.CountryName,
                dbTour.Settlement,
                dbTour.DataSize,
                new DTOImageAssetModel(dbTour.LogoImage.Id, dbTour.LogoImage.Name, dbTour.LogoImage.Description, _fileStorageService.GetFileUrl(dbTour.LogoImage.AssetContainerName, dbTour.LogoImage.AssetFileName), dbTour.LogoImage.LastUpdate),
                dbTour.TourPlaceDbModels?.Select(x => new DTOPlaceModel(
                    x.PlaceDbModel.Id,
                    x.PlaceDbModel.Name,
                    x.PlaceDbModel.DisplayName,
                    x.PlaceDbModel.Description,
                    x.PlaceDbModel.Latitude,
                    x.PlaceDbModel.Longitude,
                    x.PlaceDbModel.DataSize,
                    new DTOAudioAssetModel(x.PlaceDbModel.AudioAssetDbModel.Id, x.PlaceDbModel.AudioAssetDbModel.Name, x.PlaceDbModel.AudioAssetDbModel.Description, _fileStorageService.GetFileUrl(x.PlaceDbModel.AudioAssetDbModel.AssetContainerName, x.PlaceDbModel.AudioAssetDbModel.AssetFileName), x.PlaceDbModel.AudioAssetDbModel.LastUpdate),
                    new List<DTOPlaceImageAssetModel>(x.PlaceDbModel.PlaceImageAssetDbModels.Select(y => new DTOPlaceImageAssetModel(y.Id, y.Name, y.Description, _fileStorageService.GetFileUrl(y.AssetContainerName, y.AssetFileName), y.PointOfDisplayingStart, y.LastUpdate))))),
                dbTour.GrossPrice));
        }

        public IEnumerable<DTOTourModel> DbTourCollectionToDTO(IEnumerable<TourDbModel> dbTours)
        {
            return dbTours.Select(dbTour => new DTOTourModel(
                dbTour.Id,
                dbTour.Name,
                dbTour.Description,
                dbTour.EstimatedDuration,
                dbTour.CountryName,
                dbTour.DataSize,
                _fileStorageService.GetFileUrl(dbTour.LogoImage.AssetContainerName, dbTour.LogoImage.AssetFileName),
                dbTour.TourPlaceDbModels != null ? dbTour.TourPlaceDbModels.Count : 0,
                dbTour.GrossPrice));
        }
    }
}
