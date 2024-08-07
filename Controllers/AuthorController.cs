using Homework.DTOs;
using Homework.Models;
using Homework.Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using static System.Reflection.Metadata.BlobBuilder;

namespace Homework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : Controller
    {
        private AuthorOperation _ope;

        private HomeworkContext _context;

        public AuthorController(HomeworkContext homework,AuthorOperation ope)
        {
            _ope = ope;

            _context = homework;
        }

        [HttpGet("GetAuthors")]
        public async Task<IActionResult> GetAuthors()
        {
            var operation = await _ope.GetAuthors();

            return Ok(operation);
        }
        [HttpGet("GetAuthor/{idAuthor}")]
        public async Task<IActionResult> GetAuthor(int idAuthor)
        {
            try
            {

                await _ope.GetAuthor(idAuthor);

                var authorWithBooks = await _context.Authors.Where(a => a.IdAuthor == idAuthor).Select(a => new AuthorDTO
                {
                    IdAuthor = idAuthor,
                    Name = a.Name,
                    Email = a.Email,
                    Books = a.Books.Select(l => new BooksDto2
                    {
                        IdBook = l.IdBook,
                        IdAutor = idAuthor,
                        Name = l.Name,
                        Gender = l.Gender,
                        NumPags = l.NumPags,
                        Available = l.Available,
                    }).ToList()
                }).ToListAsync();

                return Ok(authorWithBooks);

            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateAuthor")]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorDTO authorDTO)
        {
            Author author = new Author()
            {
                Name = authorDTO.Name,
                Email = authorDTO.Email,
            };
            var operation = await _ope.CreateAuthor(author);

            return Ok(operation);
        }

        [HttpPut("UpdateAuthor/{id}")]
        public async Task<bool> UpdateAuthor(AuthorDTO authorDTO)
        {
            var operation = await _ope.UpdateAuthor(authorDTO);

            return operation;
        }

        [HttpDelete("DeleteAuthor/{id}")]
        public async Task<bool> DeleteAuthor(int id)
        {
            bool operation = await _ope.DeleteAuthor(id);

            return operation;
        }
    }
}
