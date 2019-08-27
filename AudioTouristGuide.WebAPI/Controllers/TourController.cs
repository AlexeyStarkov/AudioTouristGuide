using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using AudioTouristGuide.DTO.Models.AddNewTourZip;
using AudioTouristGuide.WebAPI.Database;
using AudioTouristGuide.WebAPI.Database.JoinTablesModels;
using AudioTouristGuide.WebAPI.Database.TourModels;
using AudioTouristGuide.WebAPI.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AudioTouristGuide.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class TourController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public TourController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/tour
        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(_dbContext.Tours.ToList());
        }

        // GET api/tour/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            return new JsonResult(_dbContext.Tours.FirstOrDefault(x => x.TourId == id));
        }

        // POST api/tour
        [HttpPost]
        public async Task<JsonResult> Post(IFormFile formFile)
        {
            var tempDirectoryPath = Path.Combine(ApplicationConstants.TempDirectoryPath, Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDirectoryPath);

            if (!formFile.FileName.Contains("zip"))
                return new JsonResult("The tour archive file should have .zip type") { StatusCode = 400 };

            try
            {
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
                    return new JsonResult("config.json not found") { StatusCode = 400 };

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
                    return new JsonResult(validationErrorMessages) { StatusCode = 400 };

                var tourAssetsFolderGuid = Guid.NewGuid().ToString();
                var tourAssetsDirectoryPath = Path.Combine(ApplicationConstants.ToursAssetsDirectoryPath, $"{config.CountryName}_{config.Name}_{tourAssetsFolderGuid}");
                Directory.CreateDirectory(tourAssetsDirectoryPath);

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

                    return assetFilePath.Substring(assetFilePath.LastIndexOf("wwwroot", StringComparison.Ordinal) + 1);
                }

                string tourLogoFileUrl = CopyAssetTo(config.LogoFileName, tourAssetsDirectoryPath);
                if (string.IsNullOrEmpty(tourLogoFileUrl))
                    return new JsonResult(null) { StatusCode = 500 };

                var dbPlaces = new List<Place>();

                foreach (var place in config.Places)
                {
                    var placeAssetsFolderGuid = Guid.NewGuid().ToString();
                    var placeAssetsDirectoryPath = Path.Combine(ApplicationConstants.PlacesAssetsDirectoryPath, $"{config.CountryName}_{place.Name}_{tourAssetsFolderGuid}");
                    Directory.CreateDirectory(placeAssetsDirectoryPath);

                    var dbPlace = new Place()
                    {
                        Name = place.Name,
                        Description = place.Description,
                        Latitude = place.Latitude,
                        Longitude = place.Longitude,
                        DisplayName = place.DisplayName
                    };
                    await _dbContext.Places.AddAsync(dbPlace);
                    await _dbContext.SaveChangesAsync();

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
                        await _dbContext.AudioAssets.AddAsync(dbAudioAsset);
                        await _dbContext.SaveChangesAsync();
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
                            await _dbContext.ImageAssets.AddAsync(dbImageAsset);
                            await _dbContext.SaveChangesAsync();
                        }
                    }

                    DirectoryInfo placeAssetsDirectoryInfo = new DirectoryInfo(placeAssetsDirectoryPath);
                    dbPlace.DataSize = placeAssetsDirectoryInfo.EnumerateFiles().Sum(file => file.Length);
                    await _dbContext.SaveChangesAsync();

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
                await _dbContext.Tours.AddAsync(dbTour);
                await _dbContext.SaveChangesAsync();

                foreach (var dbPlace in dbPlaces)
                {
                    dbPlace.TourPlaces.Add(new TourPlace() { TourId = dbTour.TourId, PlaceId = dbPlace.PlaceId });
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new JsonResult(ex) { StatusCode = 500 };
            }
            finally
            {
                Directory.Delete(tempDirectoryPath, true);
            }

            return new JsonResult(null);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            
        }
    }
}