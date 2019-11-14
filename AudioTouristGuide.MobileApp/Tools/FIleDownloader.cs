using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AudioTouristGuide.MobileApp.Enums;
using AudioTouristGuide.MobileApp.EventsArgs;
using HeyRed.Mime;
using Xamarin.Essentials;
using Xamarin.Forms;

public delegate void FileDownloadingFinishedEventHandler(object sender, FileDownloadingFinishedEventArgs args);

namespace AudioTouristGuide.MobileApp.Tools
{
    public class FileDownloader : BindableObject
    {
        public event FileDownloadingFinishedEventHandler FileDownloadingFinished;

        private const int BufferSize = 4095;

        private readonly string _fileName;
        private readonly string _url;

        private readonly string _storageFolderPath;
        private readonly HttpClient _client;

        public CancellationToken CancellationToken { get; private set; }
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

        public FileDownloader(string url, string fileName)
        {
            _client = new HttpClient();
            _storageFolderPath = FileSystem.CacheDirectory;

            if (string.IsNullOrEmpty(fileName))
                _fileName = Guid.NewGuid().ToString();

            _url = url;

            Status = DownloadingStatus.Initialized;
        }

        public void Start()
        {
            Task.Run(async () =>
            {
                try
                {
                    Device.BeginInvokeOnMainThread(() => Status = DownloadingStatus.InProgress);
                    CancellationToken = new CancellationTokenSource().Token;
                    // Step 1 : Get call
                    var response = await _client.GetAsync(_url, HttpCompletionOption.ResponseHeadersRead, CancellationToken);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception(string.Format("The request returned with HTTP status code {0}", response.StatusCode));
                    }

                    // Step 2 : Get total of data
                    var totalData = response.Content.Headers.ContentLength.GetValueOrDefault(-1L);
                    var canSendProgress = totalData != -1L;

                    // Step 3 : Get filePath
                    var fileExtension = MimeTypesMap.GetExtension(response.Content.Headers?.ContentType.MediaType);
                    var filePath = _fileName.Contains($".{fileExtension}") ? Path.Combine(_storageFolderPath, $"{_fileName}") : Path.Combine(_storageFolderPath, $"{_fileName}.{fileExtension}");

                    // Step 4 : Download data
                    using (var fileStream = OpenStream(filePath))
                    {
                        using (var stream = await response.Content.ReadAsStreamAsync())
                        {
                            var totalRead = 0L;
                            var buffer = new byte[BufferSize];
                            var isMoreDataToRead = true;

                            do
                            {
                                CancellationToken.ThrowIfCancellationRequested();

                                var read = await stream.ReadAsync(buffer, 0, buffer.Length, CancellationToken);

                                if (read == 0)
                                {
                                    isMoreDataToRead = false;
                                }
                                else
                                {
                                    // Write data on disk.
                                    await fileStream.WriteAsync(buffer, 0, read);

                                    totalRead += read;

                                    if (canSendProgress)
                                    {
                                        Device.BeginInvokeOnMainThread(() =>
                                        {
                                            Progress = (totalRead * 1d) / (totalData * 1d);
                                        });
                                    }
                                }
                            }
                            while (isMoreDataToRead);
                        }
                    }

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Status = DownloadingStatus.Success;
                        FileDownloadingFinished?.Invoke(this, new FileDownloadingFinishedEventArgs(Status, filePath, "The file has been downloaded successfully"));
                    });
                    
                }
                catch (OperationCanceledException)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Status = DownloadingStatus.Cancelled;
                        FileDownloadingFinished?.Invoke(this, new FileDownloadingFinishedEventArgs(Status, null, "Downloading has been cancelled"));
                    });
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Status = DownloadingStatus.Fail;
                        FileDownloadingFinished?.Invoke(this, new FileDownloadingFinishedEventArgs(Status, null, ex.ToString()));
                    });
                }
            });
        }

        private Stream OpenStream(string path)
        {
            return new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, BufferSize);
        }
    }
}
