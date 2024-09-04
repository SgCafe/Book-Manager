using Book_Manager.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Book_Manager.API.Controllers;

[ApiController]
[Route("api/loans")]
public class LoansControllers : ControllerBase
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
    public IActionResult Post(CreateLoanInputModel model)
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