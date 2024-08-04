using Homework.DTOs;
using Homework.Models;
using Homework.Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("GetLoan/{idLoan1}")]
        public async Task<IActionResult> GetLoan(int idLoan1)
        {

            if (idLoan1 == null)
            {
                return BadRequest("Error 301: El Id ingresado no existe");
            }

            await _loanOperation.GetLoan(idLoan1);

            var result = await _context.AuxiliartableLoans.Where(a => a.IdLoan ==  idLoan1).ToListAsync();

            var UserDB = await _context.Users.Where(a => a.IdUser == a.IdUser).FirstOrDefaultAsync(); 

            var LoanDb = await _context.Loans.Where(a => a.IdLoan == a.IdLoan).FirstOrDefaultAsync();

            var AuthorDb = await _context.Authors.Where(a => a.IdAuthor == a.IdAuthor).FirstOrDefaultAsync();

            OutPutLoanDetails outPutLoan = new OutPutLoanDetails();

            List<OutputBooks> outputBooks = new List<OutputBooks>();

            OutputAuthors outputAuthors = new OutputAuthors()
            {
                NameAuthor = AuthorDb.Name,
                EmailAuthor = AuthorDb.Email
            };

            var LoanBooksOutPut = new OutPutLoanDetails()
            {
                DateLoan = LoanDb.DateLoan,
                DateLoanCompletion = LoanDb.DateLoanCompletion
            };

            OutputUser outputUser = new OutputUser() 
            {
                NameUser = UserDB.Name,
                EmailUser = UserDB.Email
            };

            var loandbDTO = await _context.Loans.Where(a => a.IdLoan == idLoan1).FirstOrDefaultAsync();

            foreach (var item in result)
            {
                var BooksDb = await _context.Books.Where(a => a.IdBook == item.IdBook).FirstOrDefaultAsync();
                
                var booksOutPut = new OutputBooks
                {
                    idAuxiliar = item.IdAuxiliar,
                    namebook = BooksDb.Name,
                    Genrer = BooksDb.Gender
                };
                outputBooks.Add(booksOutPut);
                booksOutPut.Authors = outputAuthors;
            }            
            LoanBooksOutPut.Books = outputBooks;
            LoanBooksOutPut.User = outputUser;            

            return Ok(LoanBooksOutPut);
        }

        [HttpPost("CreateLoan")]
        public async Task<IActionResult> CreateLoan([FromBody] List<LoanCustom> loanCustoms)
        {
            foreach (var item in loanCustoms)
            {
                var ope = await _bookOperation.GetBook(item.idBook);
                
                if (ope.Available == "Si" || ope.Available == "si")
                {
                     ope.Available = "No";
                }                
                else
                {
                    return BadRequest("El libro " + ope.Name + " no está disponible en este momento");
                }
            }

            Loan loan = new Loan()
            {
                DateLoan = DateTime.UtcNow,
                DateLoanCompletion = DateTime.UtcNow,
            };

            var operation = await _loanOperation.CreateLoan(loan);

            foreach (var item in loanCustoms)
            {
                AuxiliartableLoan auxiliartable = new AuxiliartableLoan()
                {
                    IdBook = item.idBook,
                    IdLoan = loan.IdLoan,
                    IdUser = item.idUser
                };
                var auxiliatTable = await _operationL.CreateAuxiliar(auxiliartable);
            }
            await _context.SaveChangesAsync();
            return Ok(loan.IdLoan);
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
