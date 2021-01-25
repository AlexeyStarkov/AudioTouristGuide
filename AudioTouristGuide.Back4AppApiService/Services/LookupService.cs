using AudioTouristGuide.Back4AppApiService.Interfaces;
using AudioTouristGuide.Back4AppApiService.Models.Tours;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AudioTouristGuide.Back4AppApiService.Services
{
    public class LookupService : ILookupService
    {
        public async Task<IEnumerable<TourModel>> LookupToursAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
