using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AudioTouristGuide.WebAPI.Database.Entities.TourModels;
using AudioTouristGuide.WebAPI.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AudioTouristGuide.WebAPI.Database.Repositories
{
    public class TourRepository : RepositoryBase<Tour>, ITourRepository
    {
        public TourRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task<Tour> GetFullTourDataByIdAsync(long id)
        {
            return await DBContext.Tours
                .Include(t => t.TourPlaces)
                    .ThenInclude(tp => tp.Place.AudioAsset)
                .Include(t => t.TourPlaces)
                    .ThenInclude(tp => tp.Place.ImageAssets)
                .Where(t => t.TourId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Tour>> GetAllFullToursData()
        {
            return await DBContext.Tours
                .Include(t => t.TourPlaces)
                    .ThenInclude(tp => tp.Place.AudioAsset)
                .Include(t => t.TourPlaces)
                    .ThenInclude(tp => tp.Place.ImageAssets)
                .ToListAsync();
        }
    }
}
