using Domain.Interfaces;

namespace Application.Services;

public class AnimalTransferService
{
    private readonly IZooRepository _repository;
    private readonly IEventDispatcher _eventDispatcher;

    public AnimalTransferService(IZooRepository repository, IEventDispatcher eventDispatcher)
    {
        _repository = repository;
        _eventDispatcher = eventDispatcher;
    }

    public async Task TransferAnimal(int animalId, int newEnclosureId)
    {
        // 1. Получаем животное (с проверкой на null)
        var animal = await _repository.GetAnimalAsync(animalId) 
                     ?? throw new ArgumentException($"Animal with id {animalId} not found");

        // 2. Получаем исходный вольер (с проверкой)
        var oldEnclosure = await _repository.GetEnclosureAsync(animal.EnclosureId) 
                           ?? throw new ArgumentException($"Enclosure {animal.EnclosureId} not found");

        // 3. Получаем целевой вольер (с проверкой)
        var newEnclosure = await _repository.GetEnclosureAsync(newEnclosureId) 
                           ?? throw new ArgumentException($"Enclosure {newEnclosureId} not found");

        // 4. Проверяем условия
        if (oldEnclosure.CurrentAnimals <= 0)
            throw new InvalidOperationException("Source enclosure is empty");

        if (!newEnclosure.CanAddAnimal())
            throw new InvalidOperationException("Target enclosure is full");

        // 5. Выполняем перемещение
        var moveEvent = animal.Move(newEnclosureId);
        oldEnclosure.RemoveAnimal();
        newEnclosure.AddAnimal();

        await _repository.UpdateAnimalAsync(animal);
        await _repository.UpdateEnclosureAsync(oldEnclosure);
        await _repository.UpdateEnclosureAsync(newEnclosure);
        await _eventDispatcher.Dispatch(moveEvent);
    }
}