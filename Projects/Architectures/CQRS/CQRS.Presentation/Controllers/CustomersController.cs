using CQRS.Application.Commands.CreateCustomer;
using CQRS.Application.Queries.GetCustomerById;
using CQRS.Application.Queries.GetCustomers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Presentation.Controllers;

/*
    Раздельная оптимизация
    Чтение и запись масштабируются независимо
    Для чтения можно использовать быстрый Dapper, а для записи — Entity Framework

    Соблюдение принципа SRP
    Один класс обрабатывает одну бизнес-задачу (одна команда или один запрос)
    Код становится атомарным

    Тонкие контроллеры и сервисы
    Контроллеры превращаются в обычные шлюзы
    Они лишь принимают HTTP-запрос и сразу передают его в шину (например, через MediatR)

    Снижение зацепления (Loose Coupling)
    Исчезает проблема толстых сервисов, в конструкторы которых приходится внедрять по 10–15 зависимостей

    Готовность к Event Sourcing
    Архитектура CQRS идеально адаптирована для перехода на событийную модель и аудит-логирование

    Упрощение тестирования
    Тестировать изолированный обработчик одной команды проще, чем огромный сервис со множеством переплетенных методов
*/

[ApiController]
[Route("api/[controller]")]
public sealed class CustomersController(ISender sender)
{
    [HttpPost]
    public async Task<IResult> Create([FromBody] CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        return Results.Ok(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IResult> GetById(long id, CancellationToken cancellationToken)
    {
        var getCustomerByIdQuery = new GetCustomerByIdQuery(id);

        var result = await sender.Send(getCustomerByIdQuery, cancellationToken);

        return Results.Ok(result);
    }

    [HttpGet]
    public async Task<IResult> GetAll(CancellationToken cancellationToken)
    {
        var getCustomersQuery = new GetCustomersQuery();

        // MediatR находит пару GetCustomersQuery - GetCustomersQueryHandler и регистрирует в DI-контейнере связь
        // MediatR видит, что тип объекта — GetCustomersQuery
        // Формирует тип интерфейса обработчика, который ему нужен (IRequestHandler<GetCustomersQuery, Guid>)
        // Обращается к DI-контейнеру приложения (IServiceProvider) с запросом:
        // «Дай мне класс, зарегистрированный для IRequestHandler<GetCustomersQuery, Guid>»
        // MediatR вызывает метод Handle у полученного обработчика и передает туда команду
        var result = await sender.Send(getCustomersQuery, cancellationToken);

        return Results.Ok(result);
    }
}
