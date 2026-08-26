Представим таблицу:

	Id | Date       | UserId | Country | Product | Price
	------------------------------------------------------
	1  | 2026-01-01 | 101    | DE      | A       | 100
	2  | 2026-01-01 | 102    | DE      | B       | 200
	3  | 2026-01-01 | 103    | FR      | A       | 150

В обычной row-oriented БД запись физически организована примерно как:

	[Id, Date, UserId, Country, Product, Price]
	[Id, Date, UserId, Country, Product, Price]
	[Id, Date, UserId, Country, Product, Price]

А ClickHouse ориентирован на хранение:

	Id:       1, 2, 3
	Date:     01.01, 01.01, 01.01
	UserId:   101, 102, 103
	Country:  DE, DE, FR, ...
	Product:  A, B, A, ...
	Price:    100, 200, 150, ...



Не стоит воспринимать ClickHouse как PostgreSQL

В PostgreSQL можем думать:

	Мне нужна таблица пользователей,
	я буду делать INSERT/UPDATE/DELETE отдельных пользователей

В ClickHouse мышление другое:

	У меня есть огромный поток событий, логов, продаж, telemetry, заказов
	Я постоянно добавляю данные и потом очень быстро анализирую большие объёмы

	Например:

		2026-08-26 10:01:01  User 123  opened product
		2026-08-26 10:01:02  User 456  bought product
		2026-08-26 10:01:03  User 789  opened product
		...

		Сколько пользователей из Германии покупали товары категории X за последние 30 дней?
		Это очень характерная задача ClickHouse

Официальная документация подчёркивает,
что ClickHouse подходит для аналитики по большим диапазонам данных и агрегаций

Компании часто используют обе базы,
а не пытаются заменить PostgreSQL ClickHouse'ом
OLTP и OLAP решают разные задачи

===========================================================================================================

MergeTree — механизм хранения ClickHouse, который
- хранит данные частями
- сортирует их согласно ORDER BY
- в фоне объединяет эти части

Благодаря физической организации данных ClickHouse может эффективно пропускать ненужные блоки при аналитических запросах

При команде INSERT INTO Sales
ClickHouse создаёт отдельную data part — часть данных:

	Sales
	│
	├── part_1    ← первые 100 000 строк
	├── part_2    ← следующие 100 000
	├── part_3    ← следующие 100 000
	└── part_4    ← ещё 100 000

Со временем ClickHouse в фоне объединяет небольшие части:

	part_1 + part_2
		  ↓
	   part_5

	part_3 + part_4
		  ↓
	   part_6

Затем:

	part_5 + part_6
		  ↓
	   большая часть

Отсюда и MergeTree — дерево частей, которые постепенно объединяются

Представим, что каждую секунду приходит 10 000 событий
Не нужно каждый раз физически перестраивать огромную таблицу

ClickHouse добавляет новые данные отдельными частями:

	10:00:01 → part_1
	10:00:02 → part_2
	10:00:03 → part_3
	10:00:04 → part_4
	...

А потом сам занимается их объединением

Но есть ещё более важная часть

В нашем примере:

	ORDER BY (Date, Country)

это не просто сортировка результата SELECT,
а часть физической организации данных внутри MergeTree.

Например, данные будут организованы примерно по:

	Date
	  │
	  ├── 2026-08-01
	  │     ├── DE
	  │     ├── DE
	  │     ├── FR
	  │     └── IT
	  │
	  ├── 2026-08-02
	  │     ├── DE
	  │     ├── DE
	  │     └── FR
	  │
	  └── 2026-08-03
			├── DE
			├── ES
			└── FR

И это очень важно для скорости запросов.

Например, запрос:

	SELECT
		Country,
		sum(Quantity * Price)
	FROM Sales
	WHERE Date >= '2026-08-20'
	GROUP BY Country;

ClickHouse может понять, что нужны данные только начиная с 20 августа
Благодаря физической организации данных ему не обязательно читать абсолютно всё

===========================================================================================================

Создание, БД, таблиц и их наполнение:

	CREATE DATABASE IF NOT EXISTS sales_demo;

	USE sales_demo;

	CREATE TABLE IF NOT EXISTS Sales
	(
		Id       UInt64,
		Date     DateTime,
		UserId   UInt64,
		Country  LowCardinality(String),
		Product  String,
		Quantity UInt32,
		Price    Decimal(18, 2)
	)
	ENGINE = MergeTree
	ORDER BY (Date, Country);

	SELECT *
	FROM Sales
	ORDER BY Id;


Создадим 100 000 записей:

	INSERT INTO Sales
	SELECT
		number + 1000 AS Id,

		now() - INTERVAL (number % 30) DAY
			- INTERVAL (number % 86400) SECOND AS Date,

		(number % 10000) + 1 AS UserId,

		arrayElement(
			['DE', 'FR', 'IT', 'ES', 'GB', 'US'],
			(number % 6) + 1
		) AS Country,

		arrayElement(
			['A', 'B', 'C', 'D', 'E'],
			(number % 5) + 1
		) AS Product,

		(number % 5) + 1 AS Quantity,

		arrayElement(
			[100.00, 150.00, 200.00, 250.00, 300.00],
			(number % 5) + 1
		) AS Price

	FROM numbers(100000); -- создаёт последовательность 0 .. 99.999


Посмотрим данные:

	SELECT *
	FROM Sales
	LIMIT 20;


Сколько продаж по странам:

	SELECT
		Country,
		count() AS SalesCount
	FROM Sales
	GROUP BY Country
	ORDER BY SalesCount DESC;


Сколько денег заработали по странам:

	SELECT
		Country,
		sum(Quantity * Price) AS TotalSales
	FROM Sales
	GROUP BY Country
	ORDER BY TotalSales DESC;


Продажи по товарам:

	SELECT
		Product,
		sum(Quantity) AS TotalQuantity,
		sum(Quantity * Price) AS TotalSales
	FROM Sales
	GROUP BY Product
	ORDER BY TotalSales DESC;


Продажи по дням:

	SELECT
		toDate(Date) AS Day,
		count() AS SalesCount,
		sum(Quantity * Price) AS TotalSales
	FROM Sales
	GROUP BY Day
	ORDER BY Day;

Продажи Германии:

	SELECT
		Product,
		sum(Quantity) AS Quantity2,
		sum(Quantity * Price) AS TotalSales
	FROM Sales
	WHERE Country = 'DE'
	GROUP BY Product
	ORDER BY TotalSales DESC;


Типичный ClickHouse-запрос: взять огромное количество строк и быстро получить агрегированную информацию

	SELECT
		Country,
		Product,
		sum(Quantity) AS Quantity2,
		sum(Quantity * Price) AS TotalSales
	FROM Sales
	GROUP BY
		Country,
		Product
	ORDER BY
		Country,
		TotalSales DESC;
