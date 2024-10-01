using Book_Manager.Application.Models;
using Book_Manager.Infrastructure.Persistence;
using BookManager.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManager.Application.Services.Loans
{
    public class LoansServices : ILoansServices
    {
        private readonly BookManagerDbContext _context;

        public LoansServices(BookManagerDbContext context)
        {
            _context = context;
        }

        public ResultViewModel<List<LoanViewModel>> GetAll(string search = "")
        {
            var loans = _context.Loans
            .Include(u => u.User)
            .Include(b => b.Book)
            .Where(l => !l.IsDeleted && (search == "" || l.Book.Title.Contains(search)))
            .ToList();

            var model = loans.Select(LoanViewModel.FromEntity).ToList();

            return ResultViewModel<List<LoanViewModel>>.Success(model);
        }

        public ResultViewModel<LoanViewModel> GetById(int id)
        {
            var loan = _context.Loans
                .Include(b => b.Book)
                .Include(u => u.User)
                .SingleOrDefault(l => l.Id == id);

            if (loan == null)
            {
                return ResultViewModel<LoanViewModel>.Erro("Empréstimo não encontrado.");
            }

            var model = LoanViewModel.FromEntity(loan);

            return ResultViewModel<LoanViewModel>.Success(model);
        }

        public ResultViewModel<int> Post(CreateLoanInputModel model)
        {
            var loan = model.ToEntity();

            loan.Lend();

            _context.Loans.Add(loan);
            _context.SaveChanges();

            return ResultViewModel<int>.Success(loan.Id);
        }

        public ResultViewModel Put(int id, UpdateLoanInputModel model)
        {
            var loan = _context.Loans
                .Include(b => b.Book)
                .Include(u => u.User)
                .SingleOrDefault(l => l.Id == id);

            if (loan == null)
            {
                return ResultViewModel.Erro("Empréstimo não encontrado.");
            }

            loan.Update(model.BookId, model.UserId ,model.Cost);

            _context.Loans.Update(loan);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel Delete(int id)
        {
            var loan = _context.Loans.SingleOrDefault(l => l.Id == id);

            if (loan == null)
            {
                return ResultViewModel.Erro("Empréstimo não encontrado.");
            }

            loan.SetAsDeleted();
            _context.Loans.Update(loan);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel LoanReturned(int idUser, int idBook)
        {
            var loanReturn = _context.Loans
            .Include(u => u.User)
            .Include(b => b.Book)
            .SingleOrDefault(x => x.UserId == idUser && x.BookId == idBook);

            if (loanReturn == null)
            {
                throw new ArgumentException("Empréstimo não encontrado para o usuário ou livro informados");
            }

            if (loanReturn.ReturnDate < DateTime.Now)
            {
                throw new InvalidOperationException("O livro está atrasado.");
            }

            loanReturn.Returned();
            _context.Loans.Update(loanReturn);
            _context.SaveChanges();

            //return Ok("O livro está em dia");
            return ResultViewModel.Success();
        }
    }
}
