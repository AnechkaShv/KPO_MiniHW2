namespace Domain.Events;

public class FeedingTimeEvent
{
    public int AnimalId { get; }
    public DateTime FeedingTime { get; }
    public string FoodType { get; }
    public DateTime CompletedAt { get; }

    public FeedingTimeEvent(int animalId, DateTime feedingTime, string foodType)
    {
        AnimalId = animalId;
        FeedingTime = feedingTime;
        FoodType = foodType;
        CompletedAt = DateTime.UtcNow;
    }
}