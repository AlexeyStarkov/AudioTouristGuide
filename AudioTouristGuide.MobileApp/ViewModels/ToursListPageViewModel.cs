using System.Collections.Generic;
using System.Linq;
using AudioTouristGuide.MobileApp.Interfaces;
using AudioTouristGuide.MobileApp.Models;
using AudioTouristGuide.MobileApp.ViewModels.BaseObjects;
using Prism.Navigation;

namespace AudioTouristGuide.MobileApp.ViewModels
{
    public class ToursListPageViewModel : ViewModelBase
    {
        private readonly IToursAPIService _toursAPIService;

        private IEnumerable<ATGTourDetailedModel> _tours;
        public IEnumerable<ATGTourDetailedModel> Tours
        {
            get { return _tours; }
            set { SetProperty(ref _tours, value); }
        }

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
            var dtoTours = await _toursAPIService.GetAllTours();
            Tours = dtoTours.Select(x => new ATGTourDetailedModel(x));
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
        }
    }
}
