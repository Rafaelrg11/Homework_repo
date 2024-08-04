using Homework.Models;

namespace Homework.DTOs
{
    public class LoanDTO
    {
        public int IdLoan { get; set; }
        public DateTime DateLoan { get; set; } = DateTime.Now;
        public DateTime DateLoanCompletion { get; set; } = DateTime.Now;

    }
}
