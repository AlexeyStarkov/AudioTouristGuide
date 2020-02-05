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
    public class AudioAssetsRepository : RepositoryBase<AudioAssetDbModel>, IAudioAssetsRepository
    {
        public AudioAssetsRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<AudioAssetDbModel>> GetAllAsync()
        {
            return await DBContext.AudioAssetDbModels.Include(x => x.PlaceDbModels).ToListAsync();
        }

        public override async Task<IEnumerable<AudioAssetDbModel>> GetAllAsync(Expression<Func<AudioAssetDbModel, bool>> expression)
        {
            return await DBContext.AudioAssetDbModels.Where(expression).ToListAsync();
        }

        public override async Task<AudioAssetDbModel> GetByIdAsync(long id)
        {
            return await DBContext.AudioAssetDbModels.Include(x => x.PlaceDbModels).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
