using Book_Manager.Application.Models;
using BookManager.Application.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace Book_Manager.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersControllers : ControllerBase
{
    private readonly IUsersServices _services;

    public UsersControllers(IUsersServices services)
    {
        _services = services;
    }

    [HttpGet]
    public IActionResult GetAll(string search = "")
    {
        var result = _services.GetAll();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var result = _services.GetById(id);

        return Ok(result);
    }

    [HttpPost]
    public IActionResult Post(CreateUserInputModel model)
    {
        var result = _services.Post(model);

        return NoContent();
    }
}