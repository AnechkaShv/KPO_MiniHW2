using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;
using Xunit;

namespace Tests.PresentationTests;

public class AnimalsControllerTests
{
    [Fact]
    public async Task Create_ShouldReturn201_WithLocationHeader()
    {
        // Arrange
        var mockRepo = new Mock<IZooRepository>();
        var controller = new AnimalsController(mockRepo.Object, null);
        var animal = new Animal { Id = 1 };

        // Act
        var result = await controller.Create(animal);

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>()
            .Which.RouteValues["id"].Should().Be(1);
    }

    [Fact]
    public async Task Move_ShouldReturn400_OnError()
    {
        // Arrange
        var mockRepo = new Mock<IZooRepository>();
        var mockDispatcher = new Mock<IEventDispatcher>();
    
        // Создаем реальный сервис с моком репозитория
        var transferService = new AnimalTransferService(mockRepo.Object, mockDispatcher.Object);
    
        // Настраиваем репозиторий для выброса ошибки
        mockRepo.Setup(r => r.GetAnimalAsync(1))
            .ThrowsAsync(new InvalidOperationException("Test error"));
    
        var controller = new AnimalsController(mockRepo.Object, transferService);

        // Act
        var result = await controller.Move(1, 2);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>()
            .Which.Value.Should().Be("Test error");
    }
}