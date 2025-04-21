using Domain.Entities;
using Xunit;
using FluentAssertions;
namespace Tests.DomainTests;

public class EnclosureTests
{
    [Fact]
    public void AddAnimal_ShouldThrow_WhenAtMaxCapacity()
    {
        var enclosure = new Enclosure { MaxCapacity = 1, CurrentAnimals = 1 };
        
        Action act = () => enclosure.AddAnimal();
        
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("*max capacity*");
    }

    [Fact]
    public void Clean_ShouldLogMessage()
    {
        var enclosure = new Enclosure();
        using var log = new StringWriter();
        Console.SetOut(log);
        
        enclosure.Clean();
        
        log.ToString().Should().Contain("cleaned");
    }
}