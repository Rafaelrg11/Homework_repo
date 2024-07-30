using Homework.DTOs;
using Homework.Models;
using Microsoft.EntityFrameworkCore;

namespace Homework.Operations
{
    public class LoanOperation
    {
        private HomeworkContext _context;

        public LoanOperation (HomeworkContext context)
        {
            _context = context;
        }

        public async Task<List<Loan>> GetLoans()
        {
            var loan = await _context.Loans.AsNoTracking().ToListAsync();

            return loan;
        }

        public async Task<Loan> GetLoan(int idLoan)
        {
            var result = await _context.Loans.FindAsync(idLoan);

            return result;
        }

        public async Task<Loan> CreateLoan(Loan loan)
        {
            var result = await _context.Loans.AddAsync(loan);

            await _context.SaveChangesAsync();

            return loan;
        }

        public async Task<bool> UpdateLoan(LoanDTO loanDTO)
        {
            Loan? loan = await _context.Loans.FindAsync(loanDTO.IdLoan);
            if (loan == null) 
            {
                loan.DateLoan = loanDTO.DateLoan;
                loan.DateLoanCompletion = loanDTO.DateLoanCompletion;
                loan.IdBook = loanDTO.IdBook;
                loan.IdUser = loanDTO.IdUser;

                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> DeleteLoan(int idLoan)
        {
            var result = await _context.Loans.FindAsync(idLoan);
            if (result != null)
            {
                return false;
            }

            _context.Loans.Remove(result);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
