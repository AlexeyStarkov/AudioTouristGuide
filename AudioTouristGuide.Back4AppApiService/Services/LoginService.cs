using AudioTouristGuide.Back4AppApiService.Interfaces;
using Parse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AudioTouristGuide.Back4AppApiService.Services
{
    public class LoginService : ILoginService
    {
        private readonly ParseClient _parseClient;
        public LoginService()
        {
            _parseClient = ParseClient.Instance;
        }

        public async Task LoginAsGuestAsync()
        {
            var guestUser = await _parseClient.LogInAsync(Back4AppApiConfig.GuestUsername, Back4AppApiConfig.GuestPassword);
        }

        public async Task SignOutAsync()
        {
            await _parseClient.LogOutAsync();
        }
    }
}
