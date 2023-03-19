--========================== ИНДЕКСЫ =============================
 Индекс - это как оглавление в книге
 Вместо того чтобы перелистывать все страницы в поисках главы, открываем оглавление и спразу открываем нужную страницу

 Если сделать много индексов по разным столбцам, то при изменении таблицы будут изменяться и все индексы,
 это замедлит работу БД

 Большое число индексов лучше использовать в БД, которые обновляются ночью

 Кластеризованный индекс в таблице может быть один, нужен для сохранения данных в отсортированном виде
 Некластеризованный индекс мы определяем для неключевых столбцов, их таблице может быть несколько

 Индексы лучше не использовать:
 - в небольших таблицах
 - для столбцов, которые часто обновляются
 - для столбцов, где есть много null

--========================== Уровни изоляции транзакций ==========
- Read uncommitted	- используется, когда все транзакции на чтение, самый быстрый
- Read committed	- если работаем со строками, другие с транзакции тоже могут с ними работать
- Repeatable read	- исключает влияние других транзакций, но разрешает добавление новых записей
- Serializable		- полностью исключает влияние других транзакций, самый медленный

--========================== РАЗНОЕ ==============================
SELECT TOP (3) [Name], Note FROM Edu.dbo.People

create database DimaChampion

drop database Edu.dbo.People

create table Table1 
(
			id int not null primary key,	-- key - первичный ключ
		    name varchar(50),
		    passport varchar(10),
		    number varchar(10),
		    birth date,
		    unique key(passport, number),	-- поля passport и number в сочетании должны быть уникальными
		    unique key(birth),				-- поле birth должно быть уникальным независимо от других полей
		    index index1 (number)			-- добавление индекса для поля number для ускорения поиска по нему
);
		     
create index index1 on dbo.Table1(number);	-- добавление индекса для существующей таблицы 'Table1' для поля 'number'

create unique index index1 on dbo.Table1(number)	-- создание уникального индекса

drop index index1 on dbo.Table1						-- удаление индекса

insert into Edu.dbo.People(Id, Name, Note) values	-- вставить данные в таблицу
	(4, 'Dude4', 'Amazing4'),
	(5, 'Dude5', 'Amazing5'),
	(6, 'Dude6', 'Amazing6');						

update Edu.dbo.People set Name = 'Dima3', Note = 'Note3' where id = 1;	-- обновить поле Name и Note для записи с Id = 1

delete from Edu.dbo.People where Id = 4;	-- удалить из таблицы определенную запись

USE [имя БД] 		-- после создания БД можно установить ее в качестве текущей
GO 					-- разделяет отдельные наборы команд на пакеты
BEGIN END 			-- можно использовать в начале и конце хранимой процедуры

-- переменные
declare @var1 bit = 0	-- int, decimal, decimal(8,2), date, datetime, time, nchar, nvarchar

declare @var2 int = 2, @var3 int = 3

-- удаление
TRUNCATE TABLE [Имя таблицы]	-- очищает всю таблицу, ничего не возвращает
DELETE FROM [Имя таблицы]		-- если нет WHERE, то работает аналогично TRUNCATE, возвращает число удаленных строк
DROP TABLE [Имя таблицы]		-- удалить таблицу
DROP DATABASE [Имя БД]			-- удалить БД

-- switch
select
	case
		when condition_1 then result_1
		when condition_2 then result_2
		else retult_3
	end
from Products

-- проверки, что БД существует
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'People')

IF DB_ID('People') IS NOT NULL

INSERT INTO Edu.dbo.People(Id, name, Note) VALUES (6, 'Name 6', 'LName');

UPDATE Edu.dbo.People SET [name] = 'Some Name' WHERE Id = 5

-- UNION отображает данные из двух одинаковых таблиц
SELECT * FROM People
UNION
SELECT * FROM People2
				   
SELECT Name, Note, 'Table 1' as Tbl FROM People	-- Чтобы UNION отобразил данные из таблиц с разным числом столбцов,
UNION											-- нужно вручную указать одинаковые столбцы
SELECT Name, Note, 'Table 2' FROM People2		-- можно добавить столбец Tbl и указать, какие данные к какой таблице относятся
				   
SELECT * FROM Products WHERE Price =	-- вложенный запрос
	(SELECT MAX(Price) FROM Products)	-- если вложенный запрос возвращает более 1 результата, знак '=' заменить на 'IN'

-- Inner Join
select * from Authors
select * from Books

select Name, Title from Authors a	-- Возвращает записи с совпадениями в обеиз таблицах
	join Books b
	on a.Id = b.AuthorId			-- если в обеих таблицах имя сравниваемого поля одинаковое, можно написать using(Id)

select Name, Title from Authors a	-- Возвращает все из левой и совпадения из правой
	left join Books b
	on a.Id = b.AuthorId

select Name, Title from Authors a	-- Возвращает все из правой и совпадения из левой
	right join Books b
	on a.Id = b.AuthorId

select Name, Title from Authors a	-- Отображаем все записи из обеих таблиц
	full join Books b
	on a.Id = b.AuthorId

select Name, Title from Authors		-- Для каждой записи из 1й таблицы подставим все записи из 2й
	cross join Books

--==================== ИЗМЕНЕНИЕ ТАБЛИЦЫ =========================
ALTER TABLE TestTable				-- ALTER добавляет/удаляет/измененяет столбцы и ограничения в таблице
ADD newColumn nvarchar(20)			-- добавляем столбец

ALTER TABLE TestTable
DROP COLUMN newColumn				-- удаляем столбец

ALTER TABLE TestTable
ALTER COLUMN newColumn VARCHAR		-- меняем тип данных существующего столбца

