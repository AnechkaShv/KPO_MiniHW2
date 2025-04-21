using Domain.Events;

namespace Domain.Entities;

public class FeedingSchedule
{
    public int Id { get; set; }
    public int AnimalId { get; set; }
    public DateTime FeedingTime { get; set; }
    public string FoodType { get; set; }
    public bool IsCompleted { get; private set; }

    public void UpdateSchedule(DateTime newTime, string newFoodType)
    {
        FeedingTime = newTime;
        FoodType = newFoodType;
        IsCompleted = false;
    }

    public FeedingTimeEvent MarkAsCompleted()
    {
        IsCompleted = true;
        return new FeedingTimeEvent(AnimalId, FeedingTime, FoodType);
    }
}