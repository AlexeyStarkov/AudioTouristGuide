using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using AudioTouristGuide.MobileApp.ApiService.Interfaces;
using AudioTouristGuide.MobileApp.Interfaces;
using AudioTouristGuide.MobileApp.Models;
using AudioTouristGuide.MobileApp.Pages;
using AudioTouristGuide.MobileApp.Tools;
using AudioTouristGuide.MobileApp.ViewModels.BaseObjects;
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

        private FileGroupsDownloadingInformer _downloadingInformer;
        public FileGroupsDownloadingInformer DownloadingInformer
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

        public ToursListPageViewModel(INavigationService navigationService, IToursAPIService toursAPIService,
            ITourDownloadService tourDownloadService)
            : base(navigationService)
        {
            _toursAPIService = toursAPIService;
            _tourDownloadService = tourDownloadService;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            var dtoTours = await _toursAPIService.GetAllToursAsync();
            DownloadingInformer = await _tourDownloadService.DownloadOrUpdateTourAsync(dtoTours.FirstOrDefault().TourId);
            
        }
    }
}
