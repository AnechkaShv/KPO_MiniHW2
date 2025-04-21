using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace Tests.ServiceTests;

public class ZooStatisticsServiceTests
{
    [Fact]
    public async Task GetStatistics_ShouldCountFreeEnclosures()
    {
        // Arrange
        var mockRepo = new Mock<IZooRepository>();
        mockRepo.Setup(r => r.GetEnclosuresAsync())
            .ReturnsAsync(new List<Enclosure>
            {
                new() { CurrentAnimals = 0 }, // Free
                new() { CurrentAnimals = 1 }  // Occupied
            });

        var service = new ZooStatisticsService(mockRepo.Object);

        // Act
        var stats = await service.GetStatistics();

        // Assert
        stats.FreeEnclosures.Should().Be(1);
        stats.OccupiedEnclosures.Should().Be(1);
    }
}