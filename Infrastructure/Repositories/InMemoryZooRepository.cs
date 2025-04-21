using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class InMemoryZooRepository : IZooRepository
{
    private readonly List<Animal> _animals = new();
    private readonly List<Enclosure> _enclosures = new();
    private readonly List<FeedingSchedule> _feedingSchedules = new();
    private int _nextAnimalId = 1;
    private int _nextEnclosureId = 1;
    private int _nextScheduleId = 1;

    #region Animal Methods

    public Task<Animal> GetAnimalAsync(int id)
    {
        return Task.FromResult(_animals.FirstOrDefault(a => a.Id == id));
    }

    public Task<IEnumerable<Animal>> GetAnimalsAsync()
    {
        return Task.FromResult(_animals.AsEnumerable());
    }

    public Task AddAnimalAsync(Animal animal)
    {
        animal.Id = _nextAnimalId++;
        _animals.Add(animal);
        return Task.CompletedTask;
    }

    public Task UpdateAnimalAsync(Animal animal)
    {
        var index = _animals.FindIndex(a => a.Id == animal.Id);
        if (index >= 0)
            _animals[index] = animal;
        return Task.CompletedTask;
    }

    public Task DeleteAnimalAsync(int id)
    {
        _animals.RemoveAll(a => a.Id == id);
        return Task.CompletedTask;
    }

    #endregion

    #region Enclosure Methods

    public Task<Enclosure> GetEnclosureAsync(int id)
    {
        return Task.FromResult(_enclosures.FirstOrDefault(e => e.Id == id));
    }

    public Task<IEnumerable<Enclosure>> GetEnclosuresAsync()
    {
        return Task.FromResult(_enclosures.AsEnumerable());
    }

    public Task AddEnclosureAsync(Enclosure enclosure)
    {
        enclosure.Id = _nextEnclosureId++;
        _enclosures.Add(enclosure);
        return Task.CompletedTask;
    }

    public Task UpdateEnclosureAsync(Enclosure enclosure)
    {
        var index = _enclosures.FindIndex(e => e.Id == enclosure.Id);
        if (index >= 0)
            _enclosures[index] = enclosure;
        return Task.CompletedTask;
    }

    public Task DeleteEnclosureAsync(int id)
    {
        _enclosures.RemoveAll(e => e.Id == id);
        return Task.CompletedTask;
    }

    #endregion

    #region FeedingSchedule Methods

    public Task<FeedingSchedule> GetFeedingScheduleAsync(int id)
    {
        return Task.FromResult(_feedingSchedules.FirstOrDefault(f => f.Id == id));
    }

    public Task<IEnumerable<FeedingSchedule>> GetFeedingSchedulesAsync()
    {
        return Task.FromResult(_feedingSchedules.AsEnumerable());
    }

    public Task AddFeedingScheduleAsync(FeedingSchedule schedule)
    {
        schedule.Id = _nextScheduleId++;
        _feedingSchedules.Add(schedule);
        return Task.CompletedTask;
    }

    public Task UpdateFeedingScheduleAsync(FeedingSchedule schedule)
    {
        var index = _feedingSchedules.FindIndex(f => f.Id == schedule.Id);
        if (index >= 0)
            _feedingSchedules[index] = schedule;
        return Task.CompletedTask;
    }

    public Task DeleteFeedingScheduleAsync(int id)
    {
        _feedingSchedules.RemoveAll(f => f.Id == id);
        return Task.CompletedTask;
    }

    #endregion

    #region Helper Methods (Optional)

    public Task<int> SaveChangesAsync()
    {
        // For in-memory repo, changes are immediate
        return Task.FromResult(0);
    }

    #endregion
}