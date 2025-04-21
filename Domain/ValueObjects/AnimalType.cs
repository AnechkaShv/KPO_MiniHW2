namespace Domain.ValueObjects;

public record AnimalType(string Value)
{
    public static readonly AnimalType Predator = new("Predator");
    public static readonly AnimalType Herbivore = new("Herbivore");
    public static readonly AnimalType Bird = new("Bird");
    public static readonly AnimalType Aquatic = new("Aquatic");
}