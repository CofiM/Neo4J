using FindBooks.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FindBooks.Controllers
{
    
    [ApiController]
    public class PublishingHouseController : ControllerBase
    {
        public readonly PublishingHouseService service;

        public PublishingHouseController(PublishingHouseService s)
        {
            service = s;
        }

        [HttpGet]
        [Route("GetAllHouses")]
        public IActionResult GetHouses()
        {
            return Ok(service.GetAllHouse());
        }

        [HttpPost]
        [Route("AddHouse/{name}/{year}/{email}/{contact}/{place}/{password}")]
        public async Task<IActionResult> AddHouse(string name, string year, string email, string contact, string place, string password)
        {
            await service.AddHouseAsync(name, year, email, contact, place, password);
            return Ok("DODATO");
        }

    }
}
