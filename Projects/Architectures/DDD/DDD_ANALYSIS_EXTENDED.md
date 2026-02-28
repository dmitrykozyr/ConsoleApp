### 12. Нарушение изоляции Domain слоя - RequestModels и ResponseModels в Domain

**Проблема:** В Domain слое находятся `RequestModels` и `ResponseModels`, которые по своей природе относятся к Presentation слою

**Файлы:**
- `Domain/Models/RequestModels/*` - все RequestModels находятся в Domain
- `Domain/Models/ResponseModels/*` - все ResponseModels находятся в Domain

**Почему это проблема:**
- RequestModels/ResponseModels - это представления данных для внешних слоев (API, UI)
- Domain слой должен быть независим от способа представления данных
- Это создает ненужные зависимости и нарушает принцип чистой архитектуры

**Решение:**
- Переместить все RequestModels в Presentation слой (уже сделано частично)
- Переместить ResponseModels в Presentation слой
- В Domain использовать только доменные типы и DTO (если нужны для передачи между слоями)



### 14. Нарушение изоляции Domain - зависимости от инфраструктурных библиотек

**Проблема:** Domain.csproj содержит множество инфраструктурных зависимостей:

**Зависимости:**
- `Microsoft.EntityFrameworkCore` - ORM
- `System.Data.SqlClient` - провайдер БД
- `Microsoft.AspNetCore.Http.Features` - ASP.NET Core
- `Microsoft.Extensions.Caching.StackExchangeRedis` - Redis
- `StackExchange.Redis` - Redis клиент
- `Quartz.Extensions.Hosting` - планировщик задач
- `HttpMultipartParser` - парсинг HTTP

**Почему это проблема:**
- Domain должен быть чистым от инфраструктурных деталей
- Это усложняет тестирование и делает Domain менее портируемым

**Решение:**
- Удалить все инфраструктурные зависимости из Domain
- Оставить только базовые типы и интерфейсы
- Использовать абстракции вместо конкретных реализаций


### 15. Отсутствие базового класса Entity и AggregateRoot

**Проблема:** Нет базовых классов для сущностей и агрегатов.

**Текущее состояние:**
- `Customer` и `Order` не имеют базового класса
- Нет механизма для отслеживания доменных событий
- Нет явного разделения на Entity и AggregateRoot

**Решение:**
```csharp
// Domain/Common/Entity.cs
public abstract class Entity
{
    protected Entity() { }
    protected Entity(long id) => Id = id;
    
    public long Id { get; protected set; }
    
    // Domain Events
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    
    protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
}

// Domain/Common/AggregateRoot.cs
public abstract class AggregateRoot : Entity
{
    protected AggregateRoot() { }
    protected AggregateRoot(long id) : base(id) { }
    
    // Версия для оптимистичной блокировки
    public int Version { get; private set; }
    public void IncrementVersion() => Version++;
}
```


### 16. Неправильная структура Application Services

**Проблема:** Application Services слишком простые и не следуют паттернам DDD.

**Текущее состояние:**
- `DbService` имеет только один метод `Handle`
- Нет разделения на Commands и Queries (CQRS)
- Нет использования MediatR для координации

**Решение:**
- Внедрить CQRS паттерн
- Использовать MediatR для обработки команд и запросов
- Разделить Application Services на:
  - `Commands` - для изменения состояния
  - `Queries` - для чтения данных
  - `Handlers` - обработчики команд/запросов

**Структура:**
```
Application/
  Commands/
    CreateCustomer/
      CreateCustomerCommand.cs
      CreateCustomerCommandHandler.cs
  Queries/
    GetCustomer/
      GetCustomerQuery.cs
      GetCustomerQueryHandler.cs
```


### 17. Отсутствие Domain Events

**Проблема:** В коде есть закомментированная логика для доменных событий, но сами события не реализованы.

**Почему это проблема:**
- Доменные события - ключевой механизм для координации между агрегатами
- Нет способа реагировать на изменения в доменной модели
- Сложно реализовать побочные эффекты (отправка email, логирование и т.д.)

**Решение:**
```csharp
// Domain/Events/IDomainEvent.cs
public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}

// Domain/Events/CustomerCreatedEvent.cs
public record CustomerCreatedEvent(long CustomerId, string Name) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
```


### 19. Отсутствие паттерна Specification

**Проблема:** Нет механизма для инкапсуляции сложных бизнес-правил для запросов.

**Почему это проблема:**
- Сложные запросы разбросаны по коду
- Сложно переиспользовать логику выборки
- Нарушается принцип Single Responsibility

