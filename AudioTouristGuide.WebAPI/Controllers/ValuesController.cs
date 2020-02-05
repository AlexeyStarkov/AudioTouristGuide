using AudioTouristGuide.WebAPI.Database;
using AudioTouristGuide.WebAPI.Services.Interfaces;
using AudioTouristGuide.WebAPI.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace AudioTouristGuide.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IFileStorageService _blobStorageService;

        public ValuesController(IFileStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        // GET api/values
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            //string filePath = Path.Combine(ApplicationConstants.TempDirectoryPath, "testImage.jpg");
            //await _blobStorageService.UploadFileAsync("TestContainer", filePath);

            var url = _blobStorageService.GetFileUrl("tttt", "d.jpg");

            return new JsonResult(url);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost]
        [Route("UploadFile")]
        public async Task PostFile(IFormFileCollection files)
        {
            //try
            //{
            //    if (formFile != null && formFile.Length > 0)
            //    {
            //        var fileName = Path.GetFileName(formFile.FileName);
            //        var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            //        Directory.CreateDirectory(imagesPath);

            //        using (var fileStream = new FileStream(Path.Combine(imagesPath, fileName), FileMode.Create))
            //        {
            //            await formFile.CopyToAsync(fileStream);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
            
        }
    }
}
