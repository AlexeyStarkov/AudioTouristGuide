using AudioTouristGuide.MobileApp.ApiService.Interfaces;

namespace AudioTouristGuide.MobileApp.ApiService.Services
{
    public class ApiConnectionService : IApiConnectionService
    {
        public string ApiUrl => "https://audiotourguideapi.azurewebsites.net/api/v1/";
    }
}
