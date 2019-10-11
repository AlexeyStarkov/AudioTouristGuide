using AudioTouristGuide.MobileApp.Models;
using AudioTouristGuide.MobileApp.ViewModels.BaseObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AudioTouristGuide.MobileApp.ViewModels
{
    public class TourDetailsPageViewModel : ViewModelBase
    {
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


        }
    }
}
