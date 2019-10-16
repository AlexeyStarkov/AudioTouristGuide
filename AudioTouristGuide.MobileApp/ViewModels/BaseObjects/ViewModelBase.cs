using System.Windows.Input;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace AudioTouristGuide.MobileApp.ViewModels.BaseObjects
{
    public abstract class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public virtual ICommand GoBackCommand => new Command(async (parameter) =>
        {
            await NavigationService.GoBackAsync();
        });

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