**Решение:**
```csharp
// Domain/Specifications/ISpecification.cs
public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
}

// Domain/Specifications/Customer/ActiveCustomersSpecification.cs
public class ActiveCustomersSpecification : ISpecification<Customer>
{
    public Expression<Func<Customer, bool>> Criteria => c => !string.IsNullOrEmpty(c.Name);
    public List<Expression<Func<Customer, object>>> Includes => new();
}
```


### 20. Использование общих исключений вместо доменных

**Проблема:** В доменных сущностях используются общие `Exception` вместо доменных исключений.

**Примеры:**
- `Customer.Create()` - выбрасывает `Exception`
- `Order.Create()` - выбрасывает `Exception`

**Почему это проблема:**
- Нельзя различить типы ошибок
- Сложно обрабатывать специфичные доменные ошибки
- Нарушается экспрессивность доменной модели

**Решение:**
```csharp
// Domain/Exceptions/DomainException.cs
public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message) { }
}

// Domain/Exceptions/Customer/CustomerNameCannotBeEmptyException.cs
public class CustomerNameCannotBeEmptyException : DomainException
{
    public CustomerNameCannotBeEmptyException() 
        : base("Customer name cannot be empty") { }
}
```


### 21. Отсутствие Value Objects

**Проблема:** Все примитивные типы используются напрямую без инкапсуляции бизнес-правил.

**Примеры:**
- `Customer.Name` - просто `string?`
- `Customer.Address` - просто `string?`
- `Order.OrderTime` - просто `DateTime`

**Почему это проблема:**
- Нет валидации на уровне домена
- Бизнес-правила разбросаны по коду
- Невозможно гарантировать инварианты

**Решение:**
```csharp
// Domain/ValueObjects/CustomerName.cs
public record CustomerName
{
    private const int MaxLength = 100;
    private const int MinLength = 2;
    
    public string Value { get; }
    
    private CustomerName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new CustomerNameCannotBeEmptyException();
        
        if (value.Length < MinLength)
            throw new CustomerNameTooShortException(value.Length, MinLength);
        
        if (value.Length > MaxLength)
            throw new CustomerNameTooLongException(value.Length, MaxLength);
        
        Value = value;
    }
    
    public static CustomerName Create(string value) => new(value);
    public static implicit operator string(CustomerName name) => name.Value;
}
```


### 23. Отсутствие явных границ контекстов (Bounded Contexts)

**Проблема:** Нет явного разделения на Bounded Contexts.

**Текущее состояние:**
- Все смешано в одном контексте
- `Customer` и `Order` находятся вместе с `PersonInfo` и `AppRoleInfo`

**Почему это важно:**
- В больших системах разные контексты имеют разные модели
- Помогает управлять сложностью

**Рекомендация (для будущего развития):**
```
Domain/
  CustomerManagement/  // Bounded Context
    Entities/
    ValueObjects/
    Repositories/
  OrderManagement/     // Bounded Context
    Entities/
    ValueObjects/
  Authentication/      // Bounded Context
    Entities/
    ValueObjects/
```


### 24. Options (конфигурации) в Domain слое

**Проблема:** В Domain находятся Options для конфигурации.

**Файлы:**
- `Domain/Models/Options/*` - все опции конфигурации

**Почему это проблема:**
- Конфигурация - это инфраструктурная деталь
- Domain не должен знать о способе хранения настроек

**Решение:**
- Переместить Options в Infrastructure или Application слой
- Если нужны доменные константы - создать Domain Constants/Values




### 26. Отсутствие Result Pattern для обработки ошибок

**Проблема:** Использование исключений для бизнес-ошибок.

**Почему это проблема:**
- Исключения предназначены для исключительных ситуаций
- Бизнес-ошибки (валидация) - это нормальная часть бизнес-процесса
- Исключения дороги по производительности

**Решение:**
```csharp
// Domain/Common/Result.cs
public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; }
    
    protected Result(bool isSuccess, string error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }
    
    public static Result Success() => new(true, string.Empty);
    public static Result Failure(string error) => new(false, error);
}

public class Result<T> : Result
{
    public T Value { get; }
    
    private Result(T value, bool isSuccess, string error) 
        : base(isSuccess, error)
    {
        Value = value;
    }
    
    public static Result<T> Success(T value) => new(value, true, string.Empty);
    public static Result<T> Failure(string error) => new(default!, false, error);
}
```

---

### 27. Проблемы с валидаторами в Domain

**Проблема:** Валидаторы используют FluentValidation и находятся в Domain, но валидируют RequestModels.

**Файлы:**
- `Domain/Validators/*` - валидаторы для RequestModels

