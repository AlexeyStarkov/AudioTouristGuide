using AudioTouristGuide.Back4AppApiService.DTO.Assets;
using AudioTouristGuide.Back4AppApiService.DTO.Location;
using AudioTouristGuide.Back4AppApiService.DTO.Members;
using AudioTouristGuide.Back4AppApiService.DTO.Tours;
using Parse;
using System.Threading.Tasks;

namespace AudioTouristGuide.Back4AppApiService
{
    public static class Back4AppApiConfig
    {
        private const string ServerUri = "https://parseapi.back4app.com";
        private const string ParseApplicationId = "NDPrQpRcLSrJ857NetukdvGjKHoErgl33LVYcjPk";
        private const string ParsePlatformKey = "FpkaFDCLVifqBwJp8DZ9XzSajHq1uPzySDI5al5e";

        private static ParseClient _client;
        public static ParseClient Init()
        {
            if (_client != null)
            {
                return _client;
            }
            else
            {
                _client = new ParseClient(ParseApplicationId, ServerUri, ParsePlatformKey);
                
                _client.Services.RegisterSubclass(typeof(FileAssetDTOModel));
                _client.Services.RegisterSubclass(typeof(AssetTypeDTOModel));
                _client.Services.RegisterSubclass(typeof(CountryDTOModel));
                _client.Services.RegisterSubclass(typeof(LocationDTOModel));
                _client.Services.RegisterSubclass(typeof(AuthorDTOModel));
                _client.Services.RegisterSubclass(typeof(SlideshowTimeCodeDTOModel));
                _client.Services.RegisterSubclass(typeof(TourDTOModel));
                _client.Services.RegisterSubclass(typeof(TourSpotDTOModel));

                return _client;
            }
        }
    }
}
