using AudioTouristGuide.MobileApp.Models;
using AudioTouristGuide.MobileApp.ViewModels.BaseObjects;
using Prism.Navigation;

namespace AudioTouristGuide.MobileApp.ViewModels
{
    public class TourDetailsPageViewModel : ViewModelBase
    {
        public bool AlreadyPurchased => true;

        private ATGTourUIModel _tour;
        public ATGTourUIModel Tour
        {
            get { return _tour; }
            set { SetProperty(ref _tour, value); }
        }

        public TourDetailsPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

        }
    }
}
