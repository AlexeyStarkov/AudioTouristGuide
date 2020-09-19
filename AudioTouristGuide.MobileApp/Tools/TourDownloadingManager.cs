using System;
using System.Collections.Generic;
using System.Linq;
using AudioTouristGuide.MobileApp.Enums;
using Xamarin.Forms;

namespace AudioTouristGuide.MobileApp.Tools
{
    public class TourDownloadingManager : BindableObject
    {
        private readonly IEnumerable<FileGroupDownloader> _fileGroupsToDownload;

        private bool _isDownloadingAllowed;

        public long TourId { get; }
        public int FilesToDownloadCount => _fileGroupsToDownload.SelectMany(x => x.FilesToDownload.Where(y => y.Status != DownloadingStatus.Success)).Count();
        public int AllTourFilesCount { get; }
        public int SuccesfullyDownloadedFilesCount => AllTourFilesCount - FilesToDownloadCount;
        public double Progress => FilesToDownloadCount / AllTourFilesCount;
        public int ProgressPercentage => (int)Progress * 100;
        public bool HasFinished => Progress >= 1;

        public TourDownloadingManager(long tourId, IEnumerable<FileGroupDownloader> fileGroupsToDownload, int allTourFilesCount)
        {
            if (allTourFilesCount <= 0)
                throw new ArgumentOutOfRangeException($"{nameof(allTourFilesCount)} must be bigger than 0");

            TourId = tourId;
            _isDownloadingAllowed = true;

            AllTourFilesCount = allTourFilesCount;

            _fileGroupsToDownload = fileGroupsToDownload;

            foreach (var fileGroupToDownload in _fileGroupsToDownload)
            {
                foreach (var fileToDownload in fileGroupToDownload.FilesToDownload)
                {
                    fileToDownload.PropertyChanged += (s, e) =>
                    {
                        if (e.PropertyName == nameof(FileDownloader.Status))
                        {
                            OnPropertyChanged(nameof(SuccesfullyDownloadedFilesCount));
                            OnPropertyChanged(nameof(Progress));
                            OnPropertyChanged(nameof(ProgressPercentage));
                            OnPropertyChanged(nameof(HasFinished));
                        }
                    };
                }

                fileGroupToDownload.GroupDownloadedSuccessfully += (s, e) =>
                {
                    var nextGroupToDownload = _fileGroupsToDownload.FirstOrDefault(x => !x.HasFinished);

                    if (_isDownloadingAllowed)
                        nextGroupToDownload?.Start();
                };
            }
        }

        public void StopDownloading()
        {
            _isDownloadingAllowed = false;
        }

        public void StartDownloading()
        {
            _isDownloadingAllowed = true;
            var nextGroupToDownload = _fileGroupsToDownload.FirstOrDefault(x => !x.HasFinished);
            nextGroupToDownload?.Start();
        }
    }
}
