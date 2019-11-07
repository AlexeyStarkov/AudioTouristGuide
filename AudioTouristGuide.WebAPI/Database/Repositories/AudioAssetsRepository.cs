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
    public class AudioAssetsRepository : RepositoryBase<AudioAsset>, IAudioAssetsRepository
    {
        public AudioAssetsRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<AudioAsset>> GetAllAsync()
        {
            return await DBContext.AudioAssets.Include(x => x.Place).ToListAsync();
        }

        public override async Task<IEnumerable<AudioAsset>> GetAllAsync(Expression<Func<AudioAsset, bool>> expression)
        {
            return await DBContext.AudioAssets.Where(expression).ToListAsync();
        }

        public override async Task<AudioAsset> GetByIdAsync(long id)
        {
            return await DBContext.AudioAssets.Include(x => x.Place).FirstOrDefaultAsync(x => x.AudioAssetId == id);
        }
    }
}
