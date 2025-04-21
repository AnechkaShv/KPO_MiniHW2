namespace Domain.ValueObjects;

public record FoodType(string Value)
{
    public static readonly FoodType Meat = new("Meat");
    public static readonly FoodType Vegetables = new("Vegetables");
    public static readonly FoodType Fruits = new("Fruits");
    public static readonly FoodType Fish = new("Fish");
}