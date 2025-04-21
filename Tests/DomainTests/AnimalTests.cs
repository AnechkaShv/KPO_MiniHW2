using Domain.Entities;
using Domain.Events;
using Xunit;
using FluentAssertions;

namespace Tests.DomainTests;

public class AnimalTests
{
    [Fact]
    public void Move_ShouldChangeEnclosureId_AndReturnEvent()
    {
        // Arrange
        var animal = new Animal { EnclosureId = 1 };
        
        // Act
        var domainEvent = animal.Move(2);
        
        // Assert
        animal.EnclosureId.Should().Be(2);
        domainEvent.Should().BeOfType<AnimalMovedEvent>();
        domainEvent.NewEnclosureId.Should().Be(2);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Treat_ShouldSetHealthStatus(bool initialStatus)
    {
        var animal = new Animal { IsHealthy = initialStatus };
        
        animal.Treat();
        
        animal.IsHealthy.Should().BeTrue();
    }
}