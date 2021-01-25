using AudioTouristGuide.Back4AppApiService.DTO.Tours;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AudioTouristGuide.MobileApp.ApiService.Interfaces
{
    public interface IToursAPIService
    {
        Task<IEnumerable<TourDTOModel>> GetAllToursAsync();
        Task<TourDTOModel> GetTourByIdAsync(long id);
    }
}
