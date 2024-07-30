using Homework.Models;

namespace Homework.DTOs
{
    public class LoanDTO
    {
        public int IdLoan { get; set; }

        public int IdBook { get; set; }

        public int IdUser { get; set; }

        public DateOnly DateLoan { get; set; }

        public DateOnly DateLoanCompletion { get; set; }

    }
}
