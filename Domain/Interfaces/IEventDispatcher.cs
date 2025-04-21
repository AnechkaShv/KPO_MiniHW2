namespace Domain.Interfaces;

public interface IEventDispatcher
{
    Task Dispatch<TEvent>(TEvent @event) where TEvent : class;
}