using Book_Manager.API.Models;
using Book_Manager.API.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Book_Manager.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersControllers : ControllerBase
{
    private readonly BookManagerDbContext _context;

    public UsersControllers(BookManagerDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult GetAll(string search = "")
    {
        var user = _context.Users
            .Where(u => !u.IsDeleted)
            .ToList();

        var model = user.Select(UsersViewModel.FromEntity).ToList();
        
        return Ok(model);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        //Falta o Include
        var user = _context.Users
            .SingleOrDefault(u => u.Id == id);

        if (user is null)
        {
            return NotFound();
        }

        var model = UsersViewModel.FromEntity(user);
        return Ok(model);
    }

    [HttpPost]
    public IActionResult Post(CreateUserInputModel model)
    {
        var user = model.ToEntity();

        if (model == null)
        {
            return BadRequest("Erro ta aqui krl");
        }

        _context.Users.Add(user);
        _context.SaveChangesAsync();
        
        return NoContent();
    }
}