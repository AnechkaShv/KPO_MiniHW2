using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddSingleton<IZooRepository, InMemoryZooRepository>();
builder.Services.AddSingleton<IEventDispatcher, EventDispatcher>();
builder.Services.AddScoped<AnimalTransferService>();
builder.Services.AddScoped<FeedingOrganizationService>();
builder.Services.AddScoped<ZooStatisticsService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Seed initial data
var repo = app.Services.GetRequiredService<IZooRepository>();
await SeedData.Initialize(repo);

app.Run();

public static class SeedData
{
    public static async Task Initialize(IZooRepository repository)
    {
        var enclosure1 = new Enclosure { Type = "ForPredators", Size = 100, MaxCapacity = 5 };
        var enclosure2 = new Enclosure { Type = "ForHerbivores", Size = 150, MaxCapacity = 10 };
        
        await repository.AddEnclosureAsync(enclosure1);
        await repository.AddEnclosureAsync(enclosure2);

        var lion = new Animal { 
            Species = "Lion",
            Name = "Simba", 
            Birthday = new DateTime(2018, 5, 10),
            Gender = "Male",
            FavouriteFood = "Meat",
            IsHealthy = true,
            EnclosureId = enclosure1.Id
        };

        var zebra = new Animal { 
            Species = "Zebra",
            Name = "Marty", 
            Birthday = new DateTime(2019, 7, 15),
            Gender = "Male",
            FavouriteFood = "Vegetables",
            IsHealthy = true,
            EnclosureId = enclosure2.Id
        };

        await repository.AddAnimalAsync(lion);
        await repository.AddAnimalAsync(zebra);

        enclosure1.AddAnimal();
        enclosure2.AddAnimal();
        await repository.UpdateEnclosureAsync(enclosure1);
        await repository.UpdateEnclosureAsync(enclosure2);
    }
}