using System;
using AudioTouristGuide.MobileApp.Enums;

namespace AudioTouristGuide.MobileApp.EventsArgs
{
    public class GroupDownloadingFinishedEventArgs : EventArgs
    {
        public DownloadingStatus Status { get; }

        public GroupDownloadingFinishedEventArgs(DownloadingStatus status)
        {
            Status = status;
        }
    }
}
