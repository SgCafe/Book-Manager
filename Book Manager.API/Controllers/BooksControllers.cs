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
        var books = _context.Books.Select();
        
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Post(CreateBookInputModel model)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return Ok();
    }
}