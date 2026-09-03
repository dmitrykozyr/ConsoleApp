/*

	Распределенные транзакции

	Saga Pattern
		Используется, когда транзакция разносится на несколько независимых микросервисов, у каждого из которых своя БД:
		- сервис заказов бронирует товар
		- сервис оплаты списывает деньги
		- сервис доставки назначает курьера
		Если на шаге 3 произошла ошибка, Saga должна запустить компенсирующие транзакции (вернуть деньги в шаге 2 и разбронировать товар в шаге 1)

	MassTransit
		Золотой стандарт реализации паттерна Saga в .NET
		Берет на себя:
		- хранение состояния (State)
		- работу с очередями (RabbitMQ/Kafka)
		- автоматические повторы при сбоях

		Для этого используется State Machine (Автомат состояний)
		Опишем логику: Если пришло событие А — сделай Б и перейди в состояние В
		- пакеты MassTransit, MassTransit.AspNetCore
		- провайдер БД (EntityFrameworkCore для хранения состояния саги)
		- Описание состояний и данных (Saga State) - сага должна помнить, на каком этапе она находится
		  Если придет 1000 заказов одновременно, MassTransit по CorrelationId поймет,
		  какой ответ от платежной системы относится к какому заказу
		  Если сервер упадет посреди оплаты, после перезагрузки сага прочитает свое состояние из БД и продолжит с того же места
		- логику OrderStateMachine легко читать и изменять без кучи вложенных if
*/

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddMassTransit(configurator =>
//{
//	// Добавляем сагу
//	configurator.AddSagaStateMachine<OrderStateMachine, OrderSagaState>()
//		.InMemoryRepository();

//	configurator.UsingRabbitMq((context, cfg) =>
//	{
//		cfg.ConfigureEndpoints(context);
//	});
//});


//var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

//app.Run();
