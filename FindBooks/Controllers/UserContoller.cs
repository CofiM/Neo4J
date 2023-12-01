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
    public class UserContoller : ControllerBase
    {

        private readonly UserService userService;

        public UserContoller(UserService userService)
        {
            this.userService = userService;
        }

        [Route("CreateUserAccount/{username}/{mail}/{password}/{passwordConfirm}")]
        [HttpPost]

        public async Task<IActionResult> CreateUserAccount(string username, string mail, string password, string passwordConfirm)
        {
            try
            {
                return Ok(await userService.CreateAccount(username, mail, password, passwordConfirm));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("GetUserAccount/{username}/{password}")]
        [HttpGet]

        public async Task<IActionResult> GetUserAccount(string username, string password)
        {
            try
            {
                return Ok(await userService.GetAccount(username, password));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("UserReadBook/{bookId}/{userId}")]

        public async Task<IActionResult> UserReadBookAsync(int bookId, int userId)
        {
            return Ok(userService.UserReadBook(bookId, userId));
        }

    }
}
