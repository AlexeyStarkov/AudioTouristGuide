using System.Collections.Generic;
using System.Linq;
using AudioTouristGuide.MobileApp.Enums;
using AudioTouristGuide.MobileApp.EventsArgs;

public delegate void GroupDownloadingFinishedEventHandler(object source, GroupDownloadingFinishedEventArgs args);

namespace AudioTouristGuide.MobileApp.Tools
{
    public class FileGroupDownloader
    {
        public event GroupDownloadingFinishedEventHandler GroupDownloadedSuccessfully;

        public IEnumerable<FileDownloader> FilesToDownload { get; }
        public bool HasFinished { get; private set; }

        public FileGroupDownloader(IEnumerable<FileDownloader> filesToDownload)
        {
            FilesToDownload = filesToDownload;
            foreach (var fileToDownload in FilesToDownload)
            {
                fileToDownload.FileDownloadingFinished += (s, e) =>
                {
                    if (!FilesToDownload.Any(x => x.Status != DownloadingStatus.Success))
                    {
                        GroupDownloadedSuccessfully?.Invoke(this, new GroupDownloadingFinishedEventArgs(DownloadingStatus.Success));
                        HasFinished = true;
                    }
                    else if (FilesToDownload.Count(x => x.Status == DownloadingStatus.Fail) == FilesToDownload.Count())
                    {
                        GroupDownloadedSuccessfully?.Invoke(this, new GroupDownloadingFinishedEventArgs(DownloadingStatus.Fail));
                        HasFinished = true;
                    }
                };
            }
        }

        public void Start()
        {
            foreach (var fileToDownload in FilesToDownload)
            {
                fileToDownload.Start();
            }
        }
    }
}
