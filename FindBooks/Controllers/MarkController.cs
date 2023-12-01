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
    public class MarkController : ControllerBase
    {
        private readonly MarkService markService;

        public MarkController(MarkService service)
        {
            this.markService = service;
        }

        [HttpPost]
        [Route("AddMark/{grade}/{userId}/{bookName}")]
        public async Task<ActionResult> AddMark(int grade, int userId, string bookName)
        {
            try
            {
                int mark = await markService.AddMarkAsync(grade);
                await markService.MarkBook(mark, bookName);
                await markService.MarkUser(mark,userId);
                await markService.BookUser(bookName, userId);
                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteMark/{markId}")]
        public async Task<ActionResult> DeleteMark(int markId)
        {
            try
            {
                await markService.DeleteMarkAsync(markId);
                return Ok("Success");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
                
        }

        [HttpGet]
        [Route("GetUsersMarkForBook/{userId}/{bookId}")]
        public async Task<ActionResult> GetUsersMarkForBook(int userId,int bookId)
        {
            try
            {
                //fali return za 
                float res = await markService.GetUsersMarkForBook(userId, bookId);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GetAvgMarkForBook/{bookId}")]
        public async Task<ActionResult> GetAvgMarkForBook(int bookId)
        {
            try
            {
                float grade = await markService.GetAverageMarkForBook(bookId);
                return Ok(grade);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
       


