using System;
using System.Collections.Generic;
using System.Linq;
using Plugin.DownloadManager.Abstractions;
using Xamarin.Forms;

namespace AudioTouristGuide.MobileApp.Tools
{
    public class FileGroupsDownloadingInformer : BindableObject
    {
        public IEnumerable<FileGroupDownloader> FilesGroupsToDownload { get; }

        public int FilesToDownloadCount => FilesGroupsToDownload != null ? FilesGroupsToDownload.Sum(x => x.FilesToDownload.Count()) : 0;
        public int SuccesfullyDownloadedFiles => FilesGroupsToDownload != null ? FilesGroupsToDownload.Sum(x => x.FilesToDownload.Count(y => y.Status == DownloadFileStatus.COMPLETED)) : 0;
        public double Progress
        {
            get
            {
                var finalizedItemsCount = FilesGroupsToDownload.Sum(x => x.FilesToDownload.Count(y => y.Status == DownloadFileStatus.CANCELED || y.Status == DownloadFileStatus.COMPLETED || y.Status == DownloadFileStatus.FAILED));
                return finalizedItemsCount / FilesToDownloadCount;
            }
        }
        public int ProgressPercentage => (int)Progress * 100;

        public bool HasFinished { get; private set; }

        public FileGroupsDownloadingInformer(IEnumerable<FileGroupDownloader> filesGroupsToDownload)
        {
            FilesGroupsToDownload = filesGroupsToDownload;
            foreach (var fileGroupToDownload in FilesGroupsToDownload)
            {
                foreach (var fileToDownload in fileGroupToDownload.FilesToDownload)
                {
                    fileToDownload.PropertyChanged += (s, e) =>
                    {
                        if (e.PropertyName == nameof(IDownloadFile.Status))
                        {
                            OnPropertyChanged(nameof(SuccesfullyDownloadedFiles));
                            OnPropertyChanged(nameof(Progress));
                            OnPropertyChanged(nameof(ProgressPercentage));
                        }
                    };
                }
            }
        }
    }
}
