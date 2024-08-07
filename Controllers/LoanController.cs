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
            try
            {
                await _loanOperation.GetLoan(idLoan1);

                var loan = await _context.Loans
            .Include(l => l.AuxiliarTable)
                .ThenInclude(a => a.IdBookNavigation)
                    .ThenInclude(b => b.Author)
            .Include(l => l.AuxiliarTable)
                .ThenInclude(a => a.IdUserNavigation)
            .FirstOrDefaultAsync(l => l.IdLoan == idLoan1);

                var result = await _context.AuxiliartableLoans.Where(a => a.IdLoan == idLoan1).ToListAsync();

                var LoanDb = await _context.Loans.Where(a => a.IdLoan == idLoan1).FirstOrDefaultAsync();

                List<OutputBooks> outputBooks = new List<OutputBooks>();

                List<User> users = new List<User>();

                List<Author> authors = new List<Author>();


                var LoanBooksOutPut = new OutPutLoanDetails()
                {
                    DateLoan = LoanDb.DateLoan,
                    DateLoanCompletion = LoanDb.DateLoanCompletion
                };

                var authorId = new HashSet<int>();
                foreach (var item in result)
                {
                    if (item.IdBookNavigation != null)
                    {
                        authorId.Add(item.IdBookNavigation.IdAutor);
                    }                  
                }

                if (authorId.Count > 0)
                {
                    authors = await _context.Authors
                        .Where(a => authorId.Contains(a.IdAuthor))
                        .ToListAsync();
                }

                var usersId = new HashSet<int>();
                foreach(var item in result)
                {
                    usersId.Add(item.IdUser);
                }

                if (usersId.Count > 0)
                {
                    users = await _context.Users
                        .Where(a => usersId.Contains(a.IdUser))
                        .ToListAsync();
                }

                var OutPutBooks = new List<OutputBooks>();
                foreach (var item in result)
                {
                    var book = item.IdBookNavigation;
                    if (book != null) 
                    {
                        var booksAutor = new List<OutputAuthors>();
                        foreach (var item1 in authors)
                        {
                            if (item1.IdAuthor  == book.IdAutor)
                            {
                                booksAutor.Add(new OutputAuthors
                                {
                                    NameAuthor = item1.Name,
                                    EmailAuthor = item1.Email
                                });
                            }
                        }

                        outputBooks.Add(new OutputBooks
                        {
                            idAuxiliar = item.IdAuxiliar,
                            namebook = book.Name,
                            Genrer = book.Gender,
                            Authors = booksAutor
                             
                        });
                    }
                }

                OutputUser outputUser = null;               
                    if (users.Count > 0)
                    {
                        var firstUser = users.First();
                    outputUser = new OutputUser
                    {
                        NameUser = firstUser.Name,
                        EmailUser = firstUser.Email
                    };
                    }

                var loanOutput = new OutPutLoanDetails()
                {
                    DateLoan = loan.DateLoan,
                    DateLoanCompletion = loan.DateLoanCompletion,
                    Books = outputBooks,
                    User = outputUser
                };

                return Ok(loanOutput);
            }

            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }           
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
