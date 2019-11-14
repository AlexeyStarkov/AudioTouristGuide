using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AudioTouristGuide.MobileApp.ApiService.Interfaces;
using AudioTouristGuide.MobileApp.Enums;
using AudioTouristGuide.MobileApp.Interfaces;
using AudioTouristGuide.MobileApp.Storage.Interfaces;
using AudioTouristGuide.MobileApp.Storage.Models;
using AudioTouristGuide.MobileApp.Tools;

namespace AudioTouristGuide.MobileApp.Services
{
    public class TourDownloadService : ITourDownloadService
    {
        private readonly IToursAPIService _toursAPIService;
        private readonly IFileRepository _fileRepository;
        private readonly IDataRepository _dataRepository;

        public TourDownloadService(IToursAPIService toursAPIService, IFileRepository fileRepository, IDataRepository dataRepository)
        {
            _toursAPIService = toursAPIService;
            _fileRepository = fileRepository;
            _dataRepository = dataRepository;
        }

        public async Task<FileGroupsDownloadingInformer> DownloadOrUpdateTourAsync(long tourId)
        {
            //REFACTOR IS NEEDED

            var apiTour = await _toursAPIService.GetTourByIdAsync(tourId);
            if (apiTour == null)
                return null;

            var placesAssetsDownloaders = new List<FileGroupDownloader>();

            ATGTourDetailedDBModel newLocalTourModel;
            var localTourDetailedModel = _dataRepository.FirstOrDefault<ATGTourDetailedDBModel>(x => x.TourId == tourId);
            if (localTourDetailedModel == null)
            {
                var localDbTourId = _dataRepository.Add(new ATGTourDetailedDBModel(apiTour));
                newLocalTourModel = _dataRepository.GetById<ATGTourDetailedDBModel>(localDbTourId);
            }
            else
            {
                newLocalTourModel = new ATGTourDetailedDBModel(apiTour);
                newLocalTourModel.ID = localTourDetailedModel.ID;
                if (newLocalTourModel.CoverImageAsset != null)
                    newLocalTourModel.CoverImageAsset.AssetLocalStorageId = localTourDetailedModel.CoverImageAsset.AssetLocalStorageId;

                foreach (var newLocalTourPlace in newLocalTourModel.Places)
                {
                    if (newLocalTourPlace.AudioAsset != null)
                    {
                        var oldPlaceAudioAsset = localTourDetailedModel.Places.FirstOrDefault(x => x.PlaceId == newLocalTourPlace.PlaceId)?.AudioAsset;
                        newLocalTourPlace.AudioAsset.AssetLocalStorageId = oldPlaceAudioAsset?.AssetLocalStorageId;
                        newLocalTourPlace.AudioAsset.LastUpdate = oldPlaceAudioAsset?.LastUpdate;
                    }

                    foreach (var newLocalTourPlaceImageAsset in newLocalTourPlace.PlaceImageAssets)
                    {
                        var oldImageAsset = localTourDetailedModel.Places
                            .FirstOrDefault(x => x.PlaceId == newLocalTourPlace.PlaceId)?.PlaceImageAssets
                            .FirstOrDefault(y => y.PlaceImageAssetId == newLocalTourPlaceImageAsset.PlaceImageAssetId);
                        newLocalTourPlaceImageAsset.AssetLocalStorageId = oldImageAsset?.AssetLocalStorageId;
                        newLocalTourPlaceImageAsset.LastUpdate = oldImageAsset?.LastUpdate;
                    }
                }
                _dataRepository.Update(newLocalTourModel);
            }

            var newLocalTourModelLogoLocalTimeStamp = newLocalTourModel.CoverImageAsset.LastUpdate.GetValueOrDefault();
            if (apiTour.TourLogo != null && apiTour.TourLogo.LastUpdate > newLocalTourModelLogoLocalTimeStamp)
            {
                var tourLogoImageDownloader = new FileDownloader(apiTour.TourLogo.AssetFileUrl, apiTour.TourLogo.Name);

                tourLogoImageDownloader.FileDownloadingFinished += (s, e) =>
                {
                    if (e.Status == DownloadingStatus.Success)
                    {
                        var localTourData = _dataRepository.GetById<ATGTourDetailedDBModel>(newLocalTourModel.ID);
                        var tourLogoFileStorageId = localTourData.CoverImageAsset.AssetLocalStorageId;
                        if (string.IsNullOrEmpty(tourLogoFileStorageId))
                        {
                            tourLogoFileStorageId = $"{apiTour.CountryName}/{apiTour.Settlement}/{apiTour.TourId}/{apiTour.TourLogo.ImageAssetId}";
                        }

                        _fileRepository.Delete(tourLogoFileStorageId);
                        var fileLocalStorageId = _fileRepository.Add(tourLogoFileStorageId, e.FilePath);
                        localTourData.CoverImageAsset.AssetLocalStorageId = fileLocalStorageId;
                        localTourData.CoverImageAsset.LastUpdate = apiTour.TourLogo.LastUpdate;
                        _dataRepository.Update(localTourData);
                        File.Delete(e.FilePath);
                    }
                };
                placesAssetsDownloaders.Add(new FileGroupDownloader(new List<FileDownloader>() { tourLogoImageDownloader }));
            }

            FileGroupDownloader nextGroupToDownload = null;
            foreach (var apiPlace in apiTour.Places)
            {
                var placeAssetsDownloaders = new List<FileDownloader>();
                var newlocalTourPlace = newLocalTourModel.Places.FirstOrDefault(x => x.PlaceId == apiPlace.PlaceId);
                newlocalTourPlace = newLocalTourModel.Places.FirstOrDefault(x => x.PlaceId == 0);

                if (newlocalTourPlace != null)
                {
                    var localTourPlaceTimeStamp = newlocalTourPlace.AudioAsset?.LastUpdate.GetValueOrDefault();
                    if (apiPlace.AudioAsset != null && apiPlace.AudioAsset.LastUpdate > localTourPlaceTimeStamp)
                    {
                        var placeAudioAssetDownloader = new FileDownloader(apiPlace.AudioAsset.AssetFileUrl, apiPlace.AudioAsset.Name);

                        placeAudioAssetDownloader.FileDownloadingFinished += (s, e) =>
                        {
                            if (e.Status == DownloadingStatus.Success)
                            {
                                var localTourData = _dataRepository.GetById<ATGTourDetailedDBModel>(newLocalTourModel.ID);
                                var localTourPlace = localTourData.Places.First(x => x.PlaceId == apiPlace.PlaceId);
                                var placeAudioFileStorageId = localTourPlace.AudioAsset.AssetLocalStorageId;
                                if (string.IsNullOrEmpty(placeAudioFileStorageId))
                                {
                                    placeAudioFileStorageId = $"{apiTour.CountryName}/{apiTour.Settlement}/{apiTour.TourId}/{localTourPlace.PlaceId}/{localTourPlace.AudioAsset.AudioAssetId}";
                                }

                                _fileRepository.Delete(placeAudioFileStorageId);
                                var fileLocalStorageId = _fileRepository.Add(placeAudioFileStorageId, e.FilePath);
                                localTourPlace.AudioAsset.AssetLocalStorageId = fileLocalStorageId;
                                localTourPlace.AudioAsset.LastUpdate = apiPlace.AudioAsset.LastUpdate;
                                _dataRepository.Update(localTourData);
                                File.Delete(e.FilePath);
                            }
                        };
                        placeAssetsDownloaders.Add(placeAudioAssetDownloader);
                    }

                    foreach (var apiPlaceImage in apiPlace.ImageAssets)
                    {
                        var localTourPlaceImageAsset = newlocalTourPlace.PlaceImageAssets?.FirstOrDefault(x => x.PlaceImageAssetId == apiPlaceImage.PlaceImageAssetId);
                        localTourPlaceImageAsset = newlocalTourPlace.PlaceImageAssets.FirstOrDefault(x => x.PlaceImageAssetId == 0);

                        if (localTourPlaceImageAsset != null)
                        {
                            var localTourPlaceImageAssetTimeStamp = localTourPlaceImageAsset.LastUpdate.GetValueOrDefault();
                            if (apiPlaceImage.LastUpdate > localTourPlaceImageAssetTimeStamp)
                            {
                                var placeImageAssetDownloader = new FileDownloader(apiPlaceImage.AssetFileUrl, apiPlaceImage.Name);

                                placeImageAssetDownloader.FileDownloadingFinished += (s, e) =>
                                {
                                    if (e.Status == DownloadingStatus.Success)
                                    {
                                        var localTourData = _dataRepository.GetById<ATGTourDetailedDBModel>(newLocalTourModel.ID);
                                        var localTourPlace = localTourData.Places.FirstOrDefault(x => x.PlaceId == apiPlace.PlaceId);
                                        var localTourPlaceImage = localTourPlace.PlaceImageAssets.First(x => x.PlaceImageAssetId == apiPlaceImage.PlaceImageAssetId);

                                        var placeImageFileStorageId = localTourPlaceImage.AssetLocalStorageId;
                                        if (string.IsNullOrEmpty(placeImageFileStorageId))
                                        {
                                            placeImageFileStorageId = $"{apiTour.CountryName}/{apiTour.Settlement}/{apiTour.TourId}/{localTourPlace.PlaceId}/{localTourPlaceImage.PlaceImageAssetId}";
                                        }

                                        _fileRepository.Delete(placeImageFileStorageId);
                                        var fileLocalStorageId = _fileRepository.Add(placeImageFileStorageId, e.FilePath);
                                        localTourPlaceImage.AssetLocalStorageId = fileLocalStorageId;
                                        localTourPlaceImage.LastUpdate = apiPlace.AudioAsset.LastUpdate;
                                        _dataRepository.Update(localTourData);
                                        File.Delete(e.FilePath);
                                    }
                                };
                                placeAssetsDownloaders.Add(placeImageAssetDownloader);
                            }
                        }
                    }
                    var placeAssetsDownloadersGroup = new FileGroupDownloader(placeAssetsDownloaders);
                    placeAssetsDownloadersGroup.GroupDownloadedSuccessfully += (s, e) =>
                    {
                        nextGroupToDownload = placesAssetsDownloaders.FirstOrDefault(x => !x.HasFinished);
                        nextGroupToDownload?.Start();

                    };
                    placesAssetsDownloaders.Add(placeAssetsDownloadersGroup);
                }
            }

            nextGroupToDownload = placesAssetsDownloaders.FirstOrDefault(x => !x.HasFinished);
            nextGroupToDownload?.Start();

            return new FileGroupsDownloadingInformer(placesAssetsDownloaders);
        }
    }
}
