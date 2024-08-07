using Homework.DTOs;
using Homework.Models;
using Homework.Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
               var result = await _ope.GetAuthor(idAuthor);
               
                return Ok(result);
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
