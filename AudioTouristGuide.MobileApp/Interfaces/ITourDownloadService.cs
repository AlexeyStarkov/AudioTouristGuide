﻿using System.Threading.Tasks;
using AudioTouristGuide.MobileApp.Tools;

namespace AudioTouristGuide.MobileApp.Interfaces
{
    public interface ITourDownloadService
    {
        Task<FileGroupsDownloadingInformer> DownloadOrUpdateTourAsync(long tourId);
    }
}
