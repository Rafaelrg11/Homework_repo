using Homework.Operations;
using Homework.DTOs;
using Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Homework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private UserOperarion _ope;

        private HomeworkContext _context;

        public UsersController (HomeworkContext homeworkContext , UserOperarion ope)
        {
            _ope = ope;

            _context = homeworkContext;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                await _ope.GetUsers();

                var allUsers = await _context.Users.Select(a => new UserDTO
                {
                    IdUser = a.IdUser,
                    Name = a.Name,
                    Email = a.Email,
                    AuxiliarTable = a.AuxiliarTable.Select(a => new AuxiliarTableDTO
                    {
                        IdAuxiliar = a.IdAuxiliar,
                        IdBook = a.IdBook,
                        IdLoan = a.IdLoan,
                        IdUser = a.IdUser,
                    }).ToList()
                }).ToListAsync();

                return Ok(allUsers);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }

            }

        [HttpGet("GetUser/{idUser}")]
        public async Task<IActionResult> GetUser(int idUser)
        {
            try
            {
                await _ope.GetUser(idUser);

                var user = await _context.Users.Where(a => a.IdUser == idUser).Select(a => new UserDTO
                {
                    Name = a.Name,
                    Email = a.Email,
                    IdUser = idUser,
                    AuxiliarTable = a.AuxiliarTable.Select(a => new AuxiliarTableDTO
                    {
                        IdUser = a.IdUser,
                        IdBook = a.IdBook,
                        IdLoan = a.IdLoan,
                    }).ToList()
                }).ToListAsync();

                return Ok(user);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO)
        {
            User user = new User()
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
            };
            var operation = await _ope.CreateUser(user);

            return Ok(operation);
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<bool> UpdateUser(UserDTO userDTO)
        {
            var operation = await _ope.UpdateUser(userDTO);

            return operation;
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<bool> DeleteUser(int idUser)
        {
            bool operation = await _ope.DeleteUser(idUser);

            return operation;
        }
    }
}
