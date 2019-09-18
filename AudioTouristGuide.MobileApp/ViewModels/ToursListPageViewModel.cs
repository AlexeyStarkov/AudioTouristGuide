using System.Linq;
using AudioTouristGuide.MobileApp.Interfaces;
using AudioTouristGuide.MobileApp.ViewModels.BaseObjects;
using Prism.Navigation;

namespace AudioTouristGuide.MobileApp.ViewModels
{
    public class ToursListPageViewModel : ViewModelBase
    {
        private readonly IToursAPIService _toursAPIService;

        public ToursListPageViewModel(INavigationService navigationService, IToursAPIService toursAPIService)
            : base(navigationService)
        {
            _toursAPIService = toursAPIService;
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            var tours = await _toursAPIService.GetAllTours();

            var tour15 = await _toursAPIService.GetTourById(tours.Last().TourId);
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
        }
    }
}
