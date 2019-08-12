using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AudioTouristGuide.WebAPI.Database;
using AudioTouristGuide.WebAPI.Database.TourModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AudioTouristGuide.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public ValuesController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/values
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            //await _dbContext.Places.AddAsync(
            //    new Place()
            //    {
            //        Name = "TestPlace",
            //        Description = "Privet"
            //    });
            //await _dbContext.SaveChangesAsync();

            var places = _dbContext.Places.ToList();

            return new JsonResult(places);
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
    }
}
