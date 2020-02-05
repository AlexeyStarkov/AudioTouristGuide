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
    public class PlacesRepository : RepositoryBase<PlaceDbModel>, IPlacesRepository
    {
        public PlacesRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<PlaceDbModel>> GetAllAsync()
        {
            return await DBContext.PlaceDbModels.Include(x => x.AudioAssetDbModel).Include(x => x.PlaceImageAssetDbModels).Include(x => x.TourPlaces).ToListAsync();
        }

        public override async Task<IEnumerable<PlaceDbModel>> GetAllAsync(Expression<Func<PlaceDbModel, bool>> expression)
        {
            return await DBContext.PlaceDbModels.Where(expression).ToListAsync();
        }

        public override async Task<PlaceDbModel> GetByIdAsync(long id)
        {
            return await DBContext.PlaceDbModels.Include(x => x.AudioAssetDbModel).Include(x => x.PlaceImageAssetDbModels).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
