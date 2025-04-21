using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/statistics")]
public class StatisticsController : ControllerBase
{
    private readonly ZooStatisticsService _statisticsService;

    public StatisticsController(ZooStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var stats = await _statisticsService.GetStatistics();
        return Ok(stats);
    }
}
