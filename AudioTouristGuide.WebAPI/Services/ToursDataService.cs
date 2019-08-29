using System;
using System.Threading.Tasks;
using AudioTouristGuide.WebAPI.Database.Entities.TourModels;
using AudioTouristGuide.WebAPI.Database.Interfaces;

namespace AudioTouristGuide.WebAPI.Services
{
    public class ToursDataService
    {
        private readonly ITourRepository _tourRepository;

        public ToursDataService(ITourRepository tourRepository)
        {
            _tourRepository = tourRepository;
        }

        public async Task<Tour> GetFullTourDataByIdAsync(long id) => await _tourRepository.GetFullTourDataByIdAsync(id);
    }
}
