using Domain.Entities;
using FluentAssertions;
using Infrastructure.Repositories;
using Xunit;

namespace Tests.InfrastructureTests;

public class InMemoryZooRepositoryTests
{
    [Fact]
    public async Task AddAnimal_ShouldAutoIncrementId()
    {
        var repo = new InMemoryZooRepository();
        var animal = new Animal();
        
        await repo.AddAnimalAsync(animal);
        
        animal.Id.Should().Be(1);
    }

    [Fact]
    public async Task DeleteAnimal_ShouldRemoveFromCollection()
    {
        var repo = new InMemoryZooRepository();
        await repo.AddAnimalAsync(new Animal { Id = 1 });
        
        await repo.DeleteAnimalAsync(1);
        var result = await repo.GetAnimalAsync(1);
        
        result.Should().BeNull();
    }
}