using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedingSchedulesController : ControllerBase
{
    private readonly IZooRepository _repository;
    private readonly FeedingOrganizationService _feedingService;

    public FeedingSchedulesController(
        IZooRepository repository, 
        FeedingOrganizationService feedingService)
    {
        _repository = repository;
        _feedingService = feedingService;
    }

    // GET: api/feedingschedules
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FeedingSchedule>>> Get()
    {
        var schedules = await _repository.GetFeedingSchedulesAsync();
        return Ok(schedules);
    }

    // POST: api/feedingschedules
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] FeedingSchedule schedule)
    {
        await _feedingService.ScheduleFeeding(
            schedule.AnimalId, 
            schedule.FeedingTime, 
            schedule.FoodType);
            
        return CreatedAtAction(nameof(Get), new { id = schedule.Id }, schedule);
    }

    // POST: api/feedingschedules/5/complete
    [HttpPost("{id}/complete")]
    public async Task<ActionResult> Complete(int id)
    {
        await _feedingService.CompleteFeeding(id);
        return NoContent();
    }
}