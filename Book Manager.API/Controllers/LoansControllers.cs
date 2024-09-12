using Book_Manager.API.Models;
using Book_Manager.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Manager.API.Controllers;

[ApiController]
[Route("api/loans")]
public class LoansControllers : ControllerBase
{
    private readonly BookManagerDbContext _context;

    public LoansControllers(BookManagerDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll(string search = "")
    {
        var loans = _context.Loans
            .Include(u => u.User)
            .Include(b => b.Book)
            .Where(l => !l.IsDeleted && (search == "" || l.Book.Title.Contains(search)))
            .ToList();

        var model = loans.Select(LoanViewModel.FromEntity).ToList();

        return Ok(model);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var loan = _context.Loans.SingleOrDefault(l => l.Id == id);

        if (loan != null)
        {
            return NotFound();
        }
        
        var model = LoanViewModel.FromEntity(loan);
        
        return Ok(model);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateLoanInputModel model)
    {
        var loan = model.ToEntity();

        loan.Lend();

        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UpdateLoanInputModel model)
    {
        var loan = _context.Loans.SingleOrDefault(l => l.Id == id);

        if (loan == null)
        {
            return NotFound();
        }
        
        loan.Update(model.BookId, model.Cost);

        _context.Loans.Update(loan);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var loan = _context.Loans.SingleOrDefault(l => l.Id == id);
    
        loan.SetAsDeleted();
        _context.Loans.Update(loan);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }

    [HttpPut("{idUser}/return-book")]
    public async Task<IActionResult> LoanReturned(int idUser, int idBook)
    {
        var loanReturn = _context.Loans
            .Include(u => u.User)
            .Include(b => b.Book)
            .SingleOrDefault(x => x.UserId == idUser && x.BookId == idBook);

        loanReturn.Returned();
        _context.Loans.Update(loanReturn);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}