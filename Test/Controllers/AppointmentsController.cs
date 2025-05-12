using Microsoft.AspNetCore.Mvc;
using Template.Exceptions;
using Template.Models;
using Template.Services;

namespace Template.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    private readonly IDbService _dbService;
    public AppointmentsController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{id}/smth")]
    public async Task<IActionResult> GetSmth(int id)
    {
        var smth = await _dbService.GetSmth(id);
        return Ok(smth);
    }

    [HttpPost("{id}/smth")]
    public async Task<IActionResult> AddNewSmth(int id, RequestDTO requestDTO)
    {
        try
        {
            await _dbService.AddNewSmth(id, requestDTO);
        }
        catch (CustomEx1 e)
        {
            return Conflict(e.Message);
        }
        catch (CustomEx2 e)
        {
            return NotFound(e.Message);
        }
        
        return CreatedAtAction(nameof(GetSmth), new { id }, requestDTO);
    }
}