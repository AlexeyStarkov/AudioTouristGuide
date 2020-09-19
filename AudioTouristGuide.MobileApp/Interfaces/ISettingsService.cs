using System.Collections.Generic;

namespace AudioTouristGuide.MobileApp.Interfaces
{
    public interface ISettingsService
    {
        void Clear(string settingName);
        IEnumerable<long> ToursToDownload { get; set; }
        bool IsMobileDataDownloadingAllowed { get; set; }
    }
}
