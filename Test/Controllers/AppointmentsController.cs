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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppointments(int id)
    {
        try
        {
            var smth = await _dbService.GetAppointmentsAsync(id);
            return Ok(smth);
        }
        catch (NotFoundEx e)
        {
            return NotFound(e.Message);
        }

        
    }

    [HttpPost("{id}/smth")]
    public async Task<IActionResult> AddNewSmth(int id, RequestDTO requestDTO)
    {
        try
        {
            await _dbService.AddNewSmth(id, requestDTO);
        }
        catch (NotFoundEx e)
        {
            return Conflict(e.Message);
        }
        catch (CustomEx2 e)
        {
            return NotFound(e.Message);
        }
        
        return CreatedAtAction(nameof(GetAppointments), new { id }, requestDTO);
    }
}