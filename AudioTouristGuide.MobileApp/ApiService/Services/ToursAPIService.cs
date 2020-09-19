using System.Collections.Generic;
using System.Threading.Tasks;
using AudioTouristGuide.DTO.Models.Tour;
using AudioTouristGuide.MobileApp.ApiService.Interfaces;
using Refit;

namespace AudioTouristGuide.MobileApp.ApiService.Services
{
    public class ToursAPIService : IToursAPIService
    {
        private readonly string _apiUrl;

        public ToursAPIService()
        {
            _apiUrl = App.ApiUrl;
        }

        public async Task<IEnumerable<DTOTourDetailedModel>> GetAllToursAsync()
        {
            var toursRefitApi = RestService.For<IToursRefitApi>(_apiUrl);
            return await toursRefitApi.GetAllTours();
        }

        public async Task<DTOTourDetailedModel> GetTourByIdAsync(long id)
        {
            var toursRefitApi = RestService.For<IToursRefitApi>(_apiUrl);
            return await toursRefitApi.GetTourById(id);
        }
    }

    public interface IToursRefitApi
    {
        [Get("/tours/")]
        Task<IEnumerable<DTOTourDetailedModel>> GetAllTours();

        [Get("/tours/GetTourById/{id}")]
        Task<DTOTourDetailedModel> GetTourById(long id);
    }
}
