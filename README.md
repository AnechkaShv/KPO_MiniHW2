# Отчет по проекту "Автоматизация зоопарка"

## 1. Реализованный функционал

### Управление животными
- **Добавление/удаление**: 
  - `AnimalsController` (Presentation)
  - `InMemoryZooRepository` (Infrastructure)
  - `Animal` (Domain)
  
- **Перемещение между вольерами**:
  - `AnimalTransferService` (Application)
  - Метод `Move()` в `Animal` (Domain)

### Управление вольерами
- **Добавление/удаление**:
  - `EnclosuresController` (Presentation)
  - `Enclosure` (Domain)

### Расписание кормлений
- **Просмотр/добавление**:
  - `FeedingSchedulesController` (Presentation)
  - `FeedingOrganizationService` (Application)
  - `FeedingSchedule` (Domain)

### Статистика
- **Просмотр**:
  - `ZooStatisticsService` (Application)
  - `StatisticsController` (Presentation)

## 2. Примененные архитектурные принципы

### Domain-Driven Design (DDD)

| Концепция          | Реализация                          | Примеры классов               |
|--------------------|-------------------------------------|-------------------------------|
| Богатая модель     | Сущности с поведением               | `Animal.Move()`, `Enclosure.AddAnimal()` |
| Агрегаты           | Границы транзакций                  | `Enclosure` как корень агрегата |
| Value Objects      | Неизменяемые объекты                | `AnimalType`, `FoodType`      |
| Доменные события   | Реакция на изменения                | `AnimalMovedEvent`            |
| Репозитории        | Разделение интерфейса и реализации  | `IZooRepository` → `InMemoryZooRepository` |

### Clean Architecture

| Принцип            | Реализация                          | Пример                       |
|--------------------|-------------------------------------|-----------------------------|
| Разделение слоев   | Domain → Application → Infrastructure/Presentation | Нет зависимостей от внешних слоев |
| Dependency Rule    | Направление зависимостей            | Presentation → Application → Domain |
| DIP                | Абстракции vs детали                | `IZooRepository` в Domain, реализация в Infrastructure |


## 3. Запуск программы
Склонировать репозиторий
 ```bash
   git clone https://github.com/AnechkaShv/KPO_MiniHW2
```

Перейти в папку Presentation
```bash
   cd Presentation
```

Запустить API
```bash
   dotnet run
```
Swagger
http://localhost:5254/swagger/index.html

Запуск тестов
```bash
dotnet test --collect:"XPlat Code Coverage"
```
