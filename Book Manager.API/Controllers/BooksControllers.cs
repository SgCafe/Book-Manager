using Book_Manager.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Book_Manager.API.Controllers;

    [Route("api/books")]
    [ApiController]
public class BooksControllers : ControllerBase
{

    [HttpGet]
    public IActionResult GetAll(string search)
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Post(CreateBookInputModel model, int id)
    {
        return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return Ok();
    }
}