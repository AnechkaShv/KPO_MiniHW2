using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnclosuresController : ControllerBase
{
    private readonly IZooRepository _repository;

    public EnclosuresController(IZooRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var enclosures = await _repository.GetEnclosuresAsync();
        return Ok(enclosures);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var enclosure = await _repository.GetEnclosureAsync(id);
        if (enclosure == null) return NotFound();
        return Ok(enclosure);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Enclosure enclosure)
    {
        await _repository.AddEnclosureAsync(enclosure);
        return CreatedAtAction(nameof(Get), new { id = enclosure.Id }, enclosure);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteEnclosureAsync(id);
        return NoContent();
    }
}