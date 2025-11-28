# Обновленный анализ соответствия архитектуре DDD

**Дата анализа:** После перемещения сервисов из Domain в Application

---

## ✅ Улучшения (что было исправлено)

### 1. Сервисы перемещены из Domain в Application
**Статус:** ✅ **Исправлено**

- `SqlService` → `Application/Services/`
- `FilesService` → `Application/Services/API/`
- `DbService` → `Application/Services/API/`
- `LoginService` → `Application/Services/Login/`
- `MemoryCacheService` → `Application/Services/Cache/`
- `RedisService` → `Application/Services/Cache/`
- `DbConStrService` → `Application/Services/`
- `Provider` → `Application/Services/Login/`
- `LoggingBackgroundJob` → `Application/Services/Quartz_/`

**Результат:** Domain слой больше не содержит реализаций сервисов.

### 2. Application слой больше не пустой
**Статус:** ✅ **Исправлено**

Application слой теперь содержит:
- Application Services (DbService, FilesService, LoginService)
- Infrastructure Services (SqlService, Cache Services)
- Правильная структура папок

### 3. Presentation использует Application.Services
**Статус:** ✅ **Исправлено**

`ServicesExtensions.cs` теперь использует `Application.Services` вместо `Domain.Services`:
```csharp
using Application.Services;
using Application.Services.API;
using Application.Services.Cache;
using Application.Services.Login;
```

### 4. Добавлена ссылка на Infrastructure в Presentation
**Статус:** ✅ **Исправлено** (но это нарушение DDD, см. ниже)

---

## 🔴 Критические нарушения (требуют исправления)

### 1. Domain содержит инфраструктурные зависимости

#### 1.1. UserContextCommand в Domain
**Проблема:** `Domain/Domain Services/Login/UserContextCommand.cs` использует `System.Data.SqlClient`

```csharp
using System.Data.SqlClient;  // ❌ Инфраструктурная деталь

public class UserContextCommand : IDisposable
{
    private readonly SqlCommand command;  // ❌ Конкретная реализация
    private readonly SqlConnection? connection;  // ❌ Конкретная реализация
}
```

**Решение:** Переместить в `Application/Services/Login/` или `Infrastructure/`

#### 1.2. IApplicationDbContext содержит EF Core типы
**Проблема:** `Domain/Interfaces/Db/DbContext/IApplicationDbContext.cs` использует EF Core

```csharp
using Microsoft.EntityFrameworkCore;  // ❌ НЕ ДОЛЖНО БЫТЬ В DOMAIN
using Microsoft.EntityFrameworkCore.Infrastructure;  // ❌ НЕ ДОЛЖНО БЫТЬ В DOMAIN

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; set; }  // ❌ DbSet - это EF Core
    DatabaseFacade Database { get; }  // ❌ DatabaseFacade - это EF Core
}
```

**Решение:** Переписать интерфейс без использования EF Core типов:
```csharp
public interface IApplicationDbContext
{
    // Абстрактные методы без упоминания EF Core
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
```

#### 1.3. IFilesService содержит ASP.NET Core типы
**Проблема:** `Domain/Interfaces/Services/IFilesService.cs` использует `IFormFile`

```csharp
using Microsoft.AspNetCore.Http;  // ❌ НЕ ДОЛЖНО БЫТЬ В DOMAIN

public interface IFilesService
{
    Task<Guid> LoadFileFromFileSystemBySelection(
        LoadFileBySelectionRequest model, 
        IFormFile file);  // ❌ IFormFile - это ASP.NET Core
}
```

**Решение:** Заменить `IFormFile` на абстрактный тип или DTO:
```csharp
public interface IFilesService
{
    Task<Guid> LoadFileFromFileSystemBySelection(
        LoadFileBySelectionRequest model, 
        Stream fileStream,  // ✅ Абстрактный тип
        string fileName);
}
```

---

### 2. Неправильная структура зависимостей

#### 2.1. Infrastructure зависит от Application
**Проблема:** `Infrastructure.csproj` ссылается на `Application.csproj`

```xml
<ItemGroup>
  <ProjectReference Include="..\Application\Application.csproj" />  <!-- ❌ -->
  <ProjectReference Include="..\Domain\Domain.csproj" />
</ItemGroup>
```

**Правильно для DDD:**
```
Infrastructure → Domain (для реализации интерфейсов)
Infrastructure → Application (только если Application определяет интерфейсы для Infrastructure)
```

**Проблема:** Infrastructure не должен зависеть от Application. Если Infrastructure реализует интерфейсы из Application, это нарушение - интерфейсы должны быть в Domain.

