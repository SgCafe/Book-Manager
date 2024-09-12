using Book_Manager.API.Models;
using Book_Manager.API.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Book_Manager.API.Controllers;

[ApiController]
[Route("api/books")]
public class BooksControllers : ControllerBase
{
    private readonly BookManagerDbContext _context;

    public BooksControllers(BookManagerDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult GetAll(string search = "")
    {
        var books = _context.Books.Where(b => !b.IsDeleted).ToList();

        var model = books.Select(BooksViewModel.FromEntity).ToList();
        
        return Ok(model);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var book = _context.Books.SingleOrDefault(b => b.Id == id);

        if (book is null)
        {
            return NoContent();
        }
        
        var model = BooksViewModel.FromEntity(book);
        
        return Ok(model);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateBookInputModel model)
    {
        var book = model.ToEntity();
        
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var book = _context.Books.SingleOrDefault(b => b.Id == id);

        if (book is null)
        {
            return NotFound();
        }

        book.SetAsDeleted();

        _context.Books.Update(book);
        await _context.SaveChangesAsync();

        return Ok();
    }
}