using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AudioTouristGuide.WebAPI.Database.Entities.TourModels;
using AudioTouristGuide.WebAPI.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AudioTouristGuide.WebAPI.Database.Repositories
{
    public class ToursRepository : RepositoryBase<Tour>, IToursRepository
    {
        public ToursRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<Tour>> GetAllAsync()
        {
            return await DBContext.Tours
                .Include(t => t.LogoImage)
                .Include(t => t.TourPlaces)
                    .ThenInclude(tp => tp.Place.AudioAsset)
                .Include(t => t.TourPlaces)
                    .ThenInclude(tp => tp.Place.PlaceImageAssets)
                .ToListAsync();
        }

        public override async Task<Tour> GetByIdAsync(long id)
        {
            return await DBContext.Tours
                .Include(t => t.LogoImage)
                .Include(t => t.TourPlaces)
                    .ThenInclude(tp => tp.Place.AudioAsset)
                .Include(t => t.TourPlaces)
                    .ThenInclude(tp => tp.Place.PlaceImageAssets).FirstOrDefaultAsync(t => t.TourId == id);
        }

        public override async Task<IEnumerable<Tour>> GetAllAsync(Expression<Func<Tour, bool>> expression)
        {
            return await DBContext.Tours.Where(expression).ToListAsync();
        }
    }
}
