using System.Collections.Generic;
using System.Threading.Tasks;
using AudioTouristGuide.DTO.Models.Tour;

namespace AudioTouristGuide.MobileApp.Interfaces
{
    public interface IToursAPIService
    {
        Task<IEnumerable<DTOTourModel>> GetAllTours();
        Task<DTOTourModel> GetTourById(long id);
    }
}
