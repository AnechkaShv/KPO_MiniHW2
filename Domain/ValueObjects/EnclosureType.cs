namespace Domain.ValueObjects;

public record EnclosureType(string Value)
{
    public static readonly EnclosureType ForPredators = new("ForPredators");
    public static readonly EnclosureType ForHerbivores = new("ForHerbivores");
    public static readonly EnclosureType Aviary = new("Aviary");
    public static readonly EnclosureType Aquarium = new("Aquarium");
}