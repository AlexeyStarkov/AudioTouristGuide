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
    public class PlaceImageAssetsRepository : RepositoryBase<PlaceImageAsset>, IPlaceImageAssetsRepository
    {
        public PlaceImageAssetsRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<PlaceImageAsset>> GetAllAsync()
        {
            return await DBContext.PlaceImageAssets.Include(x => x.Place).ToListAsync();
        }

        public override async Task<IEnumerable<PlaceImageAsset>> GetAllAsync(Expression<Func<PlaceImageAsset, bool>> expression)
        {
            return await DBContext.PlaceImageAssets.Where(expression).ToListAsync();
        }

        public override async Task<PlaceImageAsset> GetByIdAsync(long id)
        {
            return await DBContext.PlaceImageAssets.Include(x => x.Place).FirstOrDefaultAsync(x => x.PlaceImageAssetId == id);
        }
    }
}
