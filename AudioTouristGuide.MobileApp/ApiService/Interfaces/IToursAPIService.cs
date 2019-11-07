using System.Collections.Generic;
using System.Threading.Tasks;
using AudioTouristGuide.DTO.Models.Tour;

namespace AudioTouristGuide.MobileApp.ApiService.Interfaces
{
    public interface IToursAPIService
    {
        Task<IEnumerable<DTOTourDetailedModel>> GetAllToursAsync();
        Task<DTOTourDetailedModel> GetTourByIdAsync(long id);
    }
}
