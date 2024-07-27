using Homework.Models;

namespace Homework.DTOs
{
    public class UserDTO
    {
        public int IdUser { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
