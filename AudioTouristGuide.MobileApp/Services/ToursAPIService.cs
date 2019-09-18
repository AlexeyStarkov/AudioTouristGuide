using System.Collections.Generic;
using System.Threading.Tasks;
using AudioTouristGuide.DTO.Models.Tour;
using AudioTouristGuide.MobileApp.Interfaces;
using Refit;

namespace AudioTouristGuide.MobileApp.Services
{
    public class ToursAPIService : IToursAPIService
    {
        private readonly IApiConnectionService _apiConnectionService;

        public ToursAPIService(IApiConnectionService apiConnectionService)
        {
            _apiConnectionService = apiConnectionService;
        }

        public async Task<IEnumerable<DTOTourModel>> GetAllTours()
        {
            var toursRefitApi = RestService.For<IToursRefitApi>(_apiConnectionService.ApiUrl);
            return await toursRefitApi.GetAllTours();
        }

        public async Task<DTOTourModel> GetTourById(long id)
        {
            var toursRefitApi = RestService.For<IToursRefitApi>(_apiConnectionService.ApiUrl);
            return await toursRefitApi.GetTourById(id);
        }
    }

    public interface IToursRefitApi
    {
        [Get("/tour/")]
        Task<IEnumerable<DTOTourModel>> GetAllTours();

        [Get("/tour/{id}")]
        Task<DTOTourModel> GetTourById(long id);
    }
}
