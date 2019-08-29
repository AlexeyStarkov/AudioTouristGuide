using System.Collections.Generic;
using System.Threading.Tasks;
using AudioTouristGuide.WebAPI.Database.Entities.TourModels;

namespace AudioTouristGuide.WebAPI.Database.Interfaces
{
    public interface ITourRepository : IRepositoryBase<Tour>
    {
        Task<Tour> GetFullTourDataByIdAsync(long id);
        Task<IEnumerable<Tour>> GetAllFullToursData();
    }
}
