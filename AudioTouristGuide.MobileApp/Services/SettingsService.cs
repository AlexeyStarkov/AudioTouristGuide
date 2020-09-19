using System.Collections.Generic;
using AudioTouristGuide.MobileApp.Interfaces;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace AudioTouristGuide.MobileApp.Services
{
    public class SettingsService : ISettingsService
    {
        public IEnumerable<long> ToursToDownload
        {
            get => JsonConvert.DeserializeObject<IEnumerable<long>>(Preferences.Get(nameof(ToursToDownload), "[]"));
            set => Preferences.Set(nameof(ToursToDownload), JsonConvert.SerializeObject(value));
        }

        public bool IsMobileDataDownloadingAllowed
        {
            get => Preferences.Get(nameof(IsMobileDataDownloadingAllowed), default(bool));
            set => Preferences.Set(nameof(IsMobileDataDownloadingAllowed), value);
        }

        public void Clear(string settingName) => Preferences.Clear(settingName);
    }
}
