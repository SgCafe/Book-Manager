using Microsoft.AspNetCore.Mvc;

namespace Book_Manager.API.Controllers;

[ApiController]
[Route("api/loans")]
public class LoansConstrollers : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll(string search = "")
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Post()
    {
        return NoContent();
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return NoContent();
    }
}