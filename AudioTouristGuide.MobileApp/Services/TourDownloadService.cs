using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AudioTouristGuide.MobileApp.ApiService.Interfaces;
using AudioTouristGuide.MobileApp.Interfaces;
using AudioTouristGuide.MobileApp.Storage.Interfaces;
using AudioTouristGuide.MobileApp.Storage.Models;
using AudioTouristGuide.MobileApp.Tools;
using Plugin.DownloadManager;
using Plugin.DownloadManager.Abstractions;

namespace AudioTouristGuide.MobileApp.Services
{
    public class TourDownloadService : ITourDownloadService
    {
        private readonly IToursAPIService _toursAPIService;
        private readonly IFileRepository _fileRepository;
        private readonly IDataRepository _dataRepository;
        private readonly IDownloadManager _downloadManager;

        public TourDownloadService(IToursAPIService toursAPIService, IFileRepository fileRepository, IDataRepository dataRepository)
        {
            _toursAPIService = toursAPIService;
            _fileRepository = fileRepository;
            _dataRepository = dataRepository;
            _downloadManager = CrossDownloadManager.Current;
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
                if (newLocalTourModel.LogoImageAsset != null)
                    newLocalTourModel.LogoImageAsset.AssetLocalStorageId = localTourDetailedModel.LogoImageAsset.AssetLocalStorageId;

                foreach (var newLocalTourPlace in newLocalTourModel.Places)
                {
                    if (newLocalTourPlace.AudioAsset != null)
                    {
                        var oldPlaceAudioAsset = localTourDetailedModel.Places.FirstOrDefault(x => x.PlaceId == newLocalTourPlace.PlaceId)?.AudioAsset;
                        newLocalTourPlace.AudioAsset.AssetLocalStorageId = oldPlaceAudioAsset.AssetLocalStorageId;
                        newLocalTourPlace.AudioAsset.LastUpdate = oldPlaceAudioAsset.LastUpdate;
                    }

                    foreach (var newLocalTourPlaceImageAsset in newLocalTourPlace.ImageAssets)
                    {
                        var oldImageAsset = localTourDetailedModel.Places
                            .FirstOrDefault(x => x.PlaceId == newLocalTourPlace.PlaceId)?.ImageAssets
                            .FirstOrDefault(y => y.ImageAssetId == newLocalTourPlaceImageAsset.ImageAssetId);
                        newLocalTourPlaceImageAsset.AssetLocalStorageId = oldImageAsset?.AssetLocalStorageId;
                        newLocalTourPlaceImageAsset.LastUpdate = oldImageAsset?.LastUpdate;
                    }
                }
                _dataRepository.Update(newLocalTourModel);
            }