**Решение:** Убрать зависимость Infrastructure от Application, если она не нужна. Проверить, что Infrastructure реализует только интерфейсы из Domain.

#### 2.2. Presentation зависит напрямую от Infrastructure
**Проблема:** `Presentation.csproj` ссылается на `Infrastructure.csproj`

```xml
<ItemGroup>
  <ProjectReference Include="..\Application\Application.csproj" />
  <ProjectReference Include="..\Infrastructure\Infrastructure.csproj" />  <!-- ❌ -->
</ItemGroup>
```

**Правильно для DDD:**
```
Presentation → Application (только)
Application → Domain
Infrastructure → Domain
```

**Проблема:** Presentation использует Infrastructure напрямую в расширениях:
- `DbContextExtensions.cs` → `Infrastructure.Repositories`
- `RepositoriesExtensions.cs` → `Infrastructure.Repositories.*`
- `ServicesExtensions.cs` → `Infrastructure.HttpClient_`, `Infrastructure.LoggingData`
- `VaultExtensions.cs` → `Infrastructure.Vault`

**Решение:** 
1. Переместить регистрацию сервисов из Presentation в Infrastructure
2. Создать extension методы в Infrastructure для регистрации всех сервисов
3. Presentation должен вызывать только один метод из Infrastructure: `services.AddInfrastructure()`
4. Убрать прямую зависимость Presentation от Infrastructure

---

### 3. Сущности Domain не соответствуют принципам DDD

**Проблема:** Сущности `Customer` и `Order` являются простыми POCO классами без бизнес-логики.

```csharp
public class Customer
{
    public long Id { get; init; }  // ⚠️ init setter - лучше, но все еще не идеально
    public string? Name { get; init; }  // ⚠️ init setter
    public string? Address { get; init; }  // ⚠️ init setter
}
```

**Проблемы:**
- Нет инкапсуляции бизнес-логики
- Нет валидации на уровне домена
- Нет методов для работы с сущностью
- Можно создать невалидную сущность

**Правильно:**
```csharp
public class Customer
{
    public long Id { get; private set; }  // ✅ private setter
    public string Name { get; private set; }  // ✅ private setter
    public string Address { get; private set; }  // ✅ private setter
    
    // Фабричный метод или конструктор с валидацией
    public static Customer Create(string name, string address)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name cannot be empty");
        if (string.IsNullOrWhiteSpace(address))
            throw new DomainException("Address cannot be empty");
            
        return new Customer
        {
            Id = 0,  // Будет установлен при сохранении
            Name = name,
            Address = address
        };
    }
    
    // Бизнес-логика
    public void ChangeAddress(string newAddress)
    {
        if (string.IsNullOrWhiteSpace(newAddress))
            throw new DomainException("Address cannot be empty");
        Address = newAddress;
    }
    
    private Customer() { }  // Для EF Core
}
```

---

## ⚠️ Проблемы, требующие внимания

### 1. Application содержит Infrastructure Services

**Проблема:** В Application слое находятся сервисы, которые должны быть в Infrastructure:
- `SqlService` - работа с SQL (инфраструктура)
- `MemoryCacheService`, `RedisService` - кеширование (инфраструктура)
- `DbConStrService` - работа с конфигурацией (инфраструктура)

**Правильно:** 
- Application должен содержать только Application Services (оркестрация доменной логики)
- Infrastructure Services должны быть в Infrastructure

**Решение:** Переместить инфраструктурные сервисы в Infrastructure:
- `SqlService` → `Infrastructure/Services/`
- `MemoryCacheService`, `RedisService` → `Infrastructure/Services/Cache/`
- `DbConStrService` → `Infrastructure/Services/`

### 2. Application содержит зависимости от инфраструктуры

**Проблема:** Application Services используют инфраструктурные типы:
- `FilesService` использует `HttpWebRequest`, `IFormFile`
- `LoginService` использует `System.Data.SqlClient`
- `SqlService` использует `System.Data.SqlClient`

**Решение:** 
- Вынести работу с HTTP в Infrastructure
- Вынести работу с БД в Infrastructure
- Application должен использовать только интерфейсы из Domain

### 3. Отсутствие Value Objects

**Проблема:** Нет примеров Value Objects, которые являются важной частью DDD.

**Решение:** Добавить Value Objects для инкапсуляции бизнес-правил:
```csharp
public record Email
{
    public string Value { get; }
    
    public Email(string value)
    {
        if (!IsValid(value))
            throw new DomainException("Invalid email");
        Value = value;
    }
    
    private static bool IsValid(string email) { /* ... */ }
}
```

