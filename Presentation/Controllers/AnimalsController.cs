using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IZooRepository _repository;
    private readonly AnimalTransferService _transferService;

    public AnimalsController(IZooRepository repository, AnimalTransferService transferService)
    {
        _repository = repository;
        _transferService = transferService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var animals = await _repository.GetAnimalsAsync();
        return Ok(animals);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var animal = await _repository.GetAnimalAsync(id);
        if (animal == null) return NotFound();
        return Ok(animal);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Animal animal)
    {
        await _repository.AddAnimalAsync(animal);
        return CreatedAtAction(nameof(Get), new { id = animal.Id }, animal);
    }

    [HttpPut("{id}/move")]
    public async Task<IActionResult> Move(int id, [FromBody] int newEnclosureId)
    {
        try
        {
            await _transferService.TransferAnimal(id, newEnclosureId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAnimalAsync(id);
        return NoContent();
    }
}