using Domain.Events;

namespace Domain.Entities;

public class Animal
{
    public int Id { get;  set; }
    public String Species { get;  set; }
    public string Name { get; set; }
    public DateTime Birthday { get; set; }
    public string Gender { get; set; }
    public string FavouriteFood { get; set; }
    public bool IsHealthy { get;  set; }
    public int EnclosureId { get;  set; }

    public void Feed(string foodType)
    {
        if (foodType == FavouriteFood)
        {
            Console.WriteLine($"{Name} happily eats {foodType}");
        }
        else
        {
            Console.WriteLine($"{Name} reluctantly eats {foodType}");
        }
    }

    public void Treat()
    {
        IsHealthy = true;
        Console.WriteLine($"{Name} has been treated and now is healthy");
    }

    public AnimalMovedEvent Move(int newEnclosureId)
    {
        var oldEnclosureId = EnclosureId;
        EnclosureId = newEnclosureId;
        return new AnimalMovedEvent(newEnclosureId, oldEnclosureId, newEnclosureId);
    }
}