### 4. Отсутствие Domain Events

**Проблема:** Нет механизма Domain Events для обработки побочных эффектов.

**Решение:** Реализовать паттерн Domain Events:
```csharp
public interface IDomainEvent { }
public class CustomerCreatedEvent : IDomainEvent { }
```

---

## 📊 Текущая структура зависимостей

**Текущая (неправильная):**
```
Presentation → Application, Infrastructure  ❌
Application → Domain  ✅
Infrastructure → Application, Domain  ❌
```

**Правильная для DDD:**
```
Presentation → Application  ✅
Application → Domain  ✅
Infrastructure → Domain  ✅
```

---

## 📋 Приоритетный план исправлений

### Приоритет 1 (Критично - нарушает принципы DDD)

1. **Убрать зависимости от EF Core из Domain**
   - Переписать `IApplicationDbContext` без `DbSet` и `DatabaseFacade`
   - Использовать абстрактные методы

2. **Убрать зависимости от ASP.NET Core из Domain**
   - Заменить `IFormFile` в `IFilesService` на абстрактный тип
   - Убрать `using Microsoft.AspNetCore.Http`

3. **Переместить UserContextCommand из Domain**
   - `Domain/Domain Services/Login/UserContextCommand.cs` → `Application/Services/Login/` или `Infrastructure/`

4. **Исправить зависимости Presentation**
   - Убрать прямую зависимость Presentation от Infrastructure
   - Переместить регистрацию сервисов в Infrastructure
   - Presentation должен вызывать только `services.AddInfrastructure()`

5. **Исправить зависимости Infrastructure**
   - Убрать зависимость Infrastructure от Application (если не нужна)
   - Infrastructure должен зависеть только от Domain

### Приоритет 2 (Важно)

6. **Переместить Infrastructure Services из Application в Infrastructure**
   - `SqlService` → `Infrastructure/Services/`
   - `MemoryCacheService`, `RedisService` → `Infrastructure/Services/Cache/`
   - `DbConStrService` → `Infrastructure/Services/`

7. **Улучшить сущности Domain**
   - Сделать setters private
   - Добавить методы для изменения состояния
   - Добавить валидацию на уровне домена
   - Добавить фабричные методы

8. **Убрать инфраструктурные зависимости из Application**
   - Вынести HTTP запросы в Infrastructure
   - Вынести работу с БД в Infrastructure

### Приоритет 3 (Улучшения)

9. **Добавить Value Objects**
   - Для Email, Address, Money и т.д.

10. **Реализовать Domain Events**
    - Для обработки побочных эффектов

11. **Добавить Aggregates**
    - Если есть сложные связи между сущностями

---

## 📊 Сводная таблица нарушений

| Категория | Количество | Приоритет | Статус |
|-----------|------------|-----------|--------|
| Реализации в Domain | 1 класс (UserContextCommand) | Критично | ❌ |
| Зависимости от инфраструктуры в Domain | 3 интерфейса | Критично | ❌ |
| Неправильные зависимости слоёв | 2 проекта | Критично | ❌ |
| Infrastructure Services в Application | 4+ класса | Важно | ⚠️ |
| Неправильные сущности | 2+ класса | Важно | ⚠️ |
| Отсутствие Value Objects | - | Низкий | ⚠️ |
| Отсутствие Domain Events | - | Низкий | ⚠️ |

---

## 🎯 Итоговая оценка

**Текущее состояние:** ⚠️ **Частичное соответствие DDD** (улучшено с 3/10 до 5/10)

**Оценка по компонентам:**
- Структура слоёв: ✅ Хорошо (есть разделение)
- Изоляция Domain: ⚠️ Улучшено, но есть проблемы (UserContextCommand, EF Core, ASP.NET Core)
- Application слой: ✅ Хорошо (больше не пустой)
- Infrastructure: ⚠️ Требует исправления зависимостей
- Сущности: ⚠️ Требуют улучшения
- Зависимости между слоями: ❌ Неправильные (Presentation→Infrastructure, Infrastructure→Application)

**Общая оценка:** 5/10

**Прогресс:** 
- ✅ Сервисы перемещены из Domain в Application
- ✅ Application слой заполнен
- ❌ Domain все еще содержит инфраструктурные зависимости
- ❌ Неправильная структура зависимостей между слоями

**Следующие шаги:**
1. Убрать все инфраструктурные зависимости из Domain
2. Исправить зависимости между слоями
3. Переместить Infrastructure Services из Application в Infrastructure

