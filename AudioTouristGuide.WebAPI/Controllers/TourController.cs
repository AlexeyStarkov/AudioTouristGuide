using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AudioTouristGuide.WebAPI.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AudioTouristGuide.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class TourController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/tour
        [HttpPost]
        public async Task<JsonResult> Post(IFormFile formFile)
        {
            using (var zip = new ZipArchive(formFile.OpenReadStream()))
            {
                var validFiles = zip.Entries.Where(x => ApplicationConstants.FileNameRegex.IsMatch(x.Name));

                var archiveConfig = validFiles.FirstOrDefault(x => x.Name == "config.json");

                //add getting resources from archive using config

                var placeAssetsPath = ApplicationConstants.TourPlacesAssetsDirectoryPath;
                Directory.CreateDirectory(placeAssetsPath);

                foreach (var file in validFiles)
                {
                    


                }
            }

            return new JsonResult(null);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
