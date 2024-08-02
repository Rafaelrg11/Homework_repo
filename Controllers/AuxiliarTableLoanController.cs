using Homework.DTOs;
using Homework.Models;
using Homework.Operations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuxiliarTableLoanController : ControllerBase
    {
        private AuxiliarTableLoanOperation _operation;

        private LoanOperation _LoanOpe;

        private BookOperation _bookope;

        private HomeworkContext _context;

        public AuxiliarTableLoanController(HomeworkContext context,BookOperation bookOperation,LoanOperation loan,AuxiliarTableLoanOperation operation)
        {
            _operation = operation;

            _bookope = bookOperation;

            _LoanOpe = loan;

            _context = context;
        }

        [HttpGet("GetLoansDetails")]
        public async Task<AuxiliartableLoan> GetLoanDetails(int idDetails)
        {

        }

       /* [HttpPost("CreateLoan")]
        public async Task<IActionResult> CreateLoan([FromBody] List<LoanCustom> loanCustoms)
        {

            foreach (var item in loanCustoms)
            {
                var ope = await _bookope.GetBook(item.idBook);

                if (ope.Available == "Si" || ope.Available == "si")
                {
                    ope.Available = "No" ;
                }
                else
                {
                    return BadRequest(("El libro" + ope.Name + "no está disponible en este momento"));
                }
            }

            Loan loan = new Loan()
            {
                DateLoan = DateTime.Now,
                DateLoanCompletion = DateTime.Now,
            };

            var createdLoan = await _LoanOpe.CreateLoan(loan);
            

            foreach (var item in loanCustoms)
            {
                AuxiliartableLoan auxiliartable = new AuxiliartableLoan()
                {
                    IdBook = item.idBook,
                    IdLoan = loan.IdLoan
                };

                var createAuxiliar = await _operation.CreateAuxiliar(auxiliartable);
            }
            
            await _context.SaveChangesAsync();
            
            return Ok(loan.IdLoan);
        }*/
    }
}
