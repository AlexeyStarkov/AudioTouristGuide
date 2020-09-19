using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AudioTouristGuide.MobileApp.Interfaces;
using AudioTouristGuide.MobileApp.Tools;
using Prism.Ioc;

public delegate void TourDownloadingInformerUpdatedEventHandler(TourDownloadingManager downloadingManager);

namespace AudioTouristGuide.MobileApp.Services
{
    public class DownloadingService : IDownloadingService
    {
        private readonly ISettingsService _settingsService;
        private readonly IList<TourDownloadingManager> _downloadingInformers;

        public event TourDownloadingInformerUpdatedEventHandler TourDownloadingInformerUpdated;

        public DownloadingService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
            _downloadingInformers = new List<TourDownloadingManager>();
            Initialize();
        }

        private void Initialize()
        {
            Task.Run(async () =>
            {
                var tourIdsToDownloadFromCache = _settingsService.ToursToDownload;
                if (tourIdsToDownloadFromCache.Any())
                {
                    _settingsService.Clear(nameof(_settingsService.ToursToDownload));
                    foreach (var tourIdToDownloadFromCache in tourIdsToDownloadFromCache)
                    {
                        await AddTourToQueueAndStartAsync(tourIdToDownloadFromCache);
                    }
                }
            });
        }

        public async Task AddTourToQueueAndStartAsync(long tourId)
        {
            if (!_downloadingInformers.Any(x => x.TourId == tourId))
            {
                var tourDownloader = App.Current.Container.Resolve<ITourDownloadService>();
                var downloadingManager = await tourDownloader.DownloadOrUpdateTourAsync(tourId);

                downloadingManager.PropertyChanged += OnInformerPropertyChanged;

                _downloadingInformers.Add(downloadingManager);
                _settingsService.ToursToDownload = _downloadingInformers.Select(x => x.TourId).ToList();
            }
        }

        public void StopAndRemoveTourFromQueue(long tourId)
        {
            var tourToRemove = _downloadingInformers.FirstOrDefault(x => x.TourId == tourId);
            tourToRemove.StopDownloading();
            if (tourToRemove != null)
            {
                tourToRemove.PropertyChanged -= OnInformerPropertyChanged;
                _downloadingInformers.Remove(tourToRemove);
                _settingsService.ToursToDownload = _downloadingInformers.Select(x => x.TourId).ToList();
            }
        }

        private void OnInformerPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var downloadingManager = (TourDownloadingManager)sender;
            TourDownloadingInformerUpdated?.Invoke(downloadingManager);

            if (e.PropertyName == nameof(TourDownloadingManager.HasFinished) && downloadingManager.HasFinished)
                StopAndRemoveTourFromQueue(downloadingManager.TourId);
        }
    }
}
