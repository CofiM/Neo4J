using FindBooks.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindBooks.Controllers
{
    [ApiController]
    public class BookController : ControllerBase
    {
        public readonly BookService bookService;

        public BookController(BookService book)
        {
            bookService = book;
        }

        //Mislim da ne treba
        [HttpPost]
        [Route("AddBook/{title}/{yearOfPublication}/{description}/{idGenre}/{idWriter}")]
        public IActionResult AddBook(string title, string yearOfPublication, string description, int idGenre, int idWriter)
        {

            int res = bookService.AddBook(title, yearOfPublication, description, idGenre, idWriter);
            return Ok(res);
        }

        //Mislim da ne treba
        [HttpPost]
        [Route("AddBookAndWriter/{title}/{yearOfPublication}/{description}/{idGenre}/{firstname}/{lastname}/{birthPlace}/{birthYear}/{yearOfDeath}/{biography}")]
        public IActionResult AddBookAndWriter(string title, string yearOfPublication, string description, int idGenre,
                            string firstname, string lastname, string birthPlace, string birthYear, string yearOfDeath, string biography)
        {
            int res = bookService.AddBookAndWriter(title, yearOfPublication, description, idGenre, firstname, lastname, birthPlace, birthYear, yearOfDeath, biography);
            return Ok(res);
        }

        [HttpPost]
        [Route("AddBookByPublishingHouse/{idHouse}/{title}/{yearOfPublication}/{description}/{idGenre}/{firstname}/{lastname}/{birthPlace}/{birthYear}/{yearOfDeath}/{biography}")]
        public IActionResult AddBookByPublishingHouse(int idHouse, string title, string yearOfPublication, string description, int idGenre,
                            string firstname, string lastname, string birthPlace, string birthYear, string yearOfDeath, string biography)
        {
            int res = bookService.AddBookByPublishingHouse(idHouse, title, yearOfPublication, description, idGenre, firstname, lastname, birthPlace, birthYear, yearOfDeath, biography);
            return Ok(res);
        }

        [HttpPost]
        [Route("AddBookByPublishingHouseWithExistingWriter/{idHouse}/{title}/{yearOfPublication}/{description}/{idGenre}/{firstname}/{lastname}")]
        public IActionResult AddBookByPublishingHouseWithExistingWriter(int idHouse, string title, string yearOfPublication, string description, int idGenre,
                            string firstname, string lastname)
        {
            int res = bookService.AddBookByPublishingHouseWithExistingWriter(idHouse, title, yearOfPublication, description, idGenre, firstname, lastname);
            return Ok(res);
        }

        [HttpPost]
        [Route("CreateRelationship/{idBook}/{idGenre}/{idWriter}")]
        public IActionResult CreateRelationship(int idBook, int idGenre, int idWriter)
        {
            var obj = bookService.CreateRelationship(idBook, idGenre, idWriter);
            return Ok(obj);
        }

        [HttpGet]
        [Route("GetBookByPublishingHouse/{idHouse}")]
        public IActionResult GetBooksByPublishingHouse(int idHouse)
        {
            var res = bookService.GetBookByPublishingHouse(idHouse);
            return Ok(res);
        }

        [HttpGet]
        [Route("GetBooksReadByUser/{idUser}")]
        public IActionResult GetBooksReadByUser(int idUser)
        {
            var res = bookService.GetBooksReadByUser(idUser);
            return Ok(res);
        }


        [HttpPost]
        [Route("PublishBook/{idBook}/{houseId}")]
        public IActionResult PublishBook(int idBook, int houseId)
        {
            bookService.PublishBook(houseId, idBook);
            return Ok();
        }

        [HttpPost]
        [Route("ReadBook/{bookName}/{idUser}")]
        public IActionResult ReadBook(string bookName, int idUser)
        {
            bookService.ReadBook(bookName, idUser);
            return Ok("Success");
        }

        [HttpGet]
        [Route("GetAllBooks")]
        public IActionResult GetAllBook()
        {
            var books = bookService.GetAllBookAsync();
            return Ok(books);
        }
        
        [HttpGet]
        [Route("GetBooksForGenre/{nameGenre}/{userId}")]
        public IActionResult GetBooksForGenre(string nameGenre, int userId)
        {
            return Ok(bookService.GetBooksByGenre(nameGenre, userId));
        }
        

        [HttpGet]
        [Route("GetBook/{id}")]
        public IActionResult GetBook(int id)
        {
            return Ok(bookService.GetBook(id));
        }

        [HttpGet]
        [Route("GetBookByName/{name}")]
        public IActionResult GetBookByName(string name)
        {
            return Ok(bookService.GetBookByBookName(name));
        }

        [HttpGet]
        [Route("GetBookById/{bookId}")]
        public IActionResult GetBookById(int bookId)
        {
            return Ok(bookService.GetBookById(bookId));
        }

        [HttpGet]
        [Route("GetBooksByWriter/{firstname}/{lastname}/{userId}")]
        public IActionResult GetBooksByWriter(string firstname, string lastname, int userId)
        {
            return Ok(bookService.GetBooksByWriter(firstname,lastname, userId));
        }

        [HttpGet]
        [Route("GetBooksByUser/{userId}")]
        public IActionResult GetBooksByUser(int userId)
        {
            return Ok(bookService.GetBooksByUser(userId));
        }


    }
}
