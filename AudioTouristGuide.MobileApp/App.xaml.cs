using AudioTouristGuide.Back4AppApiService;
using AudioTouristGuide.MobileApp.ApiService.Interfaces;
using AudioTouristGuide.MobileApp.Interfaces;
using AudioTouristGuide.MobileApp.Pages;
using AudioTouristGuide.MobileApp.Services;
using AudioTouristGuide.MobileApp.Storage.Interfaces;
using AudioTouristGuide.MobileApp.Storage.Repositories;
using AudioTouristGuide.MobileApp.ViewModels;
using Parse;
using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
using Prism.Unity;

namespace AudioTouristGuide.MobileApp
{
    public partial class App : PrismApplication
    {
        public static string ServerUrl => "https://localhost:5002/";
        public static string ApiUrl => $"{ServerUrl}api/v1";

        internal static ParseClient CurrentParseClient;

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            CurrentParseClient = Back4AppApiConfig.Init();

            await NavigationService.NavigateAsync(nameof(ToursListPage));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterPopupNavigationService();
            //containerRegistry.Register<IToursAPIService, ToursAPIService>();
            containerRegistry.Register<IDataRepository, DataRepository>();
            containerRegistry.Register<IFileRepository, FileRepository>();
            //containerRegistry.Register<ITourDownloadService, TourDownloadService>();
            containerRegistry.Register<ISettingsService, SettingsService>();
            containerRegistry.RegisterSingleton<IDownloadingService, DownloadingService>();

            containerRegistry.RegisterForNavigation<ToursListPage, ToursListPageViewModel>();
            containerRegistry.RegisterForNavigation<TourDetailsPage, TourDetailsPageViewModel>();
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
