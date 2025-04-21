using Domain.Interfaces;
using System.Linq;

namespace Application.Services;

public class ZooStatisticsService
{
    private readonly IZooRepository _repository;

    public ZooStatisticsService(IZooRepository repository)
    {
        _repository = repository;
    }

    public async Task<ZooStatistics> GetStatistics()
    {
        var animals = (await _repository.GetAnimalsAsync()).ToList();
        var enclosures = (await _repository.GetEnclosuresAsync()).ToList();

        return new ZooStatistics
        {
            TotalAnimals = animals.Count,
            TotalEnclosures = enclosures.Count,
            FreeEnclosures = enclosures.Count(e => e.CurrentAnimals == 0),
            OccupiedEnclosures = enclosures.Count(e => e.CurrentAnimals > 0),
            FullEnclosures = enclosures.Count(e => e.CurrentAnimals >= e.MaxCapacity),
            HealthyAnimals = animals.Count(a => a.IsHealthy),
            SickAnimals = animals.Count(a => !a.IsHealthy)
        };
    }
}

public class ZooStatistics
{
    public int TotalAnimals { get; set; }
    public int TotalEnclosures { get; set; }
    public int FreeEnclosures { get; set; }
    public int OccupiedEnclosures { get; set; }
    public int FullEnclosures { get; set; }
    public int HealthyAnimals { get; set; }
    public int SickAnimals { get; set; }
}