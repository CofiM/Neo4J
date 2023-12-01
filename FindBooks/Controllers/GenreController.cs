using FindBooks.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FindBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        public readonly GenreService genreService;

        public GenreController(GenreService genre)
        {
            genreService = genre;
        }

        [HttpPost]
        [Route("CreateGenre/{name}")]
        public IActionResult CreateGenre(string name)
        {
            int id = genreService.AddGenreAsync(name);
            return Ok(id);
        }

        [HttpGet]
        [Route("GetAllGenre")]
        public IActionResult GetGenres()
        {
            return Ok(genreService.GetAllGenre());
        }

        [HttpGet]
        [Route("GetGenre/{name}")]
        public IActionResult GetGenre(string name)
        {
            return Ok(genreService.GetGenreName(name));
        }





    }
}