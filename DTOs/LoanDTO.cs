using Homework.Models;

namespace Homework.DTOs
{
    public class LoanDTO
    {
        public int IdLoan { get; set; }
        public DateTime DateLoan { get; set; } 
        public DateTime DateLoanCompletion { get; set; }
        public List<AuxiliarTableDTO> AuxiliarTable { get; set; }
    }
}
