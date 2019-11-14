using System.Collections.Generic;
using System.Linq;
using AudioTouristGuide.MobileApp.Enums;
using Xamarin.Forms;

namespace AudioTouristGuide.MobileApp.Tools
{
    public class FileGroupsDownloadingInformer : BindableObject
    {
        public IEnumerable<FileGroupDownloader> FilesGroupsToDownload { get; }

        public int FilesToDownloadCount { get; }
        public int SuccesfullyDownloadedFilesCount => FilesGroupsToDownload != null ? FilesGroupsToDownload.SelectMany(x => x.FilesToDownload).Count(x => x.Status == DownloadingStatus.Success) : 0;
        public double Progress
        {
            get
            {
                var allFilesToDownload = FilesGroupsToDownload.SelectMany(x => x.FilesToDownload);
                return allFilesToDownload.Sum(x => x.Progress) / allFilesToDownload.Count();
            }
        }
        public int ProgressPercentage => (int)Progress * 100;

        public bool HasFinished { get; private set; }

        public FileGroupsDownloadingInformer(IEnumerable<FileGroupDownloader> filesGroupsToDownload)
        {
            FilesGroupsToDownload = filesGroupsToDownload;
            if (FilesGroupsToDownload != null)
                FilesToDownloadCount = FilesGroupsToDownload.SelectMany(x => x.FilesToDownload).Count();

            foreach (var fileGroupToDownload in FilesGroupsToDownload)
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
                        }
                    };
                }
            }
        }
    }
}
