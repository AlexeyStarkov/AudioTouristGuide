using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using AudioTouristGuide.MobileApp.Interfaces;
using AudioTouristGuide.MobileApp.Models;
using AudioTouristGuide.MobileApp.Pages;
using AudioTouristGuide.MobileApp.ViewModels.BaseObjects;
using Prism.Navigation;
using Xamarin.Forms;

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

        public ICommand GoToTourDetailsCommand => new Command(async (parameter) =>
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("tour", Tours);
            await NavigationService.NavigateAsync(nameof(TourDetailsPage), navigationParameters);
        });

        public ToursListPageViewModel(INavigationService navigationService, IToursAPIService toursAPIService)
            : base(navigationService)
        {
            _toursAPIService = toursAPIService;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            var dtoTours = await _toursAPIService.GetAllTours();
            Tours = dtoTours.Select(x => new ATGTourDetailedModel(x)).ToList();
        }
    }
}
