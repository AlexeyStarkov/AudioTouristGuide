using System.Collections.Generic;
using System.Threading.Tasks;
using AudioTouristGuide.DTO.Models.Tour;

namespace AudioTouristGuide.MobileApp.Interfaces
{
    public interface IToursAPIService
    {
        Task<IEnumerable<DTOTourDetailedModel>> GetAllTours();
        Task<DTOTourDetailedModel> GetTourById(long id);
    }
}
