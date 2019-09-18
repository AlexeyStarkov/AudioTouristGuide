using AudioTouristGuide.MobileApp.Interfaces;
using AudioTouristGuide.MobileApp.Pages;
using AudioTouristGuide.MobileApp.Services;
using AudioTouristGuide.MobileApp.ViewModels;
using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
using Prism.Unity;

namespace AudioTouristGuide.MobileApp
{
    public partial class App : PrismApplication
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync(nameof(ToursListPage));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterPopupNavigationService();
            containerRegistry.Register<IApiConnectionService, ApiConnectionService>();
            containerRegistry.Register<IToursAPIService, ToursAPIService>();

            containerRegistry.RegisterForNavigation<ToursListPage, ToursListPageViewModel>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    }
}
