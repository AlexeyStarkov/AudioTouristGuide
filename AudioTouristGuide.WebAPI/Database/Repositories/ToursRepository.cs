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
    public class ToursRepository : RepositoryBase<TourDbModel>, IToursRepository
    {
        public ToursRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<TourDbModel>> GetAllAsync()
        {
            return await DBContext.TourDbModels
                .Include(t => t.LogoImage)
                .Include(t => t.TourPlaceDbModels)
                    .ThenInclude(tp => tp.PlaceDbModel.AudioAssetDbModel)
                .Include(t => t.TourPlaceDbModels)
                    .ThenInclude(tp => tp.PlaceDbModel.PlaceImageAssetDbModels)
                .ToListAsync();
        }

        public override async Task<TourDbModel> GetByIdAsync(long id)
        {
            return await DBContext.TourDbModels
                .Include(t => t.LogoImage)
                .Include(t => t.TourPlaceDbModels)
                    .ThenInclude(tp => tp.PlaceDbModel.AudioAssetDbModel)
                .Include(t => t.TourPlaceDbModels)
                    .ThenInclude(tp => tp.PlaceDbModel.PlaceImageAssetDbModels).FirstOrDefaultAsync(t => t.Id == id);
        }

        public override async Task<IEnumerable<TourDbModel>> GetAllAsync(Expression<Func<TourDbModel, bool>> expression)
        {
            return await DBContext.TourDbModels.Where(expression).ToListAsync();
        }
    }
}
