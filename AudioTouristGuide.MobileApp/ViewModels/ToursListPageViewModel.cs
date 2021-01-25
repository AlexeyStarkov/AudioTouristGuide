using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using AudioTouristGuide.Back4AppApiService;
using AudioTouristGuide.Back4AppApiService.DTO.Location;
using AudioTouristGuide.MobileApp.ApiService.Interfaces;
using AudioTouristGuide.MobileApp.Interfaces;
using AudioTouristGuide.MobileApp.Models;
using AudioTouristGuide.MobileApp.Pages;
using AudioTouristGuide.MobileApp.Tools;
using AudioTouristGuide.MobileApp.ViewModels.BaseObjects;
using Parse;
using Prism.Navigation;
using Xamarin.Forms;

namespace AudioTouristGuide.MobileApp.ViewModels
{
    public class ToursListPageViewModel : ViewModelBase
    {
        private readonly IToursAPIService _toursAPIService;
        private readonly ITourDownloadService _tourDownloadService;

        private IEnumerable<ATGTourUIModel> _tours;
        public IEnumerable<ATGTourUIModel> Tours
        {
            get { return _tours; }
            set { SetProperty(ref _tours, value); }
        }

        private double _progressValue;
        public double ProgressValue
        {
            get { return _progressValue; }
            set { SetProperty(ref _progressValue, value); }
        }

        private string _imageSource;
        public string ImageSource
        {
            get { return _imageSource; }
            set { SetProperty(ref _imageSource, value); }
        }

        private TourDownloadingManager _downloadingInformer;
        public TourDownloadingManager DownloadingInformer
        {
            get => _downloadingInformer;
            set { SetProperty(ref _downloadingInformer, value); }
        }

        public ICommand GoToTourDetailsCommand => new Command(async (parameter) =>
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("tour", parameter);
            await NavigationService.NavigateAsync(nameof(TourDetailsPage), navigationParameters);
        });

        public ToursListPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            //_toursAPIService = toursAPIService;
            //_tourDownloadService = tourDownloadService;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            try
            {
                ParseObject testObject = new ParseObject("TestDupaClass");
                testObject.Bind(App.CurrentParseClient);
                await testObject.SaveAsync();


                var countriesQuery = App.CurrentParseClient.GetQuery<CountryDTOModel>();
                var countries = await countriesQuery.FindAsync();
            }
            catch (System.Exception ex)
            {

            }
            

            //var dtoTours = await _toursAPIService.GetAllToursAsync();
            //var tourToDownload = dtoTours.FirstOrDefault();

            //if (tourToDownload != null)
            //    DownloadingInformer = await _tourDownloadService.DownloadOrUpdateTourAsync(tourToDownload.TourId);

        }

        internal void ReportProgress(double value)
        {
            ProgressValue = value;
        }
    }
}
