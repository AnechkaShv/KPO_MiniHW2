using Application.Services;
using Domain.Entities;
using Domain.Events;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace Tests.ServiceTests;

public class AnimalTransferServiceTests
{
    [Fact]
    public async Task TransferAnimal_ShouldFail_WhenEnclosureFull()
    {
        // Arrange
        var mockRepo = new Mock<IZooRepository>();
    
        // Настраиваем корректные mock-объекты
        var animal = new Animal { Id = 1, EnclosureId = 1 };
        var oldEnclosure = new Enclosure { Id = 1, CurrentAnimals = 1 };
        var fullEnclosure = new Enclosure { Id = 2, MaxCapacity = 1, CurrentAnimals = 1 };

        mockRepo.Setup(r => r.GetAnimalAsync(1)).ReturnsAsync(animal);
        mockRepo.Setup(r => r.GetEnclosureAsync(1)).ReturnsAsync(oldEnclosure);
        mockRepo.Setup(r => r.GetEnclosureAsync(2)).ReturnsAsync(fullEnclosure);
    
        var service = new AnimalTransferService(mockRepo.Object, Mock.Of<IEventDispatcher>());

        // Act & Assert
        await service.Invoking(s => s.TransferAnimal(1, 2))
            .Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Target enclosure is full");
    }
    
    [Fact]
    public async Task TransferAnimal_ShouldDispatchEvent_OnSuccess()
    {
        // Arrange
        var mockRepo = new Mock<IZooRepository>();
        var mockDispatcher = new Mock<IEventDispatcher>();

        // 1. Настраиваем животное в исходном вольере (ID=1)
        var animal = new Animal { Id = 1, EnclosureId = 1 };
    
        // 2. Настраиваем исходный вольер (с 1 животным)
        var sourceEnclosure = new Enclosure 
        { 
            Id = 1, 
            CurrentAnimals = 1, // Важно: вольер не пустой!
            MaxCapacity = 10 
        };
    
        // 3. Настраиваем целевой вольер
        var targetEnclosure = new Enclosure 
        { 
            Id = 2, 
            CurrentAnimals = 0, 
            MaxCapacity = 10 
        };

        // 4. Настраиваем моки репозитория
        mockRepo.Setup(r => r.GetAnimalAsync(1)).ReturnsAsync(animal);
        mockRepo.Setup(r => r.GetEnclosureAsync(1)).ReturnsAsync(sourceEnclosure);
        mockRepo.Setup(r => r.GetEnclosureAsync(2)).ReturnsAsync(targetEnclosure);
    
        var service = new AnimalTransferService(mockRepo.Object, mockDispatcher.Object);

        // Act
        await service.TransferAnimal(animal.Id, targetEnclosure.Id);

        // Assert
        mockDispatcher.Verify(
            d => d.Dispatch(It.IsAny<AnimalMovedEvent>()),
            Times.Once);
    
        // Дополнительные проверки состояния
        sourceEnclosure.CurrentAnimals.Should().Be(0);
        targetEnclosure.CurrentAnimals.Should().Be(1);
        animal.EnclosureId.Should().Be(2);
    }
}