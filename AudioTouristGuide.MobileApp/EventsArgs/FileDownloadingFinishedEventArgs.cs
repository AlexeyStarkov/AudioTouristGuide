using System;
using AudioTouristGuide.MobileApp.Enums;

namespace AudioTouristGuide.MobileApp.EventsArgs
{
    public class FileDownloadingFinishedEventArgs : EventArgs
    {
        public DownloadingStatus Status { get; }
        public string FilePath { get; }
        public string Message { get; }

        public FileDownloadingFinishedEventArgs(DownloadingStatus downloadingStatus, string filepath, string message)
        {
            Status = downloadingStatus;
            FilePath = filepath;
            Message = message;
        }
    }
}
