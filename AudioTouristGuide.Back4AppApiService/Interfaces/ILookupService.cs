using AudioTouristGuide.Back4AppApiService.Models.Tours;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AudioTouristGuide.Back4AppApiService.Interfaces
{
    public interface ILookupService
    {
        Task<IEnumerable<TourModel>> LookupToursAsync();
    }
}