            var newLocalTourModelLogoLocalTimeStamp = newLocalTourModel.LogoImageAsset.LastUpdate;
            if (tour.TourLogo != null && tour.TourLogo.LastUpdate > newLocalTourModelLogoLocalTimeStamp)
            {
                var tourLogoImageDownloader = _downloadManager.CreateDownloadFile(tour.TourLogo.AssetFileUrl);

                tourLogoImageDownloader.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(IDownloadFile.Status) && s is IDownloadFile downloadFile)
                    {
                        if (downloadFile.Status == DownloadFileStatus.COMPLETED)
                        {
                            var localTourData = _dataRepository.GetById<ATGTourDetailedDBModel>(newLocalTourModel.ID);
                            var tourLogoFileStorageId = localTourData.LogoImageAsset.AssetLocalStorageId;
                            if (string.IsNullOrEmpty(tourLogoFileStorageId))
                            {
                                tourLogoFileStorageId = $"{tour.CountryName}/{tour.Settlement}/{tour.TourId}/{tour.TourLogo.ImageAssetId}";
                            }

                            _fileRepository.Delete(tourLogoFileStorageId);
                            var fileLocalStorageId = _fileRepository.Add(tourLogoFileStorageId, downloadFile.DestinationPathName);
                            localTourData.LogoImageAsset.AssetLocalStorageId = fileLocalStorageId;
                            localTourData.LogoImageAsset.LastUpdate = tour.TourLogo.LastUpdate;
                            _dataRepository.Update(localTourData);
                        }
                    }
                };
                placesAssetsDownloaders.Add(new FileGroupDownloader(new List<IDownloadFile>() { tourLogoImageDownloader }));
            }

            foreach (var localTourPlace in newLocalTourModel.Places)
            {
                var placeAssetsDownloaders = new List<IDownloadFile>();
                var apiPlace = tour.Places.First(x => x.PlaceId == localTourPlace.PlaceId);

                var localTourPlaceTimeStamp = localTourPlace.AudioAsset.LastUpdate;
                if (apiPlace.AudioAsset != null && apiPlace.AudioAsset.LastUpdate > localTourPlaceTimeStamp)
                {
                    var placeAudioAssetDownloader = _downloadManager.CreateDownloadFile(apiPlace.AudioAsset.AssetFileUrl);

                    placeAudioAssetDownloader.PropertyChanged += (s, e) =>
                    {
                        if (e.PropertyName == nameof(IDownloadFile.Status) && s is IDownloadFile downloadFile)
                        {
                            if (downloadFile.Status == DownloadFileStatus.COMPLETED)
                            {
                                var localTourData = _dataRepository.GetById<ATGTourDetailedDBModel>(newLocalTourModel.ID);
                                var localTourPlace = localTourData.Places.First(x => x.PlaceId == apiPlace.PlaceId);
                                var placeAudioFileStorageId = localTourPlace.AudioAsset.AssetLocalStorageId;
                                if (string.IsNullOrEmpty(placeAudioFileStorageId))
                                {
                                    placeAudioFileStorageId = $"{tour.CountryName}/{tour.Settlement}/{tour.TourId}/{localTourPlace.PlaceId}/{localTourPlace.AudioAsset.AudioAssetId}";
                                }

                                _fileRepository.Delete(placeAudioFileStorageId);
                                var fileLocalStorageId = _fileRepository.Add(placeAudioFileStorageId, downloadFile.DestinationPathName);
                                localTourPlace.AudioAsset.AssetLocalStorageId = fileLocalStorageId;
                                localTourPlace.AudioAsset.LastUpdate = apiPlace.AudioAsset.LastUpdate;
                                _dataRepository.Update(localTourData);
                            }
                        }
                    };
                    placeAssetsDownloaders.Add(placeAudioAssetDownloader);
                }

                foreach (var localTourPlaceImageAsset in localTourPlace.ImageAssets)
                {
                    var apiPlaceImage = apiPlace.ImageAssets.First(x => x.ImageAssetId == localTourPlaceImageAsset.ImageAssetId);
                    var localTourPlaceImageAssetTimeStamp = localTourPlaceImageAsset.LastUpdate;
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
                                    var localTourPlaceImage = localTourPlace.ImageAssets.First(x => x.ImageAssetId == apiPlaceImage.ImageAssetId);

                                    var placeImageFileStorageId = localTourPlaceImage.AssetLocalStorageId;
                                    if (string.IsNullOrEmpty(placeImageFileStorageId))
                                    {
                                        placeImageFileStorageId = $"{tour.CountryName}/{tour.Settlement}/{tour.TourId}/{localTourPlace.PlaceId}/{localTourPlaceImage.ImageAssetId}";
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
                    var nextPlaceToDownload = placesAssetsDownloaders.FirstOrDefault(x => !x.HasFinished);
                    foreach (var fileToDownload in nextPlaceToDownload?.FilesToDownload)
                    {
                        _downloadManager.Start(fileToDownload);
                    }
                };
                placesAssetsDownloaders.Add(placeAssetsDownloadersGroup);
            }

            var nextPlaceToDownload = placesAssetsDownloaders.FirstOrDefault(x => !x.HasFinished);
            foreach (var fileToDownload in nextPlaceToDownload?.FilesToDownload)
            {
                _downloadManager.Start(fileToDownload);
            }

            return new FileGroupsDownloadingInformer(placesAssetsDownloaders);
        }
    }
}
