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
    public class ImageAssetsRepository : RepositoryBase<ImageAssetDbModel>, IImageAssetsRepository
    {
        public ImageAssetsRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<ImageAssetDbModel>> GetAllAsync()
        {
            return await DBContext.ImageAssetDbModels.ToListAsync();
        }

        public override async Task<IEnumerable<ImageAssetDbModel>> GetAllAsync(Expression<Func<ImageAssetDbModel, bool>> expression)
        {
            return await DBContext.ImageAssetDbModels.Where(expression).ToListAsync();
        }

        public override async Task<ImageAssetDbModel> GetByIdAsync(long id)
        {
            return await DBContext.ImageAssetDbModels.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
