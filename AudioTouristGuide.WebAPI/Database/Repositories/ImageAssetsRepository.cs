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
    public class ImageAssetsRepository : RepositoryBase<ImageAsset>, IImageAssetsRepository
    {
        public ImageAssetsRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<ImageAsset>> GetAllAsync()
        {
            return await DBContext.ImageAssets.ToListAsync();
        }

        public override async Task<IEnumerable<ImageAsset>> GetAllAsync(Expression<Func<ImageAsset, bool>> expression)
        {
            return await DBContext.ImageAssets.Where(expression).ToListAsync();
        }

        public override async Task<ImageAsset> GetByIdAsync(long id)
        {
            return await DBContext.ImageAssets.FirstOrDefaultAsync(x => x.ImageAssetId == id);
        }
    }
}
