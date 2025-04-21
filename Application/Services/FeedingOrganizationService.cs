using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class FeedingOrganizationService
{
    private readonly IZooRepository _repository;
    private readonly IEventDispatcher _eventDispatcher;

    public FeedingOrganizationService(IZooRepository repository, IEventDispatcher eventDispatcher)
    {
        _repository = repository;
        _eventDispatcher = eventDispatcher;
    }

    public async Task ScheduleFeeding(int animalId, DateTime time, string foodType)
    {
        var schedule = new FeedingSchedule
        {
            AnimalId = animalId,
            FeedingTime = time,
            FoodType = foodType
        };

        await _repository.AddFeedingScheduleAsync(schedule);
    }

    public async Task CompleteFeeding(int scheduleId)
    {
        var schedule = await _repository.GetFeedingScheduleAsync(scheduleId);
        var feedingEvent = schedule.MarkAsCompleted();
            
        await _repository.UpdateFeedingScheduleAsync(schedule);
        await _eventDispatcher.Dispatch(feedingEvent);
    }
}