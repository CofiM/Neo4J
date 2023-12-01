using FindBooks.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WriterController : ControllerBase
    {
        private readonly WriterService writerService;

        public WriterController(WriterService writer)
        {
            writerService = writer;
        }

        [HttpPost]
        [Route("AddWriter/{firstname}/{lastname}/{birthPlace}/{birthYear}/{yearOfDeath}/{biography}")]
        public IActionResult AddWriter(string firstname, string lastname, string birthPlace, string birthYear, string yearOfDeath, string biography)
        {

            writerService.AddAsync(firstname, lastname, birthPlace, birthYear, yearOfDeath, biography);

            return Ok("Uspesno dodat pisac");
        }

        [HttpGet]
        [Route("GetWriterById/{id}")]
        public IActionResult GetWriterById(int id)
        {
            return Ok(writerService.GetWriterById(id));
        }

        [HttpGet]
        [Route("GetWriterByName/{firstname}/{lastname}")]
        public IActionResult GetWriterByName(string firstname, string lastname)
        {
            return Ok(writerService.GetWriterByName(firstname, lastname));
        }

        [HttpGet]
        [Route("GetAllWriters")]
        public IActionResult GetAllWriters()
        {
            return Ok(writerService.GetAllWriter());
        }

    }
}
