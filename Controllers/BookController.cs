using Homework.DTOs;
using Homework.Models;
using Homework.Operations;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace Homework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private BookOperation _ope;

        private HomeworkContext _context;

        public BookController(HomeworkContext homeworkContext, BookOperation ope)
        {
            _ope = ope;

            _context = homeworkContext;
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
           var result = await _ope.GetBook(idbook);           

            return Ok(result);
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

        [HttpPut("ReturnBook/{id}")]
        public async Task<bool> ReturnBook(ReturnBookDTO bookDTO )
        {
            var operation = await _ope.ReturnBook(bookDTO);

            return operation;
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
