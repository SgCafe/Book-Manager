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
    public IActionResult GetAll(string search)
    {
        var books = _context.Books.ToList();
        
        return Ok(books);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var book = _context.Books
            .SingleOrDefault(b => b.Id == id);

        if (book is null)
        {
            return NoContent();
        }
        
        var model = BooksViewModel.FromEntity(book);
        
        return Ok();
    }

    [HttpPost]
    public IActionResult Post(CreateBookInputModel model)
    {
        var book = model.ToEntity();
        
        _context.Books.Add(book);
        _context.SaveChangesAsync();
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return Ok();
    }
}