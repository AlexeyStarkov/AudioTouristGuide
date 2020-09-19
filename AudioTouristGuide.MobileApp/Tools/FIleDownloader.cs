using System;
using System.Net;
using System.Threading.Tasks;
using AudioTouristGuide.MobileApp.Enums;
using AudioTouristGuide.MobileApp.EventsArgs;
using Xamarin.Essentials;
using Xamarin.Forms;

public delegate void FileDownloadingFinishedEventHandler(object sender, FileDownloadingFinishedEventArgs args);

namespace AudioTouristGuide.MobileApp.Tools
{
    public class FileDownloader : BindableObject
    {
        public event FileDownloadingFinishedEventHandler FileDownloadingFinished;

        private readonly string _filePath;
        private readonly string _url;

        public DownloadingStatus Status { get; private set; }

        private double _progress;
        public double Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }

        private int _progressPercentage;
        public int ProgressPercentage
        {
            get => _progressPercentage;
            set
            {
                _progressPercentage = value;
                OnPropertyChanged(nameof(ProgressPercentage));
            }
        }

        public FileDownloader(string url, string subfolderName, string fileName)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(subfolderName) || string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("Constructor parameter can't be null or empty");
            }

            _filePath = $"{FileSystem.AppDataDirectory}/{subfolderName}/{fileName}";
            _url = url;

            Status = DownloadingStatus.Pending;
        }

        public async Task DownloadFileAsync()
        {
            using (var webClient = new WebClient())
            {
                try
                {
                    webClient.DownloadFileCompleted += (s, e) =>
                    {
                        if (e.Cancelled)
                        {
                            Status = DownloadingStatus.Cancelled;
                            FileDownloadingFinished?.Invoke(this, new FileDownloadingFinishedEventArgs(DownloadingStatus.Cancelled, null, null));
                        }   
                        else if (e.Error != null)
                        {
                            Status = DownloadingStatus.Fail;
                            FileDownloadingFinished?.Invoke(this, new FileDownloadingFinishedEventArgs(DownloadingStatus.Fail, null, e.Error.Message));
                        }
                        else
                        {
                            Status = DownloadingStatus.Success;
                            FileDownloadingFinished?.Invoke(this, new FileDownloadingFinishedEventArgs(DownloadingStatus.Success, _filePath, null));
                        }   
                    };

                    webClient.DownloadProgressChanged += (s, e) =>
                    {
                        Status = DownloadingStatus.InProgress;
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Progress = e.BytesReceived / e.TotalBytesToReceive;
                            ProgressPercentage = e.ProgressPercentage;
                        });
                    };

                    Status = DownloadingStatus.InProgress;
                    await webClient.DownloadFileTaskAsync(new Uri(_url), _filePath);
                }
                catch (Exception ex)
                {
                    FileDownloadingFinished?.Invoke(this, new FileDownloadingFinishedEventArgs(DownloadingStatus.Fail, null, ex.Message));
                }
            }
        }
    }
}
