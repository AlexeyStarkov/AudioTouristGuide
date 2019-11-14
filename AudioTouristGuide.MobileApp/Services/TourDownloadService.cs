using System.Collections.Generic;
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
            var tour = await _toursAPIService.GetTourByIdAsync(tourId);
            if (tour == null)
                return null;

            var placesAssetsDownloaders = new List<FileGroupDownloader>();

            ATGTourDetailedDBModel newLocalTourModel;
            var localTourDetailedModel = _dataRepository.FirstOrDefault<ATGTourDetailedDBModel>(x => x.TourId == tourId);
            if (localTourDetailedModel == null)
            {
                var localDbTourId = _dataRepository.Add(new ATGTourDetailedDBModel(tour));
                newLocalTourModel = _dataRepository.GetById<ATGTourDetailedDBModel>(localDbTourId);
            }
            else
            {
                newLocalTourModel = new ATGTourDetailedDBModel(tour);
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
            if (tour.TourLogo != null && tour.TourLogo.LastUpdate > newLocalTourModelLogoLocalTimeStamp)
            {
                var tourLogoImageDownloader = new FileDownloader(tour.TourLogo.AssetFileUrl, tour.TourLogo.Name);

                tourLogoImageDownloader.FileDownloadingFinished += (s, e) =>
                {
                    if (e.Status == DownloadingStatus.Success)
                    {
                        var localTourData = _dataRepository.GetById<ATGTourDetailedDBModel>(newLocalTourModel.ID);
                        var tourLogoFileStorageId = localTourData.CoverImageAsset.AssetLocalStorageId;
                        if (string.IsNullOrEmpty(tourLogoFileStorageId))
                        {
                            tourLogoFileStorageId = $"{tour.CountryName}/{tour.Settlement}/{tour.TourId}/{tour.TourLogo.ImageAssetId}";
                        }

                        _fileRepository.Delete(tourLogoFileStorageId);
                        var fileLocalStorageId = _fileRepository.Add(tourLogoFileStorageId, e.FilePath);
                        localTourData.CoverImageAsset.AssetLocalStorageId = fileLocalStorageId;
                        localTourData.CoverImageAsset.LastUpdate = tour.TourLogo.LastUpdate;
                        _dataRepository.Update(localTourData);
                    }
                };
                placesAssetsDownloaders.Add(new FileGroupDownloader(new List<FileDownloader>() { tourLogoImageDownloader }));
            }

            FileGroupDownloader nextPlaceToDownload = null;
            foreach (var newlocalTourPlace in newLocalTourModel.Places)
            {
                var placeAssetsDownloaders = new List<FileDownloader>();
                var apiPlace = tour.Places.First(x => x.PlaceId == newlocalTourPlace.PlaceId);

                var localTourPlaceTimeStamp = newlocalTourPlace.AudioAsset.LastUpdate.GetValueOrDefault();
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
                                placeAudioFileStorageId = $"{tour.CountryName}/{tour.Settlement}/{tour.TourId}/{localTourPlace.PlaceId}/{localTourPlace.AudioAsset.AudioAssetId}";
                            }

                            _fileRepository.Delete(placeAudioFileStorageId);
                            var fileLocalStorageId = _fileRepository.Add(placeAudioFileStorageId, e.FilePath);
                            localTourPlace.AudioAsset.AssetLocalStorageId = fileLocalStorageId;
                            localTourPlace.AudioAsset.LastUpdate = apiPlace.AudioAsset.LastUpdate;
                            _dataRepository.Update(localTourData);
                        }
                    };
                    placeAssetsDownloaders.Add(placeAudioAssetDownloader);
                }

                foreach (var localTourPlaceImageAsset in newlocalTourPlace.PlaceImageAssets)
                {
                    var apiPlaceImage = apiPlace.ImageAssets.First(x => x.PlaceImageAssetId == localTourPlaceImageAsset.PlaceImageAssetId);
                    var localTourPlaceImageAssetTimeStamp = localTourPlaceImageAsset.LastUpdate.GetValueOrDefault();
                    if (apiPlaceImage.LastUpdate > localTourPlaceImageAssetTimeStamp)
                    {
                        var placeImageAssetDownloader = _downloadManager.CreateDownloadFile(apiPlaceImage.AssetFileUrl);

                        placeImageAssetDownloader.PropertyChanged += (s, e) =>
                        {
                            if (e.PropertyName == nameof(IDownloadFile.Status) && s is IDownloadFile downloadFile)
                            {
                                if (downloadFile.Status == DownloadFileStatus.COMPLETED)
                                {
                                    var localTourData = _dataRepository.GetById<ATGTourDetailedDBModel>(newLocalTourModel.ID);
                                    var localTourPlace = localTourData.Places.FirstOrDefault(x => x.PlaceId == apiPlace.PlaceId);
                                    var localTourPlaceImage = localTourPlace.PlaceImageAssets.First(x => x.PlaceImageAssetId == apiPlaceImage.PlaceImageAssetId);

                                    var placeImageFileStorageId = localTourPlaceImage.AssetLocalStorageId;
                                    if (string.IsNullOrEmpty(placeImageFileStorageId))
                                    {
                                        placeImageFileStorageId = $"{tour.CountryName}/{tour.Settlement}/{tour.TourId}/{localTourPlace.PlaceId}/{localTourPlaceImage.PlaceImageAssetId}";
                                    }

                                    _fileRepository.Delete(placeImageFileStorageId);
                                    var fileLocalStorageId = _fileRepository.Add(placeImageFileStorageId, downloadFile.DestinationPathName);
                                    localTourPlaceImage.AssetLocalStorageId = fileLocalStorageId;
                                    localTourPlaceImage.LastUpdate = apiPlace.AudioAsset.LastUpdate;
                                    _dataRepository.Update(localTourData);
                                }
                            }
                        };
                        placeAssetsDownloaders.Add(placeImageAssetDownloader);
                    }
                }
                var placeAssetsDownloadersGroup = new FileGroupDownloader(placeAssetsDownloaders);
                placeAssetsDownloadersGroup.GroupDownloadedSuccessfully += (s, e) =>
                {
                    nextPlaceToDownload = placesAssetsDownloaders.FirstOrDefault(x => !x.HasFinished);
                    foreach (var fileToDownload in nextPlaceToDownload?.FilesToDownload)
                    {
                        _downloadManager.Start(fileToDownload);
                    }
                };
                placesAssetsDownloaders.Add(placeAssetsDownloadersGroup);
            }

            nextPlaceToDownload = placesAssetsDownloaders.FirstOrDefault(x => !x.HasFinished);
            foreach (var fileToDownload in nextPlaceToDownload?.FilesToDownload)
            {
                _downloadManager.Start(fileToDownload);
            }

            return new FileGroupsDownloadingInformer(placesAssetsDownloaders);
        }
    }
}
