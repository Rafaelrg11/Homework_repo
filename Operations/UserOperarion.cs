using Homework.DTOs;
using Homework.Models;
using Microsoft.EntityFrameworkCore;

namespace Homework.Operations
{
    public class UserOperarion
    {
        public HomeworkContext _context;

        public UserOperarion(HomeworkContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsers()
        {
            var user = await _context.Users.AsNoTracking().ToListAsync();

            return user;
        }

        public async Task<User> GetUser(int iduser)
        {
            var operation = await _context.Users.FindAsync(iduser);

            return operation;
        }

        public async Task<User> CreateUser(User user)
        {
            var result = await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UpdateUser(UserDTO userdto)
        {
            User? user = await _context.Users.FindAsync(userdto);
            if (user != null) 
            {
                user.Name = userdto.Name;
                user.Email = userdto.Email;

                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> DeleteUser(int iduser)
        {
            var result = await _context.Users.FindAsync(iduser);
            if (result != null) 
            {
                return false;
            }
            _context.Users.Remove(result);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