**Почему это проблема:**
- RequestModels не должны быть в Domain
- Валидация должна быть на разных уровнях:
  - Input валидация - в Presentation (RequestModels)
  - Domain валидация - в Domain (Value Objects, Entities)

**Решение:**
- Переместить валидаторы RequestModels в Presentation/Application
- Domain валидация должна быть встроена в Value Objects и Entities

---

### 28. Отсутствие явных Application Use Cases

**Проблема:** Application Services не структурированы как Use Cases.

**Текущее состояние:**
- Один сервис с одним методом `Handle`

**Решение:**
- Каждый Use Case должен быть отдельным классом
- Использовать CQRS с явными командами/запросами

---

### 29. Неправильное использование IApplicationDbContext

**Проблема:** `IApplicationDbContext` находится в Domain и используется в Repository.

**Почему это проблема:**
- DbContext - это инфраструктурная деталь (EF Core)
- Репозиторий должен скрывать детали реализации

**Решение:**
- Удалить `IApplicationDbContext` из Domain
- Репозитории должны работать только с доменными типами
- DbContext должен быть полностью в Infrastructure

---

### 30. Отсутствие явного разделения на Read и Write модели (CQRS)

**Проблема:** Одна и та же модель используется для чтения и записи.

**Почему это важно:**
- Модели чтения могут быть оптимизированы для запросов
- Модели записи могут быть оптимизированы для команд
- Разделение упрощает масштабирование

**Рекомендация:**
- Внедрить CQRS на уровне Application
- Использовать отдельные модели для Commands и Queries



## 📊 Приоритизация улучшений

1. **Удалить все инфраструктурные зависимости из Domain**
   - EF Core, SqlClient, ASP.NET Core, Redis, Quartz и др.
   - Оставить только базовые типы и интерфейсы

2. **Удалить RequestModels и ResponseModels из Domain**
   - Переместить в Presentation слой

3. **Удалить IApplicationDbContext из Domain**
   - Репозитории не должны использовать DbContext напрямую

4. **Переместить Application Service интерфейсы из Domain**
   - `IDbService` должен быть в Application

5. **Убрать зависимости от ASP.NET Core из Domain**
   - `IFormFile` заменить на абстракцию

6. **Создать базовые классы Entity и AggregateRoot**
   - С доменными событиями
   - С поддержкой версионирования

7. **Внедрить Domain Events**
   - Интерфейс IDomainEvent
   - Механизм публикации через MediatR

8. **Создать Value Objects**
   - CustomerName, Address, OrderDate и др.
   - С встроенной валидацией

9. **Создать доменные исключения**
   - DomainException базовый класс
   - Специфичные исключения для каждого случая

10. **Реализовать Result Pattern**
    - Для обработки бизнес-ошибок
    - Вместо исключений для валидации

11. **Внедрить CQRS паттерн**
    - Разделение Commands и Queries
    - Использование MediatR

12. **Асинхронные методы в репозиториях**
    - Все методы должны быть async

13. **Добавить паттерн Specification**
    - Для сложных запросов
    - Инкапсуляция бизнес-правил

14. **Переместить Options из Domain**
    - В Infrastructure или Application

15. **Рефакторинг Application Services**
    - Явные Use Cases
    - Правильная структура


## 🎯 Рекомендуемая структура проекта


Domain/
  Entities/           // Сущности домена
    Customer/
    Order/
  ValueObjects/       // Value Objects
    CustomerName/
    Address/
  Aggregates/         // Агрегаты
    Customer/
      Customer.cs (AggregateRoot)
      Order.cs (Entity)
  Common/             // Базовые классы
    Entity.cs
    AggregateRoot.cs
    Result.cs
  Events/             // Доменные события
    IDomainEvent.cs
    CustomerCreatedEvent.cs
  Exceptions/         // Доменные исключения
    DomainException.cs
    Customer/
  Interfaces/
    Repositories/     // Только интерфейсы репозиториев
    Services/         // Только доменные сервисы
  Specifications/     // Паттерн Specification


Application/
  Commands/           // CQRS Commands
    CreateCustomer/
  Queries/            // CQRS Queries
    GetCustomer/
  Interfaces/         // Application Service интерфейсы
    IDbService.cs
  DTOs/               // DTO для передачи между слоями
  Mappings/           // AutoMapper профили


Infrastructure/
  Persistence/        // Реализация репозиториев
  Services/           // Внешние сервисы
  Options/            // Конфигурации
  Events/             // Обработчики событий


Presentation/
  Controllers/        // API контроллеры
  Models/
    RequestModels/    // Request DTO
    ResponseModels/   // Response DTO
    ViewModels/       // View Models
  Validators/         // FluentValidation для RequestModels



