using Homework.DTOs;
using Homework.Models;
using Homework.Operations;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {
        private LoanOperation _loanOperation;

        public LoanController(LoanOperation loanOperation) 
        {
            _loanOperation = loanOperation;
        }

        [HttpGet("GetLoans")]
        public async Task<IActionResult> GetLoans()
        {
            var operation = await _loanOperation.GetLoans();

            return Ok(operation);
        }

        [HttpGet("GetLoan")]
        public async Task<IActionResult> GetLoan(int idLoan)
        {
            var operation = await _loanOperation.GetLoan(idLoan);

            return Ok(operation);
        }

        [HttpPost("CreateLoan")]
        public async Task<IActionResult> CreateLoan(LoanDTO loandto)
        {
            Loan loan = new Loan()
            {
                IdBook = loandto.IdBook,
                IdUser = loandto.IdUser,
                DateLoan = loandto.DateLoan,
                DateLoanCompletion = loandto.DateLoanCompletion,
            };

            var operation = await _loanOperation.CreateLoan(loan);

            return Ok(operation);
        }

        [HttpPut("UpdateLoan/{id}")]
        public async Task<bool> UpdateLoan(LoanDTO loanDTO)
        {
            var operation = await _loanOperation.UpdateLoan(loanDTO);

            return operation;
        }

        [HttpDelete("DeleteLoan/{id}")]
        public async Task<bool> DeleteLoan(int idLoan)
        {
            var result = await _loanOperation.DeleteLoan(idLoan);

            return result;
        }

    }
}
