using Domain.Entities;

namespace Domain.Interfaces;

public interface IZooRepository
{
    Task<Animal> GetAnimalAsync(int id);
    Task<IEnumerable<Animal>> GetAnimalsAsync();
    Task AddAnimalAsync(Animal animal);
    Task UpdateAnimalAsync(Animal animal);
    Task DeleteAnimalAsync(int id);

    Task<Enclosure> GetEnclosureAsync(int id);
    Task<IEnumerable<Enclosure>> GetEnclosuresAsync();
    Task AddEnclosureAsync(Enclosure enclosure);
    Task UpdateEnclosureAsync(Enclosure enclosure);
    Task DeleteEnclosureAsync(int id);

    Task<FeedingSchedule> GetFeedingScheduleAsync(int id);
    Task<IEnumerable<FeedingSchedule>> GetFeedingSchedulesAsync();
    Task AddFeedingScheduleAsync(FeedingSchedule schedule);
    Task UpdateFeedingScheduleAsync(FeedingSchedule schedule);
    Task DeleteFeedingScheduleAsync(int id);
}