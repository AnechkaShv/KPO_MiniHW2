namespace Domain.Events;

public class AnimalMovedEvent
{
    public int AnimalId { get; }
    public int OldEnclosureId { get; }
    public int NewEnclosureId { get; }
    public DateTime MovedAt { get; }

    public AnimalMovedEvent(int animalId, int oldEnclosureId, int newEnclosureId)
    {
        AnimalId = animalId;
        OldEnclosureId = oldEnclosureId;
        NewEnclosureId = newEnclosureId;
        MovedAt = DateTime.UtcNow;
    }
}