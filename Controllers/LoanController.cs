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
        private BookOperation _bookOperation;

        private LoanOperation _loanOperation;
        
        private HomeworkContext _context;

        private AuxiliarTableLoanOperation _operationL { get; set; }

        public LoanController(AuxiliarTableLoanOperation operationL, BookOperation operation, LoanOperation loanOperation, HomeworkContext homeworkContext) 
        {
            _loanOperation = loanOperation;

            _context = homeworkContext;

            _bookOperation = operation;

            _operationL = operationL;
        }

        [HttpGet("GetLoans")]
        public async Task<IActionResult> GetLoans()
        {
            var ope = await _loanOperation.GetLoans();

            return Ok(ope);
        }

        [HttpGet("GetLoan")]
        public async Task<IActionResult> GetLoan(int idLoan)
        {
            var operation = await _loanOperation.GetLoan(idLoan);

            return Ok(operation);
        }

        [HttpPost("CreateLoan")]
        public async Task<IActionResult> CreateLoan([FromBody] List<LoanCustom> loanCustoms)
        {
            foreach (var item in loanCustoms)
            {
                var ope = await _bookOperation.GetBook(item.idBook);
                
                if (ope.Available == "Si" || ope.Available == "si")
                {
                    ope.Available = "no";
                }
                else 
                {
                    return BadRequest("El libro" + ope.Name + "no está disponible en este momento");
                }
            }

            Loan loan = new Loan()
            {
                DateLoan = DateTime.UtcNow,
                DateLoanCompletion = DateTime.UtcNow,
            };

            foreach (var item in loanCustoms)
            {
                AuxiliartableLoan auxiliartable = new AuxiliartableLoan()
                {
                    IdBook = item.idBook,
                    IdLoan = loan.IdLoan
                };
            }

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
