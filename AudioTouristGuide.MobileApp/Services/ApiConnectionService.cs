using AudioTouristGuide.MobileApp.Interfaces;

namespace AudioTouristGuide.MobileApp.Services
{
    public class ApiConnectionService : IApiConnectionService
    {
        public string ApiUrl => "https://audiotourguideapi.azurewebsites.net/api";
    }
}
