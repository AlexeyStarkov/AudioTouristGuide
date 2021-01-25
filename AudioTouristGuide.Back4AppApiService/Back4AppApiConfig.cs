using AudioTouristGuide.Back4AppApiService.DTO.Assets;
using AudioTouristGuide.Back4AppApiService.DTO.Location;
using AudioTouristGuide.Back4AppApiService.DTO.Members;
using AudioTouristGuide.Back4AppApiService.DTO.Tours;
using Parse;

namespace AudioTouristGuide.Back4AppApiService
{
    public static class Back4AppApiConfig
    {
        private const string ServerUri = "https://parseapi.back4app.com";
        private const string ParseApplicationId = "";
        private const string ParsePlatformKey = "";

        public const string GuestUsername = "Guest";
        public const string GuestPassword = "Guest";

        private static bool _isInitialized;
        public static ParseClient Init()
        {
            if (_isInitialized)
            {
                return ParseClient.Instance;
            }
            else
            {
                var client = new ParseClient(ParseApplicationId, ServerUri, ParsePlatformKey);

                client.Services.RegisterSubclass(typeof(FileAssetDTOModel));
                client.Services.RegisterSubclass(typeof(AssetTypeDTOModel));
                client.Services.RegisterSubclass(typeof(CountryDTOModel));
                client.Services.RegisterSubclass(typeof(LocationDTOModel));
                client.Services.RegisterSubclass(typeof(AuthorDTOModel));
                client.Services.RegisterSubclass(typeof(SlideshowTimeCodeDTOModel));
                client.Services.RegisterSubclass(typeof(TourDTOModel));
                client.Services.RegisterSubclass(typeof(TourSpotDTOModel));

                client.Publicize();
                _isInitialized = true;
                return client;
            }
        }
    }
}
