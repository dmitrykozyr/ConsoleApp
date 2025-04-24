public class A
{
	// Типы
    /*		
		- integer
		- integer[]
		- bigint
		- varchar(1200)
		- text
		- timestamp
		- date
		- boolean
		- uuid
		- jsonb		 
	*/

	#region CREATE

		CREATE DATABASE DimaChampion

		CREATE TABLE IF NOT EXISTS schema_name.Table1
		(
			id			bigint generated always as identity
						primary key
			name		varchar(50),
			condition	integer			not null,
			passport	varchar(10),
			number		varchar(10),
			birth		date,
			unique		key(passport, number),	// поля passport и number в сочетании должны быть уникальными
			unique		key(birth),				// поле birth должно быть уникальным независимо от других полей
			index		index1(number)			// добавление индекса для поля number для ускорения поиска по нему
		);

	#endregion

	#region SELECT

		SELECT TOP(3) [Name], Note
		FROM Edu.dbo.People

		// Если вложенный запрос возвращает более 1 результата, знак '=' заменить на 'IN'
		SELECT *
		FROM Products
		WHERE Price = (SELECT MAX(Price) FROM Products)

		// WHERE фильтрует строки на основе условий, указанных в WHERE
		// HAVING фильтрует сгруппированыме по определенным столбцам строки на основе агрегатных функций COUNT, SUM, AVG
		// Найдем клиентов с суммой заказов > 1000р
		// Группируем и вычисляем сумму функцией SUM
		// HAVING отфильтровывает группы строк, у которых сумма заказов > 1000р
		SELECT customer_id, SUM(quantity * price) AS total_spent
		FROM orders
		GROUP BY customer_id
		HAVING total_spent > 1000;

		// SWITCH
		SELECT
			CASE
				WHEN condition_1 THEN result_1
				WHEN condition_2 THEN result_2
				ELSE retult_3
			END
		FROM Products

        // UNION
        // Отображает данные из двух одинаковых таблиц
        SELECT* FROM People
		UNION
		SELECT * FROM People2
				   
		SELECT Name, Note, 'Table 1' as Tbl FROM People	// Чтобы UNION отобразил данные из таблиц с разным числом столбцов,
		UNION											// нужно указать одинаковые столбцы
		SELECT Name, Note, 'Table 2' FROM People2

	#endregion

	#region ИЗМЕНЕНИЕ

        // ALTER добавляет/удаляет/измененяет столбцы и ограничения в таблице

        // Добавить столбец
        ALTER TABLE TestTable
		ADD newColumn nvarchar(20)

		// Удалить столбец
		ALTER TABLE TestTable
		DROP COLUMN newColumn

		// Изменить тип данных столбца
		ALTER TABLE TestTable
		ALTER COLUMN newColumn VARCHAR

        // Добавить столбец с ограничением
        ALTER TABLE Products
		ADD price decimal CONSTRAINT checkPrice check (price >= 0)

		// Добавить данные
		INSERT INTO Edu.dbo.People(Id, Name, Note) VALUES
			(4, 'Dude4', 'Amazing4'),
			(5, 'Dude5', 'Amazing5'),
			(6, 'Dude6', 'Amazing6');

		// Обновить данные
		UPDATE Edu.dbo.People
		SET Name = 'Dima3', Note = 'Note3'
		WHERE id = 1;

    #endregion

	#region УДАЛЕНИЕ

        DROP DATABASE [Имя БД]

		DROP TABLE [Имя таблицы]

		TRUNCATE TABLE [Имя таблицы]	// Очистить таблицу

		DELETE FROM [Имя таблицы]		// Если нет WHERE, то работает аналогично TRUNCATE, возвращает число удаленных строк

		DELETE FROM Edu.dbo.People WHERE Id = 4;

		DROP INDEX index1 ON dbo.Table1

	#endregion


	#region Индексы
		/*
             Это как оглавление в книге
             Вместо перелистываия всех страниц в поисках главы, открываем оглавление и спразу открываем нужную страницу
             Если сделать много индексов по разным столбцам, то при изменении таблицы будут изменяться все индексы, это замедлит работу БД
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

                B-дерево (B-tree):
                    - по умолчанию для большинства СУБД
                    - эффективное выполнение операциий поиска, вставки и удаления, диапазонных запросов
                    - индексация столбцов, по которым часто выполняются операции WHERE, ORDER BY, GROUP BY

                        CREATE INDEX idx_column_name ON table_name(column_name);
                        SELECT * FROM table_name WHERE column_name = 'value';

                Хэш-индекс:
                    - когда требуется быстрое выполнение точечных запросов (например, =)
                    - но не поддерживает диапазонные запросы
                    - подходит для уникальных идентификаторов или ключей

                        CREATE INDEX idx_hash_column ON table_name USING HASH (column_name);
                        SELECT * FROM table_name WHERE column_name = 'value';

                Индексы на основе полнотекстового поиска:
                    - эффективный поиск по полям с большим объемом текста
                    - эффективно ищет слова и фразы в текстовых данных

                        CREATE FULLTEXT INDEX idx_fulltext ON table_name(column_name);
                        SELECT * FROM table_name WHERE MATCH(column_name) AGAINST('search phrase');

                Партитированные индексы:
                    - при работе с большими объемами данных, которые можно разделить на логические части
                    - индексы создаются для каждой партиции данных, что улучшает производительность
                    - когда данные имеют четкую структуру, например, по дате или географическому региону

                        CREATE INDEX idx_partitioned ON table_name(column_name) PARTITION BY RANGE (column_name);
                        SELECT * FROM table_name WHERE column_name BETWEEN '2023-01-01' AND '2023-12-31';

                Индексы на основе выражений:
                    - когда нужно индексировать результат выражения или ф-ии
                    - создаются на основе вычисляемого значения
                    - ускоряют запросы, использующие ф-ии или выражения

                        CREATE INDEX idx_expression ON table_name(LOWER(column_name));
                        SELECT * FROM table_name WHERE LOWER(column_name) = 'value';

                Составные индексы:
                    - когда запросы используют несколько столбцов в условиях WHERE

                        CREATE INDEX idx_composite ON table_name(column1, column2);
                        SELECT * FROM table_name WHERE column1 = 'value1' AND column2 = 'value2';
        */
	#endregion

	#region Транзакции

		/*
			ACID (Atomicity, Consistency, Isolation, Durability)
			Св-ва, обеспечивающие надежность и целостность транзакций

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
			  гарантирует, что изменения, внесенные в рамках транзакции,
			  будут сохранены даже в случае сбоя системы
		*/

		// Начало транзакции с уровнем изоляции READ COMMITTED
		BEGIN TRANSACTION;
		SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

		UPDATE Employees SET Salary = Salary * 1.1 WHERE Department = 'IT';

		COMMIT; // Фиксация транзакции



		// Начало транзакции с уровнем изоляции SERIALIZABLE
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
		Настраиваются отдельно для каждого соединения с БД
		set session transaction isolation level [тип уровня изоляции]
	
		- Read uncommitted
			Данные еще не закоммичены в рамках одной транзакции, но можем их прочитать из другой
			Грязное чтение - незакомиченные данные лучше не трогать
			Нельзя использовать для точных вычислений, можно для примерных
		
		- Read committed
			Чтение только закоммиченных данных из другой транзакции
		
		- Repeatable read (по умолчанию)
			Исключает влияние других транзакций, но разрешает добавление новых записей
		
		- Serializable
			Полностью исключает влияние других транзакций, самый медленный
		
		- Snapshot
			Если две транзакции совершаются параллельно - каждой из них выдают свой слепок БД
			После выполнения операций со слепком (удаление/запись/изменение/чтение) изменения вливаются в основную БД
			Транзакция завершится успешно, если в основной БД к моменту окончания транзакции ни в одной из ячеек, измененных транзакцией, не было изменений
			Если две транзакции выполняли операции над разными частями БД, то конфликтов не возникнет и произойдет слияние
			Если изменялись одни и те же данные - получим аномалию
	*/
	#endregion

	#region Join

		// JOIN (INNER JOIN) - совпадения из обеих таблиц
		SELECT Name, Title
		FROM Authors a
		JOIN Books b
		ON a.Id = b.AuthorId

        // LEFT JOIN (LEFT OUTER JOIN) - все из левой и совпадения из правой
        SELECT Name, Title
		FROM Authors a
		LEFT JOIN Books b
		ON a.Id = b.AuthorId

        // RIGHT JOIN (RIGHT OUTER JOIN) - все из правой и совпадения из левой
        SELECT Name, Title
		FROM Authors a
		RIGHT JOIN Books b
		ON a.Id = b.AuthorId

        // FULL JOIN (FULL OUTER JOIN) - все записи из обеих таблиц
        SELECT Name, Title
		FROM Authors a
		FULL JOIN Books b
		ON a.Id = b.AuthorId

		// CROSS JOIN - для каждой записи из 1й таблицы подставим все записи из 2й
		SELECT Name, Title
		FROM Authors
		CROSS JOIN Books

    #endregion

    #region Процедуры

        USE [имя БД]	// после создания БД можно установить ее в качестве текущей
        GO				// разделяет отдельные наборы команд на пакеты

        BEGIN END 		// можно использовать в начале и конце хранимой процедуры


        // Вместо procedure можно писать proc
        // Если вместо create написать alter, можно изменить процедуру
        create or alter procedure spProcedure1
		as
			select * from Edu.dbo.People
		go


        // Процедуры могут принимать параметры, но не возвращают значения
        // Параметру можно задать дефолтное значение, тогда его можно не передавать
        create or alter proc spProcedure2
			@var1 int = 0
		as
			select * from dbo.People
			where Id > @var1
		go


        // Вызов процедур
        execute spProcedure1
        exec spProcedure2 @var1=1

    #endregion

	#region View

		// Представления используют для отображения данных

		CREATE OR REPLACE VIEW loyalty.report_view AS
		SELECT
			c."ID_CR", c."IssueDate",
			parent.abs_code AS Abs_Parent,
			friend."sLastName" AS Lastname_Friend
		FROM
			loyalty.card_orders AS c
			INNER JOIN loyalty.client AS parent
				ON c."GUID" = parent.giud
			LEFT JOIN loyalty.client AS friend
				ON c."ABS_Code" = friend.abs_code

			WHERE c."ID" > 2
		GO
		SELECT * FROM ShowProducts

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
            1:
            - нет повторяющихся данных в разных таблицах и в пределах одной таблицы
            - для каждого набора связанных данных есть отдельная таблица
            - каждый набор связанных данных идентифицирован с помощью первичного ключа
            - каждая ячейка таблицы должна содержать только одно неделимое значение

            2:
            - есть отдельные таблицы для наборов значений, относящихся к нескольким записям
            - эти таблицы связаны с помощью внешнего ключа
            - записи не зависят от чего-либо, кроме первичного ключа таблицы (составного ключа, если это необходимо)

            3:
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

    #region Триггеры

        // Триггер на insert в таблицу OrderDetails
        begin
            create trigger trMatchingCtocksOnInsert

            on dbo.OrderDetails for insert
			as
				// rowcount показыает, сколько строк мы собираемся вставить
				if @@ROWCOUNT = 0
					return

                // Отключаем сообщения о числе обработанных записей, увеличивая производительность
                set nocount on

                // inserted - служебная таблица с вставляемыми данными
                update Stocks set Qty = s.Qty - i.Qty

                from Stocks s join (select ProductID, sum(Qty) Qty from inserted group by ProductID) i
                on s.ProductID = i.ProductID

        // Триггер на delete
        create trigger trMatchingStocksOnDelete

        on OrderDetails for delete
		as
			if @@ROWCOUNT = 0
				return
			set nocount on

            // Служебная таблица deleted хранит записи, которые будут удалены
            update Stocks set Qty = s.Qty + d.Qty

            from Stocks s join(select ProductID, sum(Qty) Qty from deleted group by ProductID) d
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
        join(select ProductID, sum(Qty) Qty from deleted group by ProductID) d
             on s.ProductID = d.ProductID
        join(select ProductID, sum(Qty) Qty from inserted group by ProductID) i
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
		if exists(select 1 from inserted) and exists(select 1 from deleted)

            begin
				if not UPDATE(Qty)
					return
				update..end
		// Если в inserted нет записей, а в deleted есть, то происходит удаление
		else if not exists(select 1 from inserted) and exists(select 1 from deleted)

            begin
                delete..
            end
		// Если в inserted есть записи, а в deleted нет, то происходит insert
		else if exists (select 1 from inserted) and not exists(select 1 from deleted)

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

    #region Оконная функция
    /*
     

		// Внутри функций нельзя выполнять транзакции, только в хранимых процедурах
		// Функции должны быть детерминированными и не могут изменять состояние БД

		// Объявление ф-ии с указанием типа возвращаемого значения
        create function fnFunc1 (@var1 int)
		   returns int
		as
			begin
			declare @var2 int;
		set @var2 = @var1 * 2;
		return @var2;

		// Вызов ф-ии
		print dbo.fnFunc1 (2)




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