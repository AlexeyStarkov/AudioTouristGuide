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
    public class PlaceImageAssetsRepository : RepositoryBase<PlaceImageAssetDbModel>, IPlaceImageAssetsRepository
    {
        public PlaceImageAssetsRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<PlaceImageAssetDbModel>> GetAllAsync()
        {
            return await DBContext.PlaceImageAssetDbModels.Include(x => x.PlaceDbModel).ToListAsync();
        }

        public override async Task<IEnumerable<PlaceImageAssetDbModel>> GetAllAsync(Expression<Func<PlaceImageAssetDbModel, bool>> expression)
        {
            return await DBContext.PlaceImageAssetDbModels.Where(expression).ToListAsync();
        }

        public override async Task<PlaceImageAssetDbModel> GetByIdAsync(long id)
        {
            return await DBContext.PlaceImageAssetDbModels.Include(x => x.PlaceDbModel).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
