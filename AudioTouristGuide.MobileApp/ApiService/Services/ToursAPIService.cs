using System.Collections.Generic;
using System.Threading.Tasks;
using AudioTouristGuide.DTO.Models.Tour;
using AudioTouristGuide.MobileApp.ApiService.Interfaces;
using Refit;

namespace AudioTouristGuide.MobileApp.ApiService.Services
{
    public class ToursAPIService : IToursAPIService
    {
        private readonly IApiConnectionService _apiConnectionService;

        public ToursAPIService(IApiConnectionService apiConnectionService)
        {
            _apiConnectionService = apiConnectionService;
        }

        public async Task<IEnumerable<DTOTourDetailedModel>> GetAllToursAsync()
        {
            var toursRefitApi = RestService.For<IToursRefitApi>(_apiConnectionService.ApiUrl);
            return await toursRefitApi.GetAllTours();
        }

        public async Task<DTOTourDetailedModel> GetTourByIdAsync(long id)
        {
            var toursRefitApi = RestService.For<IToursRefitApi>(_apiConnectionService.ApiUrl);
            return await toursRefitApi.GetTourById(id);
        }
    }

    public interface IToursRefitApi
    {
        [Get("/tour/")]
        Task<IEnumerable<DTOTourDetailedModel>> GetAllTours();

        [Get("/tour/{id}")]
        Task<DTOTourDetailedModel> GetTourById(long id);
    }
}
