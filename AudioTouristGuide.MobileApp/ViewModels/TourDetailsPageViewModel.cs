using AudioTouristGuide.MobileApp.Models;
using AudioTouristGuide.MobileApp.ViewModels.BaseObjects;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioTouristGuide.MobileApp.ViewModels
{
    public class TourDetailsPageViewModel : ViewModelBase
    {
        public bool AlreadyPurchased => true;

        private ATGTourDetailedModel _tour;
        public ATGTourDetailedModel Tour
        {
            get { return _tour; }
            set { SetProperty(ref _tour, value); }
        }

        public TourDetailsPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            Tour = parameters.FirstOrDefault(x => x.Value is ATGTourDetailedModel).Value as ATGTourDetailedModel;
        }
    }
}
