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
            await _ope.GetBook(idbook);

            var libroConAutor = _context.Books.Include(l => l.Author).Where(l => l.IdBook == idbook).Select(l => new BookDTO
            {
                IdBook = idbook,
                IdAutor = l.IdAutor,
                Name = l.Name,
                Gender = l.Gender,
                NumPags = l.NumPags,
                Available = l.Available,
                NameAutor = l.Author.Name,
                EmailAutor = l.Author.Email
            });

            return Ok(libroConAutor);
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
