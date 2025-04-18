public class A
{
	#region Разное

		select top(3) [Name], Note from Edu.dbo.People

		create database DimaChampion

		drop database Edu.dbo.People

		create table Table1 
		(
					id int not null primary key,    // key - первичный ключ
					name varchar(50),
					passport varchar(10),
					number varchar(10),
					birth date,
					unique key(passport, number),	// поля passport и number в сочетании должны быть уникальными
					unique key(birth),				// поле birth должно быть уникальным независимо от других полей
					index index1 (number)			// добавление индекса для поля number для ускорения поиска по нему
		);

		create index index1 on dbo.Table1(number);	// добавление индекса для существующей таблицы 'Table1'
													// для поля 'number'

		create unique index index1 on dbo.Table1(number)	// создание уникального индекса

		drop index index1 on dbo.Table1						// удаление индекса

		insert into Edu.dbo.People(Id, Name, Note) values	// вставить данные в таблицу
			(4, 'Dude4', 'Amazing4'),
			(5, 'Dude5', 'Amazing5'),
			(6, 'Dude6', 'Amazing6');

		// обновить поле Name и Note для записи с Id = 1
		update Edu.dbo.People
		set Name = 'Dima3', Note = 'Note3'
		where id = 1;

		// удалить из таблицы определенную запись
		delete from Edu.dbo.People
		where Id = 4;

		use [имя БД]	// после создания БД можно установить ее в качестве текущей
		go 				// разделяет отдельные наборы команд на пакеты
		begin end 		// можно использовать в начале и конце хранимой процедуры

		declare @var1 bit = 0	// int, decimal, decimal(8,2), date, datetime, time, nchar, nvarchar
		declare @var2 int = 2, @var3 int = 3

	#endregion

	#region Having Where

	/*

		WHERE
		используется для фильтрации строк на основе условий, указанных в WHERE
		Работает с отдельными строками таблицы

		HAVING
		работает с группами строк, сгруппироваными по определенным столбцам на основе агрегатных функций COUNT, SUM, AVG и т.д.

		Например, хотим найти клиентов, у которых общая сумма заказов больше 1000 рублей
		Группируем строки по "customer_id" и вычисляем общую сумму заказов с помощью функции SUM
		Затем применяем оператор HAVING, чтобы отфильтровать только те группы строк,
		у которых общая сумма заказов больше 1000 рублей

	*/

		SELECT customer_id, SUM(quantity * price) AS total_spent
		FROM orders
		GROUP BY customer_id
		HAVING total_spent > 1000;

	#endregion

	#region ACID
	/*
		ACID (Atomicity, Consistency, Isolation, Durability)
		Набор свойств, которые обеспечивают надежность и целостность транзакций в БД 

		- Atomicity (Атомарность)
		  гарантирует, что транзакция будет выполнена целиком или не выполнена совсем
		  Если в процессе выполнения транзакции происходит ошибка - все изменения откатываются

		- Consistency (Согласованность)
		  обеспечивает целостность данных в БД
		  Транзакция должна приводить БД из одного согласованного состояния в другое

		- Isolation (Изолированность)
		  гарантирует, что транзакции выполняются параллельно
		  и никакая транзакция не может повлиять на результат другой транзакции

		- Durability (Надежность)
		  гарантирует, что изменения, внесенные в БД в рамках транзакции,
		  будут сохранены даже в случае сбоя системы
	*/
	#endregion

	#region Индексы
	/*
		 Индекс - это как оглавление в книге
		 Вместо того чтобы перелистывать все страницы в поисках главы, открываем оглавление и спразу открываем нужную страницу

		 Если сделать много индексов по разным столбцам, то при изменении таблицы будут изменяться и все индексы,
		 это замедлит работу БД

		 Большое число индексов лучше использовать в БД, которые обновляются ночью

		 Кластеризованный индекс
		 - может быть один
		 - нужен для сохранения данных в отсортированном виде
		 
		Некластеризованный индекс
		- определяем для неключевых столбцов, их в таблице может быть несколько

		 Индексы лучше не использовать:
		 - в небольших таблицах
		 - для столбцов, которые часто обновляются
		 - для столбцов, где есть много null 


		Виды индексов:

			B-дерево (B-tree)
				- По умолчанию для большинства СУБД (MySQL, PostgreSQL)
				- Позволяет эффективно выполнять операции поиска, вставки и удаления
				  Хорошо работает для диапазонных запросов
				- Индексация столбцов, по которым часто выполняются операции WHERE, ORDER BY, GROUP BY

			Хэш-индекс
				- Когда требуется быстрое выполнение точечных запросов (например, =)
				- Позволяет быстро находить записи по точному значению, но не поддерживает диапазонные запросы
				- Подходит для уникальных идентификаторов или ключей

			Индексы на основе полнотекстового поиска
				- При необходимости выполнять поиск по тексту (например, в полях с большим объемом текста)
				- Позволяет эффективно искать слова и фразы в текстовых данных
				- Поиск по статьям, описаниям, комментариям и т.д

			Партитированные индексы
				- При работе с большими объемами данных, которые можно разделить на логические части
				- Индексы создаются для каждой партиции данных, что может улучшить производительность запросов
				- Когда данные имеют четкую структуру (например, по дате или географическому региону)

			Индексы на основе выражений
				- Когда нужно индексировать результат выражения или функции
				- Индекс создается на основе вычисляемого значения (например, LOWER(column_name))
				- Ускоряет запросы, использующие функции или выражения

			Составные индексы
				- Когда запросы часто используют несколько столбцов в условиях WHERE
				- Индексируется комбинация нескольких столбцов
				- Ускоряет выполнение запросов с несколькими условиями

	*/
	#endregion

	#region Уровни изоляции транзакций

		// Начало транзакции с уровнем изоляции READ COMMITTED
		BEGIN TRANSACTION;
		SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

		UPDATE Employees SET Salary = Salary * 1.1 WHERE Department = 'IT';

		COMMIT; // Фиксация транзакции


		// Начало новой транзакции с уровнем изоляции SERIALIZABLE
		BEGIN TRANSACTION;
		SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

		INSERT INTO Orders(OrderDate, ProductID, Quantity) VALUES('2023-01-15', 101, 5);

		// Фиксация или откат транзакции в зависимости от условий
		IF(SELECT COUNT(*) FROM Orders WHERE OrderDate = '2023-01-15') > 0
			BEGIN
				COMMIT;
			END
		ELSE
			BEGIN
				ROLLBACK;
			END;

	/*
		Уровни изоляции транзакций:
		Настраиваются отдельно для каждого соединения с БД
		set session transaction isolation level [тип уровня изоляции]
	
		- Read uncommitted
			если данные еще не закоммичены в рамках одной транзакции,
			мы все-равно можем их прочитать из другой
			(грязное чтение - незакомиченные данные лучше не трогать)
			Нельзя использовать для точных вычислений, можно для примерных,
			т.к. транзация может быть отменена и данные будут неточными
		
		- Read committed
			позволяет читать только закоммиченные данные из другой транзакции
		
		- Repeatable read (по умолчанию)
			исключает влияние других транзакций, но разрешает добавление новых записей
		
		- Serializable
			полностью исключает влияние других транзакций, самый медленный
		
		- Snapshot
			если две транзакции совершаются параллельно - каждой из них выдают свой слепок БД
			После выполнения операций со слепком (удаление/запись/изменение/чтение),
			изменения вливаются в основную БД
			Транзакция завершится успешно, если в основной БД к моменту окончания
			транзакции ни в одной из ячеек БД, измененных транзакцией, не было изменений
			за время ее выполнения
			Если две транзакции выполняли операции над разными частями БД,
			то конфликтов не возникнет и произойдет слияние
			Если изменялись одни и те же данные - получим аномалию
	*/
	#endregion

	#region Удаление

	TRUNCATE TABLE [Имя таблицы]	// Очищает всю таблицу, ничего не возвращает
	DELETE FROM [Имя таблицы]		// Если нет WHERE, то работает аналогично TRUNCATE, возвращает число удаленных строк
	DROP TABLE [Имя таблицы]		// Удалить таблицу
	DROP DATABASE [Имя БД]			// Удалить БД

	#endregion

	#region switch

		select
			case
				when condition_1 then result_1
				when condition_2 then result_2
				else retult_3
			end
		from Products

	#endregion

	#region Проверки, что БД существует

		IF EXISTS (SELECT name FROM sys.databases WHERE name = 'People')

		IF DB_ID('People') IS NOT NULL

	#endregion

	#region UNION отображает данные из двух одинаковых таблиц

		SELECT* FROM People
		UNION
		SELECT * FROM People2
				   
		SELECT Name, Note, 'Table 1' as Tbl FROM People	// Чтобы UNION отобразил данные из таблиц с разным числом столбцов,
		UNION											// нужно вручную указать одинаковые столбцы
		SELECT Name, Note, 'Table 2' FROM People2		// Можно добавить столбец Tbl и указать, какие данные к какой таблице относятся

        // Вложенный запрос
        SELECT* FROM Products
		WHERE Price = (SELECT MAX(Price) FROM Products)	// если вложенный запрос возвращает более 1 результата, знак '=' заменить на 'IN'

	#endregion

	#region Join

	select* from Authors
	select * from Books

	// join
	select Name, Title from Authors a		// Возвращает записи с совпадениями в обеих таблицах
		join Books b
		on a.Id = b.AuthorId

	// left join
	select Name, Title from Authors a		// Возвращает все из левой и совпадения из правой
		left join Books b
		on a.Id = b.AuthorId

	// right join
	select Name, Title from Authors a		// Возвращает все из правой и совпадения из левой
		right join Books b
		on a.Id = b.AuthorId

	// full join
	select Name, Title from Authors a		// Отображаем все записи из обеих таблиц
		full join Books b
		on a.Id = b.AuthorId

	// cross join
	select Name, Title from Authors			// Для каждой записи из 1й таблицы подставим все записи из 2й
		cross join Books

	#endregion

	#region Изменение таблицы

	ALTER TABLE TestTable				// ALTER добавляет/удаляет/измененяет столбцы и ограничения в таблице
	ADD newColumn nvarchar(20)			// Добавляем столбец

	ALTER TABLE TestTable
	DROP COLUMN newColumn				// Удаляем столбец

	ALTER TABLE TestTable
	ALTER COLUMN newColumn VARCHAR		// Меняем тип данных существующего столбца

	alter table Products				// Добавить столбец с ограничением
	add price decimal constraint checkPrice check (price >= 0)

	#endregion

	#region Курсоры - позволяют читать данные построчно

		declare cursor_name cursor scroll			// scroll позволяет читать курсор в обратном направлении
		for select * from Edu.dbo.People			// Объявляем курсор с определенным select
			open cursor_name						// Открываем курсор для чтения
			fetch next from cursor_name				// Вывели первую строку и переставили курсор вперед
			fetch prior from cursor_name			// Вывели прерыдущую строку и переставили курсор назад
			fetch last from cursor_name				// Вывели первую строку
			fetch first from cursor_name			// Вывели последнюю строку
			fetch absolute 3 from cursor_name		// Вывели третью строку
			fetch relative 2 from cursor_name		// С каким шагом будем ходить, может быть отрицательным
		close cursor_name							// Закрываем курсор
		deallocate cursor_name						// Удаляем курсор

	#endregion

	#region Процедуры

		use Edu								// Дальнейшие команды будут относиться к БД Edu
		go

											// Процедуры могут принимать параметры, но не возвращают значения
											// Создадим процедуру
		create procedure spProcedure1		// Вместо procedure можно писать proc
											// Если вместо create написать alter, можно изменить процедуру
		as
			select * from Edu.dbo.People
		go

		execute spProcedure1				// Вызываем процедуру через execute или exec
		go

		create proc spProcedure2			// Процедура с параметром
			@var1 int = 0					// Параметру можно задать дефолтное значение, тогда его можно не передавать
		as
			select * from dbo.People
			where Id > @var1
		go

		exec spProcedure2 @var1=1			// Вызов процедуры с параметром
		go

	#endregion

	#region Функции

		create function fnFunc1 (@var1 int)		// Объявление функции
		   returns int							// Тип возвращаемого значения
		as
			begin
			declare @var2 int;
		set @var2 = @var1 * 2;
		return @var2;

		print dbo.fnFunc1 (2)					// Вызов функции

	#endregion

	#region Триггеры

		// Триггер на insert в таблицу OrderDetails
		begin
			create trigger trMatchingCtocksOnInsert		
			on dbo.OrderDetails for insert
			as
				if @@ROWCOUNT = 0			// rowcount показыает, сколько строк мы собираемся вставить
					return
				set nocount on				// Отключаем сообщения о числе обработанных записей, увеличивая производительность

				update Stocks set Qty = s.Qty - i.Qty	// inserted - служебная таблица с вставляемыми данными
				from Stocks s join (select ProductID, sum(Qty) Qty from inserted group by ProductID) i
				on s.ProductID = i.ProductID

		// Триггер на delete
		create trigger trMatchingStocksOnDelete
		on OrderDetails for delete
		as
			if @@ROWCOUNT = 0
				return
			set nocount on

			update Stocks set Qty = s.Qty + d.Qty	// Служебная таблица deleted хранит записи,
                                                    // которые будут удалены
			from Stocks s join (select ProductID, sum(Qty) Qty from deleted group by ProductID) d
			on s.ProductID = d.ProductID
		go

		// Триггер на update
		create trigger trMatchingStocksOnUpdate
		on OrderDetails for update
		as
		if @@ROWCOUNT = 0
			return

		// Если не обновляем поле Qty, то выходим из триггера
		if not UPDATE(Qty)
			return

		set nocount on

		update Stocks set Qty = s.Qty - (i.Qty - d.Qty)
		from Stocks s
		join (select ProductID, sum(Qty) Qty from deleted group by ProductID) d
			 on s.ProductID = d.ProductID
		join (select ProductID, sum(Qty) Qty from inserted group by ProductID) i
			 on s.ProductID = i.ProductID
		go

		// Триггер на insert, delete, update
		create trigger trMatchingStocks
		on OrderDetails
		for insert, delete, update
		as
		if @@ROWCOUNT = 0
			return
		set nocount on

		// Если в таблицах exists и deleted есть данные, то происходит update
		// Указываем 1 для увеличения производительности, т.к. это просто проверка, есть-ли в этих таблицах строки
		if exists (select 1 from inserted) and exists(select 1 from deleted)
			begin
				if not UPDATE(Qty)
					return
				update..end
		// Если в inserted нет записей, а в deleted есть, то происходит удаление
		else if not exists (select 1 from inserted) and exists(select 1 from deleted)
			begin
				delete..
			end
		// Если в inserted есть записи, а в deleted нет, то происходит insert
		else if exists (select 1 from inserted) and not exists (select 1 from deleted)	
			begin
				insert..
			end
		go

		// Если перед удалением записей из таблицы Products нужно проверить, можно-ли удалять, используем instead of delete
		create trigger trAllowDeleteProdut
		on Products
		instead of delete
		as
		begin
			// Если в OrderDetails есть хоть одна запись, отменяем удаление
			if exists (select 1 from OrderDetails od
					   join deleted d
					   on od.ProductID = d.ID)
				raiserror('Товар не может быть удален', 10, 1)
			// Если записей нет, выполняем удаление
			else
				delete Products where ID in (select ID from deleted)
		end
		go

	#endregion 

	#region Предаставления, процедуры

		// Представления используют для отображения данных
		CREATE VIEW ShowProducts
		AS
		SELECT FName + ' ' + LName FullName FROM Customers WHERE ID > 2
		GO
		SELECT * FROM ShowProducts

		// Процедуры используют для добавления/изменения данных
		create or alter proc spSearchProducts
			@LName nvarchar(20) = '%'// Входные параметры с значениями по умолчанию
		as
		set nocount on
		select * from Customers where LName = @LName
		go
		exec spSearchProducts 'Крава'

    #endregion

    #region Методы борьбы с SQL - инъекциями

    /*
		Использование параметризованных запросов вместо строковых
		Использование хранимых процедур для выполнения операций с БД
		Проверка входных данных на наличие запрещенных символов и символов-разделителей
		Использование ORM-библиотек, которые автоматически защищают от SQL-инъекций (Entity Framework Core)
		Ограничение прав доступа к БД для пользователя, использующего приложение
		Использование транзакций при выполнении операций с БД
	*/

    #endregion

    #region План запроса

    /*
		План запроса (Query Execution Plan) определяет:
		- как СУБД будет выполнять запрос
		- какие индексы и операции будут использованы для получения требуемых данных
		- помогает оптимизировать выполнение запросов

		Включает шаги:
		- парсинг запроса - СУБД проверяет синтаксис на корректность
		- оптимизация запроса - СУБД оптимизирует запрос, выбирая наилучший план выполнения
		  Учитываются индексы, статистика таблиц, доступные ресурсы
		- генерация плана выполнения - СУБД создает план выполнения, который описывает порядок операций для получения данных
		  План может включать выборку данных из таблиц, использование индексов, сортировку, объединение данных
		- выполнение плана - СУБД выполняет план выполнения запроса и возвращает результат

		Анализируя план запроса, можно определить эффективность выполнения запроса,
		выявить проблемы с производительностью и внести изменения для оптимизации

		Как можно работать с планом запроса:
		- Анализ производительности - изучите план запроса, чтобы понять, 
		  как СУБД выполняет запрос, какие индексы используются, какие операции выполняются
		- Индексирование - просмотрите план запроса, чтобы определить, какие индексы используются или не используются при выполнении запроса
		- Использование подсказок - в некоторых случаях СУБД может выбрать неоптимальный план выполнения запроса
		  Вы можете использовать подсказки в SQL запросе, чтобы указать СУБД на определенный план выполнения
		- Мониторинг производительности - регулярно анализируйте планы выполнения запросов для отслеживания производительности
	*/

    #endregion

    #region Нормальные формы

    /*
		Первая нормальная форма:
		- нет повторяющихся данных в разных таблицах и в пределах одной таблицы
		- для каждого набора связанных данных есть отдельная таблица
		- каждый набор связанных данных идентифицирован с помощью первичного ключа
		- каждая ячейка таблицы должна содержать только одно неделимое значение

		Вторая нормальная форма:
		- есть отдельные таблицы для наборов значений, относящихся к нескольким записям
		- эти таблицы связаны с помощью внешнего ключа
		- записи не зависят от чего-либо, кроме первичного ключа таблицы (составного ключа, если это необходимо)

		Третья нормальная форма:
		- если содержимое группы полей относится более чем к одной записи в таблице - эти поля выносятся в отдельную таблицу
	*/

    #endregion

    #region Преимущества и недостатки хранимых процедур по сравнению с запросами
    /*

		Преимущества хранимых процедур (ХП):
			- компилируются и оптимизируются на сервере БД, чтобыстрее по сравнению с динамическими SQL-запросами,
			  которые компилируются каждый раз при выполнении
			- могут выполнять сложные операции на стороне БД, что уменьшает объем данных, передаваемых между приложением и БД
			- можно предоставить пользователям доступ к выполнению процедур без предоставления прямого доступа к таблицам, что более безопасно

		Недостатки хранимых процедур:
			- ХП часто зависят от конкретной реализации СУБД, что может затруднить переносимость приложения между различными системами
			- отладка и тестирование сложнее, чем отладка кода на уровне приложения
			- увеличение сложности управления версиями
			- если бизнес-логика сильно завязана на ХП - это усложняет поддержку и изменение приложения

		Преимущества запросов со среднего слоя:
			- запросы легко менять без необходимости изменять код БД
			- код приложения легче отлаживать, чем ХП
			- запросы на уровне приложения могут быть более портируемыми между различными СУБД
			- логика приложения может быть четко отделена от логики БД, что упрощает поддержку

		Недостатки запросов со среднего слоя:
			- динамические запросы могут быть менее эффективными по сравнению с хранимыми процедурами, если часто выполняются с одинаковыми параметрами
			- если приложение выполняет множество отдельных запросов для получения данных, растет объем передаваемых данных
			- необходимость управления доступом к данным на уровне приложения может привести к уязвимостям, если не реализованы механизмы безопасности

	*/
    #endregion

    #region Оконная функция
    /*

		Позволяют выполнять вычисления по набору строк, связанному с текущей строкой

		Полезны для таких операций:
		- как вычисление скользящих средних, 
		- ранжирование и агрегация данных без группировки

		Пример 1:

			Пример использования оконной функции ROW_NUMBER() для присвоения уникального номера каждой строке в рамках определенной группы

			Есть таблица employees с данными о сотрудниках:

				CREATE TABLE employees (
					id INT,
					name VARCHAR(100),
					department VARCHAR(100),
					salary DECIMAL(10, 2)
				);

			Мы хотим получить список сотрудников с их уникальными номерами в пределах каждого отдела, отсортированным по зарплате

				SELECT 
					id,
					name,
					department,
					salary,
					ROW_NUMBER() OVER (PARTITION BY department ORDER BY salary DESC) AS rank
				FROM 
					employees;

			- PARTITION BY department
			  делит набор строк на группы по отделам

			- ORDER BY salary DESC
			  сортирует сотрудников внутри каждой группы по зарплате в порядке убывания

			- ROW_NUMBER()
			  присваивает уникальный номер каждой строке в пределах своей группы.

			Результат будет содержать столбец rank, который показывает позицию каждого сотрудника в своем отделе по зарплате

		Пример 2:

			Есть таблица sales, которая содержит информацию о продажах:

				CREATE TABLE sales (
					id INT,
					salesperson VARCHAR(100),
					sale_amount DECIMAL(10, 2),
					sale_date DATE
				);

			Мы хотим рассчитать кумулятивную сумму продаж для каждого продавца по датам
			Используем оконную функцию SUM() с предложением OVER():

				SELECT 
					id,
					salesperson,
					sale_amount,
					sale_date,
					SUM(sale_amount) OVER (PARTITION BY salesperson ORDER BY sale_date) AS cumulative_sales
				FROM 
					sales
				ORDER BY 
					salesperson, sale_date;

			- SUM(sale_amount)
			  вычисляет сумму продаж

			- OVER (PARTITION BY salesperson ORDER BY sale_date)
			  делит набор строк на группы по продавцам и сортирует их по дате продажи
			  Кумулятивная сумма будет рассчитываться для каждой группы (продавца) по порядку дат

			- ORDER BY salesperson, sale_date
			  сортирует итоговый результат по продавцу и дате

			Результат будет содержать столбец cumulative_sales,
			который показывает кумулятивную сумму продаж для каждого продавца по мере продвижения по датам

	*/
    #endregion

}