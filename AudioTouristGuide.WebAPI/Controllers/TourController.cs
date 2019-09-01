using AudioTouristGuide.DTO.Models.AddNewTourZip;
using AudioTouristGuide.DTO.Models.Tour;
using AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels;
using AudioTouristGuide.WebAPI.Database.Entities.TourModels;
using AudioTouristGuide.WebAPI.Database.Interfaces;
using AudioTouristGuide.WebAPI.SwaggerTools.DTOConverters;
using AudioTouristGuide.WebAPI.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AudioTouristGuide.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class TourController : Controller
    {
        private readonly IToursRepository _tourRepository;
        private readonly IPlacesRepository _placesRepository;
        private readonly IAudioAssetsRepository _audioAssetsRepository;
        private readonly IImageAssetsRepository _imageAssetsRepository;

        public TourController(IToursRepository tourRepository, IPlacesRepository placesRepository, 
            IAudioAssetsRepository audioAssetsRepository, IImageAssetsRepository imageAssetsRepository)
        {
            _tourRepository = tourRepository;
            _placesRepository = placesRepository;
            _audioAssetsRepository = audioAssetsRepository;
            _imageAssetsRepository = imageAssetsRepository;
        }

        // GET: api/tour
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var dbTours = await _tourRepository.GetAllAsync();
            var dtoTours = TourDTOConverters.DbTourCollectionToDTO(dbTours);

            return new JsonResult(dtoTours);
        }

        // GET api/tour/5
        [HttpGet("{id}")]
        public async Task<JsonResult> Get(int id)
        {
            var dbTour = await _tourRepository.GetByIdAsync(id);
            var dtoTour = TourDTOConverters.DbTourToDTO(dbTour);

            if (dtoTour == null)
                return new JsonResult(dtoTour) { StatusCode = StatusCodes.Status404NotFound };

            return new JsonResult(dtoTour);
        }

        // POST api/tour
        [HttpPost]
        public async Task<JsonResult> Post(IFormFile formFile)
        {
            TourModel addedTourDto;

            if (!formFile.FileName.Contains("zip"))
                return new JsonResult("The tour archive file should have .zip type") { StatusCode = StatusCodes.Status400BadRequest };

            var tempDirectoryPath = Path.Combine(ApplicationConstants.TempDirectoryPath, Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDirectoryPath);

            var permanentDirectoriesPaths = new List<string>();

            try
            {
                //Unzip files to temporary directory
                using (var zip = new ZipArchive(formFile.OpenReadStream()))
                {
                    var validEntries = zip.Entries.Where(x => ApplicationConstants.FileNameRegex.IsMatch(x.Name));

                    foreach (var zipEntry in validEntries)
                    {
                        zipEntry.ExtractToFile(Path.Combine(tempDirectoryPath, zipEntry.Name));
                    }
                }

                var tempFiles = Directory.GetFiles(tempDirectoryPath);

                if (!tempFiles.Any(x => x.Contains("config.json")))
                    return new JsonResult("config.json not found") { StatusCode = StatusCodes.Status400BadRequest };

                //read tour config and valid critical fields
                var configJson = await System.IO.File.ReadAllTextAsync(tempFiles.First(x => x.Contains("config.json")));
                var config = JsonConvert.DeserializeObject<AddNewTourZipConfig>(configJson);

                var validationErrorMessages = new List<string>();

                if (string.IsNullOrEmpty(config.Name))
                    validationErrorMessages.Add("Name is required");

                if (string.IsNullOrEmpty(config.Description))
                    validationErrorMessages.Add("Description is required");

                if (string.IsNullOrEmpty(config.CountryName))
                    validationErrorMessages.Add("Country name is required");

                if (config.EstimatedDuration.TotalSeconds < 1)
                    validationErrorMessages.Add("Estimated duration is required");

                if (config.GrossPrice.GetValueOrDefault(0) == 0)
                    validationErrorMessages.Add($"GrossPrice should be null or more than 0");

                if (config.Places == null)
                {
                    validationErrorMessages.Add($"Tour places not found");
                }
                else
                {
                    foreach (var place in config.Places)
                    {
                        if (place.AudioTrack == null)
                            validationErrorMessages.Add($"Audio track file info should not be null");
                        else if (!tempFiles.Any(x => x.Contains(place.AudioTrack?.FileName)))
                            validationErrorMessages.Add($"{place.AudioTrack.FileName} file not found");
                    }
                }

                if (validationErrorMessages.Any())
                    return new JsonResult(validationErrorMessages) { StatusCode = StatusCodes.Status400BadRequest };

                //create permanent directory for tour assets and copy assets
                var tourAssetsFolderGuid = Guid.NewGuid().ToString();
                var tourAssetsDirectoryPath = Path.Combine(ApplicationConstants.ToursAssetsDirectoryPath, $"{config.CountryName}_{config.Name}_{tourAssetsFolderGuid}");
                Directory.CreateDirectory(tourAssetsDirectoryPath);
                permanentDirectoriesPaths.Add(tourAssetsDirectoryPath);

                string CopyAssetTo(string assetFileName, string targetDirectoryPath)
                {
                    var tempFilePath = tempFiles.FirstOrDefault(x => x.Contains(assetFileName));
                    if (tempFilePath == null)
                        return null;

                    var tempFileName = tempFilePath.Split(Path.DirectorySeparatorChar).LastOrDefault();
                    if (tempFileName == null)
                        return null;

                    var assetFilePath = Path.Combine(targetDirectoryPath, tempFileName);
                    System.IO.File.Copy(tempFilePath, assetFilePath);

                    return assetFilePath.Substring(assetFilePath.IndexOf("wwwroot", StringComparison.Ordinal) + 8);
                }

                string tourLogoFileUrl = CopyAssetTo(config.LogoFileName, tourAssetsDirectoryPath);
                if (string.IsNullOrEmpty(tourLogoFileUrl))
                    return new JsonResult(null) { StatusCode = StatusCodes.Status500InternalServerError };

                //add tour places to database and copy place assets to place assets directory
                var dbPlaces = new List<Place>();

                foreach (var place in config.Places)
                {
                    var placeAssetsFolderGuid = Guid.NewGuid().ToString();
                    var placeAssetsDirectoryPath = Path.Combine(ApplicationConstants.PlacesAssetsDirectoryPath, $"{config.CountryName}_{place.Name}_{tourAssetsFolderGuid}");
                    Directory.CreateDirectory(placeAssetsDirectoryPath);
                    permanentDirectoriesPaths.Add(placeAssetsDirectoryPath);

                    var dbPlace = new Place()
                    {
                        Name = place.Name,
                        Description = place.Description,
                        Latitude = place.Latitude,
                        Longitude = place.Longitude,
                        DisplayName = place.DisplayName,
                        AssetsFolderGuid = placeAssetsFolderGuid
                    };
                    _placesRepository.Create(dbPlace);
                    await _placesRepository.SaveChangesAsync();

                    var audioTrackUrl = CopyAssetTo(place.AudioTrack.FileName, placeAssetsDirectoryPath);
                    if (audioTrackUrl != null)
                    {
                        var dbAudioAsset = new AudioAsset()
                        {
                            Name = place.AudioTrack.Name,
                            Description = place.AudioTrack.Description,
                            AssetFileUrl = audioTrackUrl,
                            PlaceId = dbPlace.PlaceId
                        };
                        _audioAssetsRepository.Create(dbAudioAsset);
                        await _audioAssetsRepository.SaveChangesAsync();
                    }

                    foreach (var image in place.Images)
                    {
                        var imageUrl = CopyAssetTo(image.FileName, placeAssetsDirectoryPath);
                        if (imageUrl != null)
                        {
                            var dbImageAsset = new ImageAsset()
                            {
                                Name = image.Name,
                                Description = image.Description,
                                PointOfDisplayingStart = image.PointOfDisplayingStart,
                                AssetFileUrl = imageUrl,
                                Place = dbPlace
                            };
                            _imageAssetsRepository.Create(dbImageAsset);
                            await _imageAssetsRepository.SaveChangesAsync();
                        }
                    }

                    DirectoryInfo placeAssetsDirectoryInfo = new DirectoryInfo(placeAssetsDirectoryPath);
                    dbPlace.DataSize = placeAssetsDirectoryInfo.EnumerateFiles().Sum(file => file.Length);
                    _placesRepository.Update(dbPlace);
                    await _placesRepository.SaveChangesAsync();

                    dbPlaces.Add(dbPlace);
                }

                DirectoryInfo tourAssetsDirectoryInfo = new DirectoryInfo(tourAssetsDirectoryPath);
                long tourAssetsSize = tourAssetsDirectoryInfo.EnumerateFiles().Sum(file => file.Length);
                tourAssetsSize += dbPlaces.Sum(x => x.DataSize);

                var dbTour = new Tour()
                {
                    Name = config.Name,
                    Description = config.Description,
                    AssetsFolderGuid = tourAssetsFolderGuid,
                    CountryName = config.CountryName,
                    EstimatedDuration = config.EstimatedDuration,
                    GrossPrice = config.GrossPrice,
                    DataSize = tourAssetsSize,
                    LogoUrl = tourLogoFileUrl
                };
                _tourRepository.Create(dbTour);
                await _tourRepository.SaveChangesAsync();

                var places = (await _placesRepository.GetAllAsync()).Where(x => dbPlaces.Any(y => y.PlaceId == x.PlaceId));
                foreach (var place in places)
                {
                    place.TourPlaces.Add(new TourPlace() { TourId = dbTour.TourId, PlaceId = place.PlaceId });
                    _placesRepository.Update(place);
                }
                await _placesRepository.SaveChangesAsync();

                var addedTour = await _tourRepository.GetByIdAsync(dbTour.TourId);
                addedTourDto = TourDTOConverters.DbTourToDTO(addedTour);
            }
            catch (Exception ex)
            {
                foreach (var path in permanentDirectoriesPaths)
                {
                    Directory.Delete(path, true);
                }

                return new JsonResult(ex) { StatusCode = StatusCodes.Status500InternalServerError };
            }
            finally
            {
                Directory.Delete(tempDirectoryPath, true);
            }

            return new JsonResult(addedTourDto) { StatusCode = StatusCodes.Status201Created };
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> Delete(int id)
        {
            var tourToRemove = await _tourRepository.GetByIdAsync(id);
            if (tourToRemove == null)
                return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };

            //TO DO: add delete related assets

            _tourRepository.Delete(tourToRemove);
            await _tourRepository.SaveChangesAsync();

            

            return new JsonResult("Tour has been removed successfully");
        }
    }
}