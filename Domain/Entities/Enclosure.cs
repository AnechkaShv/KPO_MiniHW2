namespace Domain.Entities;

public class Enclosure
{
    public int Id { get; set; }
    public string Type { get; set; }
    public double Size { get; set; }
    public int CurrentAnimals { get; set; }
    public int MaxCapacity { get; set; }

    public bool CanAddAnimal()
    {
        return CurrentAnimals < MaxCapacity;
    }

    public void AddAnimal()
    {
        if (CanAddAnimal())
            CurrentAnimals++;
        else
            throw new InvalidOperationException("Enclosure is at max capacity");
    }

    public void RemoveAnimal()
    {
        if (CurrentAnimals > 0)
            CurrentAnimals--;
        else
            throw new InvalidOperationException("No animals in enclosure");
    }

    public void Clean()
    {
        Console.WriteLine($"Enclosure {Id} has been cleaned");
    }
}