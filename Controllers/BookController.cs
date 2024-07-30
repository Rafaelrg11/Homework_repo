using Homework.DTOs;
using Homework.Models;
using Homework.Operations;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private BookOperation _ope;

        public BookController(BookOperation ope)
        {
            _ope = ope;
        }

        [HttpGet("GetBooks")]
        public async Task<IActionResult> GetBooks()
        {
            var operation = await _ope.GetBooks();

            return Ok(operation);
        }

        [HttpGet("GetBook")]
        public async Task<IActionResult> GetBook(int idbook)
        {
            var operation = await _ope.GetBook(idbook);
            
            return Ok(operation);
        }

        [HttpPost("CreateBook")]
        public async Task<IActionResult> CreateBook([FromBody] BookDTO bookDTO)
        {
            Book result = new Book()
            {
                Name = bookDTO.Name,
                IdAutor = bookDTO.IdAutor,
                Gender = bookDTO.Gender,
                NumPags = bookDTO.NumPags,
                Available = bookDTO.Available
            };

            var operation = await _ope.CreateBook(result);

            return Ok(operation);
        }

        [HttpPut("UpdateBook/{id}")]
        public async Task<bool> UpdateBook(BookDTO bookDTO)
        {
            var operation = await _ope.UpdateBook(bookDTO);

            return operation;
        }

        [HttpDelete("DeleteBook/{id}")]
        public async Task<bool> DeleteBook(int idbook)
        {
            bool result = await _ope.DeleteBook(idbook);
            
            return result;
        }

    } 
}