alter table Products				-- добавить столбец с ограничением
add price decimal constraint checkPrice check (price >= 0)
										     
--========================== КУРСОРЫ =============================
-- кусор позволяет читать данные построчно
declare cursor_name cursor scroll		-- scroll позволяет читать курсор в обратном направлении
	for select * from Edu.dbo.People	-- объявляем курсор с определенным select
open cursor_name						-- открываем курсор для чтения
fetch next from cursor_name				-- вывели первую строку и переставили курсор вперед
fetch prior from cursor_name			-- вывели прерыдущую строку и переставили курсор назад
fetch last from cursor_name				-- вывели первую строку
fetch first from cursor_name			-- вывели последнюю строку
fetch absolute 3 from cursor_name		-- вывели третью строку
fetch relative 2 from cursor_name		-- с каким шагом будем ходить, может быть отрицательным
close cursor_name						-- закрываем курсор
deallocate cursor_name					-- удаляем курсор

--======================== ПРОЦЕДУРЫ =============================
use Edu								-- дальнейшие команды будут относиться к БД Edu
go

-- процедуры могут принимать параметры, но не возвращают значения
-- создадим процедуру с префиксом sp
create procedure spProcedure1		-- вместо procedure можно писать proc
									-- если вместо create написать alter, можно изменить процедуру
as
	select * from Edu.dbo.People
go

execute spProcedure1				-- вызываем процедуру через execute или exec
go

create proc spProcedure2			-- процедура с параметром
	@var1 int = 0					-- параметру можно задать дефолтное значение, тогда его можно не передавать
as
	set nocount on					-- отключаем отображение количества обработанных строк
	select * from dbo.People
	where Id > @var1
go

exec spProcedure2 @var1=1			-- вызов процедуры с параметром
go

--========================== ФУНКЦИИ =============================
create function fnFunc1 (@var1 int)		-- объявление функции
	returns int							-- тип возвращаемого значения
as
	begin
	declare @var2 int;
	set @var2 = @var1 * 2;
	return @var2;
	end
go

print dbo.fnFunc1 (2)					-- вызов функции
										     
--========================== ТРИГГЕРЫ ============================
-- триггер на insert в таблицу OrderDetails
begin
	create trigger trMatchingCtocksOnInsert		
	on dbo.OrderDetails for insert
	as
		if @@ROWCOUNT = 0			-- rowcount показыает, сколько строк мы собираемся вставить
			return
		set nocount on				-- отключаем сообщения о числе обработанных записей, увеличивая производительность

		update Stocks set Qty = s.Qty - i.Qty	-- inserted - служебная таблица с вставляемыми данными
		from Stocks s join (select ProductID, sum(Qty) Qty from inserted group by ProductID) i
		on s.ProductID = i.ProductID
end
go

-- триггер на delete
create trigger trMatchingStocksOnDelete
on OrderDetails for delete
as
	if @@ROWCOUNT = 0
		return
	set nocount on

	update Stocks set Qty = s.Qty + d.Qty	-- служебная таблица deleted хранит записи, которые будут удалены
	from Stocks s join (select ProductID, sum(Qty) Qty from deleted group by ProductID) d
	on s.ProductID = d.ProductID
go

-- триггер на update
create trigger trMatchingStocksOnUpdate
on OrderDetails for update
as
	if @@ROWCOUNT = 0
		return

	if not UPDATE(Qty)						-- если мы не обновляем поле Qty, то выходим из триггера
		return

	set nocount on

	update Stocks set Qty = s.Qty - (i.Qty - d.Qty)
	from Stocks s
	join	
	(select ProductID, sum(Qty) Qty from deleted group by ProductID) d
	on s.ProductID = d.ProductID
	join
	(select ProductID, sum(Qty) Qty from inserted group by ProductID) i
	on s.ProductID = i.ProductID
go

-- триггер на insert, delete, update
create trigger trMatchingStocks
on OrderDetails
for insert, delete, update
as
	if @@ROWCOUNT = 0
		return
	set nocount on

	-- если в таблицах exists и deleted есть данные, то происходит update
	-- указываем 1 для увеличения производительности, т.к. это просто проверка, есть-ли в этих таблицах строки
	if exists (select 1 from inserted) and exists (select 1 from deleted)
		begin
			if not UPDATE(Qty)
				return
			update ..
		end
	-- если в inserted нет записей, а в deleted есть, то происходит удаление
	else if not exists (select 1 from inserted) and exists (select 1 from deleted)
		begin
			delete ..
		end
	-- если в inserted есть записи, а в deleted нет, то происходит insert
	else if exists (select 1 from inserted) and not exists (select 1 from deleted)	
		begin
			insert ..
		end
go

-- если перед удалением записей из таблицы Products нужно проверить, можно-ли удалять, используем instead of delete
create trigger trAllowDeleteProdut
on Products
instead of delete
as
	begin
		-- если в OrderDetails есть хоть одна запись, отменяем удаление
		if exists (select 1 from OrderDetails od
				   join deleted d
				   on od.ProductID = d.ID)
			raiserror('Товар не может быть удален', 10, 1)
		-- если записей нет, выполняем удаление
		else
			delete Products where ID in (select ID from deleted)
	end
go

--=============== ПРЕДСТАВЛЕНИЯ, ПРОЦЕДУРЫ =======================
-- представления используют для отображения данных
create view ShowProducts
as
select FName + ' ' + LName FullName from Customers where ID > 2
go
select * from ShowProducts

-- процедуры используют для добавления/изменения данных
create or alter proc spSearchProducts
	@LName nvarchar(20) = '%'		-- входные параметры с значениями по умолчанию
as
set nocount on
select * from Customers where LName = @LName
go
exec spSearchProducts 'Крава'
