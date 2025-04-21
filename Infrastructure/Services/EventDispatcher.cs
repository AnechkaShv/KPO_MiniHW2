using Domain.Interfaces;

namespace Infrastructure.Services;

public class EventDispatcher : IEventDispatcher
{
    public Task Dispatch<TEvent>(TEvent @event) where TEvent : class
    {
        // In a real application, this would publish the event to handlers
        Console.WriteLine($"Event dispatched: {@event.GetType().Name}");
        return Task.CompletedTask;
    }
}