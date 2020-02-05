﻿using AudioTouristGuide.DTO.Models.AddNewTourZip;
using AudioTouristGuide.DTO.Models.Tour;
using AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels;
using AudioTouristGuide.WebAPI.Database.Entities.TourModels;
using AudioTouristGuide.WebAPI.Database.Interfaces;
using AudioTouristGuide.WebAPI.Services.Interfaces;
using AudioTouristGuide.WebAPI.Storage.Models;
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
    [Route("api/v1/[controller]")]
    public class ToursController : Controller
    {
        private readonly IToursRepository _toursRepository;
        private readonly IPlacesRepository _placesRepository;
        private readonly IAudioAssetsRepository _audioAssetsRepository;
        private readonly IImageAssetsRepository _imageAssetsRepository;
        private readonly IPlaceImageAssetsRepository _placeImageAssetsRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly TourDTOConverters _tourDTOConverters;

        public ToursController(IToursRepository toursRepository, IPlacesRepository placesRepository, 
            IAudioAssetsRepository audioAssetsRepository, IImageAssetsRepository imageAssetsRepository,
            IFileStorageService fileStorageService, TourDTOConverters tourDTOConverters,
            IPlaceImageAssetsRepository placeImageAssetsRepository)
        {
            _toursRepository = toursRepository;
            _placesRepository = placesRepository;
            _audioAssetsRepository = audioAssetsRepository;
            _imageAssetsRepository = imageAssetsRepository;
            _fileStorageService = fileStorageService;
            _tourDTOConverters = tourDTOConverters;
            _placeImageAssetsRepository = placeImageAssetsRepository;
        }

        // GET: api/tours
        [HttpGet("")]
        public async Task<JsonResult> GetAllTours()
        {
            var dbTours = await _toursRepository.GetAllAsync();
            return new JsonResult(_tourDTOConverters.DbTourDetailedCollectionToDTO(dbTours));
        }

        // GET api/tours
        [HttpGet("GetTourById/{id}")]
        public async Task<JsonResult> GetTourById(int id)
        {
            var dbTour = await _toursRepository.GetByIdAsync(id);
            var dtoTour = _tourDTOConverters.DbTourToDTOModel(dbTour);

            if (dtoTour == null)
                return new JsonResult(dtoTour) { StatusCode = StatusCodes.Status404NotFound };

            return new JsonResult(dtoTour);
        }

        // POST api/tours
        [HttpPost]
        public async Task<JsonResult> Post(IFormFile formFile)
        {
            DTOTourDetailedModel addedTourDto;

            if (!formFile.FileName.Contains("zip"))
                return new JsonResult("The tour archive file should have .zip type") { StatusCode = StatusCodes.Status400BadRequest };

            var tempDirectoryPath = Path.Combine(ApplicationConstants.TempDirectoryPath, Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDirectoryPath);

            var containersNames = new List<string>();

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

                //tour config reading and critical fields validation
                var configJson = await System.IO.File.ReadAllTextAsync(tempFiles.First(x => x.Contains("config.json")));
                var config = JsonConvert.DeserializeObject<AddNewTourZipConfig>(configJson);

                var validationErrorMessages = new List<string>();

                if (string.IsNullOrEmpty(config.Name))
                    validationErrorMessages.Add("Name is required");

                if (string.IsNullOrEmpty(config.Description))
                    validationErrorMessages.Add("Description is required");

                if (string.IsNullOrEmpty(config.CountryName))
                    validationErrorMessages.Add("Country name is required");

                if (string.IsNullOrEmpty(config.Settlement))
                    validationErrorMessages.Add("Settlement name is required");

                if (config.EstimatedDuration.TotalSeconds < 1)
                    validationErrorMessages.Add("Estimated duration is required");

                if (config.GrossPrice.HasValue && config.GrossPrice.Value == 0)
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

                //upload tour assets to azure blob storage
                var tourAssetsContainerGuid = Guid.NewGuid().ToString().Replace("-",string.Empty);
                var tourAssetsContainerName = $"{config.CountryName.Replace(" ", string.Empty)}-{config.Settlement.Replace(" ", string.Empty)}-{config.Name.Replace(" ", string.Empty)}-{tourAssetsContainerGuid}";
                tourAssetsContainerName = tourAssetsContainerName.Length > 62 ? tourAssetsContainerName.Substring(0, 62) : tourAssetsContainerName;
                var tourLogoFileName = config.CoverImageFileName;
                containersNames.Add(tourAssetsContainerName);

                async Task<FileSavingResult> UploadAssetToAsync(string assetFileName, string targetContainerName)
                {
                    var tempFilePath = tempFiles.FirstOrDefault(x => x.Contains(assetFileName));
                    if (tempFilePath == null)
                        return new FileSavingResult(false, null, null);

                    var tempFileName = tempFilePath.Split(Path.DirectorySeparatorChar).LastOrDefault();
                    if (tempFileName == null)
                        return new FileSavingResult(false, null, null);

                    return await _fileStorageService.SaveFileAsync(targetContainerName, tempFilePath, tempFileName);
                }

                var tourAssetsUploadingResult = await UploadAssetToAsync(tourLogoFileName, tourAssetsContainerName);
                if (!tourAssetsUploadingResult.HasSuccess)
                    return new JsonResult(null) { StatusCode = StatusCodes.Status500InternalServerError };

                //add tour places to database and upload place assets to azure blob storage
                var dbPlaces = new List<Place>();

                foreach (var place in config.Places)
                {
                    var placeAssetsContainerGuid = Guid.NewGuid().ToString().Replace("-", string.Empty);
                    var placeAssetsContainerName = $"{config.CountryName.Replace(" ", string.Empty)}-{config.Settlement.Replace(" ", string.Empty)}-{place.Name.Replace(" ", string.Empty)}-{placeAssetsContainerGuid}";
                    placeAssetsContainerName = placeAssetsContainerName.Length > 62 ? placeAssetsContainerName.Substring(0, 62) : placeAssetsContainerName;
                    containersNames.Add(placeAssetsContainerName);

                    var dbPlace = new Place()
                    {
                        Name = place.Name,
                        Description = place.Description,
                        Latitude = place.Latitude,
                        Longitude = place.Longitude,
                        DisplayName = place.DisplayName,
                        AssetsContainerName = placeAssetsContainerName
                    };
                    _placesRepository.Create(dbPlace);
                    await _placesRepository.SaveChangesAsync();

                    var audioTrackFileName = place.AudioTrack.FileName;
                    var audioTrackUploadingResult = await UploadAssetToAsync(audioTrackFileName, placeAssetsContainerName);
                    if (audioTrackUploadingResult.HasSuccess)
                    {
                        var dbAudioAsset = new AudioAsset()
                        {
                            Name = place.AudioTrack.Name,
                            Description = place.AudioTrack.Description,
                            AssetContainerName = audioTrackUploadingResult.ContainerName,
                            AssetFileName = audioTrackUploadingResult.FileName,
                            PlaceId = dbPlace.PlaceId,
                            LastUpdate = DateTime.Now
                        };
                        _audioAssetsRepository.Create(dbAudioAsset);
                        await _audioAssetsRepository.SaveChangesAsync();
                    }

                    foreach (var image in place.Images)
                    {
                        var imageFileName = image.FileName;
                        var imageUploadingResult = await UploadAssetToAsync(imageFileName, placeAssetsContainerName);
                        if (imageUploadingResult.HasSuccess)
                        {
                            var dbImageAsset = new PlaceImageAsset()
                            {
                                Name = image.Name,
                                Description = image.Description,
                                PointOfDisplayingStart = image.PointOfDisplayingStart,
                                AssetContainerName = imageUploadingResult.ContainerName,
                                AssetFileName = imageUploadingResult.FileName,
                                Place = dbPlace,
                                LastUpdate = DateTime.Now
                            };
                            _placeImageAssetsRepository.Create(dbImageAsset);
                            await _imageAssetsRepository.SaveChangesAsync();
                        }
                    }

                    var placeContainerInfo = _fileStorageService.GetFileContainerInfo(placeAssetsContainerName);
                    dbPlace.DataSize = placeContainerInfo != null ? placeContainerInfo.TotalBytes : 0;
                    _placesRepository.Update(dbPlace);
                    await _placesRepository.SaveChangesAsync();

                    dbPlaces.Add(dbPlace);
                }

                var tourContainerInfo = _fileStorageService.GetFileContainerInfo(tourAssetsContainerName);
                long tourAssetsSize = tourContainerInfo != null ? tourContainerInfo.TotalBytes : 0;
                tourAssetsSize += dbPlaces.Sum(x => x.DataSize);

                var dbTour = new Tour()
                {
                    Name = config.Name,
                    Description = config.Description,
                    CountryName = config.CountryName,
                    Settlement = config.Settlement,
                    EstimatedDuration = config.EstimatedDuration,
                    GrossPrice = config.GrossPrice,
                    DataSize = tourAssetsSize,
                    LogoImage = new ImageAsset()
                    {
                        Name = tourLogoFileName,
                        AssetContainerName = tourAssetsUploadingResult.ContainerName,
                        AssetFileName = tourAssetsUploadingResult.FileName,
                        LastUpdate = DateTime.Now
                    }
                };
                _toursRepository.Create(dbTour);
                await _toursRepository.SaveChangesAsync();

                var places = (await _placesRepository.GetAllAsync()).Where(x => dbPlaces.Any(y => y.PlaceId == x.PlaceId));
                foreach (var place in places)
                {
                    place.TourPlaces.Add(new TourPlace() { TourId = dbTour.TourId, PlaceId = place.PlaceId });
                    _placesRepository.Update(place);
                }
                await _placesRepository.SaveChangesAsync();

                var addedTour = await _toursRepository.GetByIdAsync(dbTour.TourId);
                addedTourDto = _tourDTOConverters.DbTourToDTOModel(addedTour);
            }
            catch (Exception ex)
            {
                foreach (var containerName in containersNames)
                {
                    await _fileStorageService.RemoveFileContainerAsync(containerName);
                }

                return new JsonResult(ex) { StatusCode = StatusCodes.Status500InternalServerError };
            }
            finally
            {
                Directory.Delete(tempDirectoryPath, true);
            }

            return new JsonResult(addedTourDto) { StatusCode = StatusCodes.Status201Created };
        }

        // DELETE api/tours/{id}
        [HttpDelete("{id}")]
        public async Task<JsonResult> Delete(int id)
        {
            var tourToRemove = await _toursRepository.GetByIdAsync(id);
            if (tourToRemove == null)
                return new JsonResult($"Tour with id {id} not found") { StatusCode = StatusCodes.Status404NotFound };

            var removingTasks = new List<Task>();
            if (!string.IsNullOrEmpty(tourToRemove.LogoImage?.AssetContainerName))
                removingTasks.Add(_fileStorageService.RemoveFileContainerAsync(tourToRemove.LogoImage.AssetContainerName));

            foreach (var place in tourToRemove.TourPlaces)
            {
                if (!string.IsNullOrEmpty(place.Place?.AssetsContainerName))
                    removingTasks.Add(_fileStorageService.RemoveFileContainerAsync(place.Place.AssetsContainerName));
            }

            await Task.WhenAll(removingTasks);

            _toursRepository.Delete(tourToRemove);
            await _toursRepository.SaveChangesAsync();

            return new JsonResult("The tour has been removed successfully");
        }
    }
}
