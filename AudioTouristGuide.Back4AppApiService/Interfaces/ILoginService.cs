using System.Threading.Tasks;

namespace AudioTouristGuide.Back4AppApiService.Interfaces
{
    public interface ILoginService
    {
        Task LoginAsGuestAsync();
        Task SignOutAsync();
    }
}
