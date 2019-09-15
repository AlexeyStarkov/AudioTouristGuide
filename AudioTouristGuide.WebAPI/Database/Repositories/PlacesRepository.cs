using AudioTouristGuide.WebAPI.Database.Entities.TourModels;
using AudioTouristGuide.WebAPI.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AudioTouristGuide.WebAPI.Database.Repositories
{
    public class PlacesRepository : RepositoryBase<Place>, IPlacesRepository
    {
        public PlacesRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<Place>> GetAllAsync()
        {
            return await DBContext.Places.Include(x => x.AudioAsset).Include(x => x.ImageAssets).Include(x => x.TourPlaces).ToListAsync();
        }

        public override async Task<IEnumerable<Place>> GetByCondition(Expression<Func<Place, bool>> expression)
        {
            return await DBContext.Places.Where(expression).ToListAsync();
        }

        public override async Task<Place> GetByIdAsync(long id)
        {
            return await DBContext.Places.Include(x => x.AudioAsset).Include(x => x.ImageAssets).FirstOrDefaultAsync(x => x.PlaceId == id);
        }
    }
}
