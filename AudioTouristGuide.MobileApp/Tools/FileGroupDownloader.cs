using System;
using System.Collections.Generic;
using System.Linq;
using Plugin.DownloadManager.Abstractions;

namespace AudioTouristGuide.MobileApp.Tools
{
    public class FileGroupDownloader
    {
        public event EventHandler GroupDownloadedSuccessfully;

        public IEnumerable<IDownloadFile> FilesToDownload { get; }
        public bool HasFinished { get; private set; }

        public FileGroupDownloader(IEnumerable<IDownloadFile> filesToDownload)
        {
            FilesToDownload = filesToDownload;
            foreach (var fileToDownload in FilesToDownload)
            {
                fileToDownload.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(IDownloadFile.Status) && !FilesToDownload.Any(x => x.Status != DownloadFileStatus.COMPLETED))
                    {
                        GroupDownloadedSuccessfully?.Invoke(this, new EventArgs());
                        HasFinished = true;
                    }
                };
            }
        }
    }
}
