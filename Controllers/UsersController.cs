using Homework.Operations;
using Homework.DTOs;
using Homework.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private UserOperarion _ope;

        public UsersController (UserOperarion ope)
        {
            _ope = ope;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var operations = await _ope.GetUsers();

            return Ok(operations);
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(int idUser)
        {
            var user = await _ope.GetUser(idUser);

            return Ok(user);
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
