using System.Threading.Tasks;

namespace AudioTouristGuide.MobileApp.Interfaces
{
    public interface IDownloadingService
    {
        Task AddTourToQueueAndStartAsync(long tourId);
        void StopAndRemoveTourFromQueue(long tourId);
    }
}
