public class Program
{
    #region Что будет выведено в консоль 1

    public record A(int Value, string Value2);

    static void Main_(string[] args)
    {
        // Здесь оба числа 1 приводятся к типу object, и сравниваются как ссылки
        // Поскольку это два разных объекта, результат будет false
        bool a = (object)1 == (object)1;

        // Метод Equals для значения 1 сравнивает значения, а не ссылки
        // Поскольку оба значения равны, результат будет true
        bool b = 1.Equals(1);

        // Аналогично первому случаю, здесь строки "1" также приводятся к типу object
        // Хотя строки содержат одинаковые символы, это два разных объекта в памяти, поэтому результат будет false
        bool c = (object)"1" == (object)"1";

        // Для record метод Equals переопределен так, что сравнивает значения всех свойств, а не ссылки
        // Поскольку оба экземпляра A имеют одинаковые значения всех свойств, результат будет true
        bool d = new A(1, "TEST").Equals(new A(1, "TEST"));
    }

    #endregion

    #region Что будет выведено в консоль 2

    public static int Increment(int a)
    {
        var i = a + 1;

        return i;
    }

    static void Main_()
    {
        var init = 100;
        Console.WriteLine("{0}, {1}", init++, Increment(++init));
    }

    /*
    
        1. Инициализация переменной init:
   
           var init = 100;
   
        2. Вызов Console.WriteLine:
   
           Console.WriteLine("{0}, {1}", init++, Increment(++init));
   
           • init++ — это постфиксный инкремент
             Сначала будет использовано текущее значение init (то есть 100), а затем init увеличится на 1
             После этого вызова init станет равным 101
   
           • Increment(++init)
             Здесь происходит сначала префиксный инкремент ++init, который увеличивает init на 1, так что init теперь становится 102
             Затем это значение передается в метод Increment.

        3. Вызов метода Increment:
   
           int Increment(int a)
           {
               var i = a + 1;
               return i;
           }
   
           В методе Increment, значение a будет равно 102 (так как мы передали ++init)
           Метод добавляет 1 к этому значению, и возвращает 103.

        4. Форматированный вывод:
           Когда мы вызываем Console.WriteLine, у нас есть:

           • {0} будет заменено на 100 (значение до инкремента),
           • {1} будет заменено на 103 (результат работы метода Increment)

        Так, в консоль будет выведено: 100, 103

    */

    #endregion

    # region Что будет выведено в консоль 3

    static void Main__()
    {
        var i = 0;

        var baselist = new List<int> { 1, 2, 3, 4, 5 };

        var test = baselist.Where(t => t > 2).Select(_ => i++);

        var number = test.First();

        Console.WriteLine($"i = {i}, number = {number}");
    }

    /*
    
        Давайте разберем этот код шаг за шагом:

        1. Инициализация переменной i
        2. Создание списка baselist
        3. Фильтрация и выборка с помощью LINQ:
   
           var test = baselist.Where(t => t > 2).Select(_ => i++);   

           • baselist.Where(t => t > 2) создает последовательность, содержащую только элементы, которые больше 2
             В результате мы получаем последовательность { 3, 4, 5 }

           • с помощью Select(_ => i++) мы создаем новую последовательность,
             где для каждого элемента из предыдущей последовательности будет возвращено текущее значение i, а затем i будет увеличено на 1

        4. Получение первого элемента:
   
           var number = test.First();
   
           Здесь вызывается метод First() на последовательности test
           Этот метод выполняет итерацию по элементам последовательности и возвращает первый элемент

           • На первой итерации

             • i равно 0, поэтому number будет равно 0, и затем i увеличивается на 1, так что теперь i равно 1

        5. Вывод в консоль:
   
           Console.WriteLine($"i = {i}, number = {number}");
   
        i = 1
        number = 0
     
    */

    #endregion

    #region При вызове throw ex

    public static void Test()
    {
        throw new NotImplementedException();
    }

    static void Main___()
    {
        try
        {
            Test();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    // A) StackTrace будет дополнен информацией из ex
    // B) StackTrace первоначального исключения будет потерян
    // C) StackTrace не изменится

    /*
    
        При вызове throw ex произойдет потеря информации о первоначальном исключении
    
        При использовании throw ex, мы повторно выбрасываем исключение, но с использованием переменной ex,
        что означает, что стек вызовов (stack trace) будет указывать на место, где было выполнено повторное выбрасывание исключения,
        а не на место, где оно было первоначально выброшено

        B) StackTrace первоначального исключения будет потерян

        Если использовать просто throw без указания переменной,
        стек вызовов сохранил бы полную информацию о первоначальном исключении

    */

    #endregion

    #region Когда будет выведено сообщение "Clean up"

    class TestGC : IDisposable
    {
        ~TestGC()
        {
            Console.WriteLine("Clean up");
        }

        public void Dispose()
        {
        }
    }

    static void Main_2()
    {
        // В каком-то методе его вызывают:
        using (var test = new TestGC())
        {
        }

        GC.Collect();
    }

    // A) Сразу после конструкции using
    // B) После GC.Collect()
    // C) Точный момент не определен

    /*
    
        При использовании класса TestGC, который реализует финализатор (метод ~TestGC()),
        сообщение "Clean up" будет выведено в момент, когда сборщик мусора (Garbage Collector, GC) решит вызвать финализатор для объекта TestGC

        Теперь разберем варианты:

        A) Сразу после конструкции using
           Неверно, т.к. финализатор не вызывается сразу после выхода из блока using
           В этом блоке вызывается метод Dispose(), но он не вызывает финализатор

        B) После GC.Collect()
           Это может быть верно, но не гарантировано
           Вызов GC.Collect() инициирует сборку мусора, но точный момент вызова финализатора зависит от реализации сборщика мусора и его внутренней логики

        C) Точный момент не определен:
           Это правильный ответ
           Вызов финализатора происходит в неопределенный момент после того, как объект становится недоступным для использования,
           и хотя GC.Collect() может инициировать сборку мусора, она не гарантирует немедленный вызов финализатора
     
    */

    #endregion

    # region Что будет выведено в консоль 4

    static async Task TestThread1()
    {
        var startThreadId = Environment.CurrentManagedThreadId;

        var test = await Task.FromResult("TEST 1");

        var endThreadId = Environment.CurrentManagedThreadId;

        Console.WriteLine("{0} => {1}", test, startThreadId == endThreadId);
    }

    // A) TEST 1 => True
    // B) TEST 1 => False
    // C) Будет ошибка

    /*
     
        В данном коде используется асинхронный метод TestThread1, который выполняет задачу с помощью await Task.FromResult("TEST 1"). 

        Разберем код:

        1. Получение идентификатора потока:
           var startThreadId = Environment.CurrentManagedThreadId
    
        2. Асинхронное ожидание:
           var test = await Task.FromResult("TEST 1");
           Этот вызов не блокирует поток, а возвращает управление вызывающему коду
           Поскольку Task.FromResult завершает задачу синхронно, выполнение продолжится в том же потоке после завершения задачи

        3. Получение идентификатора потока после ожидания:
           var endThreadId = Environment.CurrentManagedThreadId;

        Так как Task.FromResult завершает задачу немедленно и выполнение продолжится в том же потоке,
        идентификаторы потоков (startThreadId и endThreadId) будут одинаковыми

        A) TEST 1 => True
     
    */

    #endregion

    # region В консоль будет выведено

    static async Task TestThread2()
    {
        var startThreadId = Environment.CurrentManagedThreadId;

        var test = await Task.Run(() => "TEST 2");

        var endThreadId = Environment.CurrentManagedThreadId;

        Console.WriteLine("{0} => {1}", test, startThreadId == endThreadId);
    }

    // A) TEST 1 => True
    // B) TEST 1 => False
    // C) Может как TEST 1 => False, так и TEST 1 => True

    /*

        Разберем код:

        1. Получение идентификатора потока:
           var startThreadId = Environment.CurrentManagedThreadId;

        2. Асинхронное выполнение:
           var test = await Task.Run(() => "TEST 2");
           Этот вызов создает новую задачу, которая выполняется в пуле потоков
           Выполнение может перейти на другой поток, и когда задача завершится, управление вернется к текущему контексту

        3. Получение идентификатора потока после ожидания:
           var endThreadId = Environment.CurrentManagedThreadId;

        Поскольку Task.Run создает новую задачу, которая может выполняться в другом потоке,
        скорее всего, идентификаторы потоков (startThreadId и endThreadId) будут различаться

        Таким образом, вывод будет:

        B) TEST 2 => False.

    */

    # endregion

    # region Что верно для Task.WaitAll(): 

    // А) Асинхронно ожидает завершения всех задач
    // B) Синхронно блокирует поток до завершения всех задач
    // C) Асинхронно ожидает завершения первой задачи

    /*
    
        Правильный ответ:

        B) Синхронно блокирует поток до завершения всех задач.

        Task.WaitAll() — это метод, который блокирует вызывающий поток, пока все переданные задачи не завершатся
        Task.WhenAll() - асинхронный метод, позволяют избежать блокировки потока, ожидая завершения всех задач асинхронно
     
    */

    # endregion

    # region Какие из этих утверждений верны для System.String

    // A) Это ссылочный тип
    // B) Это значимый тип
    // C) Это неизменяемый тип
    // D) "Внутри" используется StringBuilder

    /*
    
        Правильные ответы:

        A) Это ссылочный тип  
        C) Это неизменяемый тип

    */

    # endregion

    # region В данном примере фильтрация товаров будет выполнена:

    class DbContextConfiguration { }
    
    class DbContext
    {
        public List<Product> Products { get; set; }

        public DbContext(DbContextConfiguration conf) { }
    }

    class Product
    {
        public double Cost { get; set; }
    }

    static void Main_1()
    {
        var _dbConfiguration = new DbContextConfiguration();
        var _dbContext = new DbContext(_dbConfiguration);

        IEnumerable<Product> products = _dbContext.Products.Where(p => p.Cost >= 25.00);
    }

    // А) На стороне приложения
    // В) На стороне БД
    // С) Зависит от количества записей в БД

    /*
    
        Фильтрация товаров в данном примере будет выполнена:

        В) На стороне БД

        Когда вы используете метод Where в LINQ с IQueryable (который обычно возвращается из контекста БД, как в случае с _dbContext.Products),
        запрос будет преобразован в SQL и выполнен на стороне БД
    
        Это позволяет оптимизировать производительность, так как БД может фильтровать данные, прежде чем они будут загружены в приложение

        Если бы вы использовали IEnumerable, тогда фильтрация происходила бы на стороне приложения, но в этом случае, поскольку вы работаете с IQueryable,
        фильтрация выполняется на стороне БД
     
    */

    # endregion

    # region Насколько безопасно использовать эту функцию

    //private async Task<Client> FindClientByDealIdAsync(string dealId)
    //{
    //    using var connection = _connectionFactory.Create();

    //    var selectCommandText = $@"SELECT c.FirstName, c.SecondName, c.*
    //                               FROM clients as c   
    //                               INNER JOIN client_deal as cd on cd.ClientId = c.Id   
    //                               WHERE cd.DealId = {dealId}";

    //    var selectSqlCommand = new SqlCommand(selectCommandText, connection);

    //    var result = await connection.ExecuteAsync(readQuery);
    //    return result;
    //}

    // А) Проблем нет
    // В) Данный код имеет уязвимость
    // С) Выполнение данной функции безопасно только в изолированном docker контейнере

    /*
    
        В) Данный код имеет уязвимость

        Код имеет уязвимость к SQL-инъекциям, потому что значение dealId вставляется непосредственно
        в строку SQL-запроса без какой-либо обработки или параметризации
        Для защиты от SQL-инъекций следует использовать параметризованные запросы
        Вот пример, как это можно сделать:

            private async Task<Client> FindClientByDealIdAsync_Correct(string dealId)
            {
                using var connection = _connectionFactory.Create();

                var selectCommandText = @"SELECT c.FirstName, c.SecondName, c.*
                                          FROM clients as c   
                                          INNER JOIN client_deal as cd on cd.ClientId = c.Id   
                                          WHERE cd.DealId = @DealId";

                var selectSqlCommand = new SqlCommand(selectCommandText, connection);

                selectSqlCommand.Parameters.AddWithValue("@DealId", dealId);

                var result = await connection.ExecuteAsync(selectSqlCommand);
                return result;
            }
     
    */

    #endregion

    #region Интерфейс IQueryable включает в себя:

    // А) Type ElementType
    // В) Expression Expression
    // С) T Current
    // D) bool MoveNext()

    /*
     
        Интерфейс IQueryable включает в себя следующие члены:
        A) Type ElementType
           Да

        B) Expression Expression
           Да

        C) T Current
           Нет
           Это свойство обычно присутствует в интерфейсе IEnumerator

        D) bool MoveNext()
           Нет
           Это метод, который присутствует в интерфейсе IEnumerator
     
    */

    #endregion

    #region Можно ли внутри SQL функции выполнить транзакцию?

    // А) Да
    // В) Нет
    // С) Да, но только с уровнем изоляции транзакции READ UNCOMMITTED

    /*
 
        Правильный ответ: В) Нет

        В SQL Server нельзя выполнять транзакции внутри пользовательских функций, а именно команды 
        - BEGIN TRANSACTION
        - COMMIT
        - ROLLBACK

        Функции в SQL Server должны быть детерминированными и не могут изменять состояние БД
        Транзакции могут быть использованы в хранимых процедурах, но не в функциях

    */

    #endregion

    #region Сколько можно создать кластеризованных индексов для таблицы:

    // A) Количество не ограничено
    // B) Для MS SQL Server ограничение 256
    // C) Определяется настройками базы данных
    // D) Нет правильного ответа

    /*
    
        Правильный ответ: D) Нет правильного ответа

        В SQL Server можно создать только один кластеризованный индекс для таблицы
        Кластеризованный индекс определяет физический порядок хранения строк в таблице, поэтому только один такой порядок может быть установлен
     
    */

    # endregion

    # region Запрос вернет: 

    /*
        create table authors(
            author_id int primary key,
            first_name text,
            last_name text,
            index decimal);

        insert into authors values
            (1, /Александр/, /Пушкин/, 20000),
            (2, /Сергей/, /Есенин/, 10000),
            (3, /Федор/, /Тютчев/, 20000),
            (4, /Максим/, /Горький/, 10000);

        select first_name, last_name, index
        from authors
        where index > avg(index)

    */

    // А) 2 записи
    // В) 4 записи
    // С) Будет ошибка

    /*
    
        Запрос, который вы привели, имеет несколько проблем, которые могут привести к ошибке

        1. Использование функции AVG:
           В SQL для получения среднего значения необходимо использовать подзапрос или оконную функцию
           В текущем виде avg(index) не будет работать, потому что SQL не знает, как вычислить среднее значение в контексте запроса

        2. Некорректные символы для строк:
           В SQL для строковых значений используются одинарные кавычки ('), а не косые (/)
           
        Чтобы исправить запрос, можно использовать подзапрос для вычисления среднего значения:

        select first_name, last_name, index
        from authors
        where index > (select avg(index) from authors);
     
    */

    # endregion

    # region В таблице authors 10 строк. Запрос вернет:

    /*
        select first_name, last_name, index
        from authors
        where NULL = NULL
    */

    // А) Ни одной записи
    // В) Все записи
    // С) Будет ошибка

    /*

        Запрос, имеет условие NULL = NULL
        В SQL выражение, сравнивающее NULL с NULL, всегда возвращает FALSE, потому что NULL представляет собой неопределенное значение,
        и SQL не считает его равным самому себе

        А) Ни одной записи

        В SQL для проверки на NULL используется оператор IS NULL, например:

        where column_name IS NULL

    */

    # endregion

    # region SQL
    // В таблице authors есть 100 строк, есть индекс по колонке first_name (селективность 10%),
    // все колонки таблицы определены как не уникальные
    // Сколько строк просканирует СУБД при обработке запроса?

    /*

        create table authors(
            author_id int primary key,
            first_name text,
            last_name text,
            index decimal);
        CREATE INDEX idx_authors_first_name ON authors(first_name);

        insert into authors
        values
            (1, /Александр/, /Пушкин/, 20000),
            (2, /Сергей/, /Есенин/, 10000),
            (3, /Федор/, /Тютчев/, 25000),
            (4, /Максим/, /Горький/, 40000),
            (5, /Михаил/, /Булгаков/, 12000),
            -- еще 9990 записей...
            ;

        select first_name, last_name, index
        from authors
        where first_name = / Иван /;

    */

    // А) 0 
    // В) 5
    // С) 100
    // D) Точное количество не определено, но не более 100

    /*
     
        В данном случае у нас есть таблица authors с 100 строками и индексом по колонке first_name, который имеет селективность 10%
        Селективность 10% означает, что примерно 10% записей в таблице имеют одинаковое значение в колонке first_name

        При выполнении запроса:

        select
            first_name,
            last_name,
            index
        from authors
        where first_name = 'Иван';

        СУБД будет использовать индекс для поиска строк, соответствующих условию
        Однако поскольку нет информации, сколько строк с именем "Иван" существует в таблице,
        можем предположить, что таких строк может быть от 0 до 10 (поскольку 10% от 100 — это 10)

        Если строки с именем "Иван" отсутствуют, то СУБД просканирует 0 строк
        Если такие строки существуют - для их нахождения потребуется просканировать все строки с одинаковым значением в first_name, что может составлять до 10 строк

        Таким образом, точное количество строк, которое будет просканировано, не определено, но оно не превысит 100

        D) Точное количество не определено, но не более 100
     
    */

    # endregion

    # region SQL 2

    // В БД порядка 100000 записей
    // Как оптимизировать скорость выполнения этого кода, чтобы выполнение занимало не более 2 секунд: 

    class Repository
    {
        private DbContext _context; // Контекст EF Core 6.0

        public Repository(DbContext context)
        {
            _context = context;
        }

        //public async Task UpdateAsync()
        //{
        //    var requests = await _context.Requests.ToListAsync();   // 300 sec

        //    requests.ForEach(_ => _.status = "New");                // 0.02 sec

        //    await _context.SaveChangesAsync();                      // 400 sec
        //}
    }

    // А) Разбить Requests на блоки по 1000 элементов и обрабатывать их параллельно
    // В) Добавить индекс для поля status
    // С) Сделать метод синхронным
    // D) Нет правильного ответа

    /*
    
        А) Разбить Requests на блоки по 1000 элементов и обрабатывать их параллельно
           Это позволит уменьшить объем данных, обрабатываемых за один раз,
           и использовать параллельные операции для обновления статусов,
           что может значительно сократить время выполнения

        В) Добавление индекса для поля status может помочь в других запросах к базе данных,
           но не сильно повлияет на скорость выполнения текущего обновления, так как вы обновляете все записи

        С) Синхронизация метода не имеет смысла в данном контексте

        Таким образом, правильный ответ — А
     
    */

    # endregion

    # region Какие методы REST неидемпотентны?

    // А) Post
    // В) Delete
    // С) Patch
    // D) Put

    /*
    
        Неидемпотентные методы могут приводить к различным результатам при многократном выполнении одного и того же запроса

        Из предложенных вариантов:

        А) POST — неидемпотентный метод
           Каждый раз, когда вы отправляете POST-запрос, создается новый ресурс, и повторный запрос может привести к созданию дополнительных ресурсов

        В) DELETE — идемпотентный метод
           Если вы удаляете ресурс несколько раз, результат будет одинаковым: ресурс будет удален, и последующие запросы не изменят состояние

        С) PATCH — неидемпотентный метод
           В зависимости от изменения, которое вы вносите, повторный PATCH-запрос может привести к различным состояниям ресурса

        D) PUT — идемпотентный метод
           Если вы отправляете один и тот же PUT-запрос несколько раз, ресурс будет оставаться в одном и том же состоянии после первого запроса

        Таким образом, неидемпотентными методами являются:

        • А) POST
        • С) PATCH 
     
    */

    # endregion

    # region Внешний сервис работает нестабильно и периодически возвращает HttpCode 500. Какой паттерн поможет решить проблему?

    // А) Saga
    // В) Circuit breaker
    // С) Strangler

    /*
    
        A) Saga
           Паттерн используется для управления распределенными транзакциями
           и не решает проблему с нестабильностью внешнего сервиса

        B) Circuit Breaker
           Паттерн предназначен для предотвращения повторных попыток обращения к нестабильному или недоступному сервису
           Когда сервис возвращает ошибку (например, HTTP 500), "прерыватель цепи" блокирует дальнейшие запросы на некоторое время,
           позволяя сервису восстановиться - это помогает избежать перегрузки сервиса

        C) Strangler
           Паттерн используется для постепенного замещения устаревших систем новыми,
           но также не предназначен для решения проблем с доступностью внешних сервисов

        Правильный ответ — В) Circuit breaker
     
    */

    # endregion

    # region В данном примере FrozenDeposit:

    public abstract class Deposit
    {
        public decimal Balance { get; protected set; }

        public virtual void Credit(decimal amount)
        {
            Balance += amount;
        }
    }

    public class FrozenDeposit : Deposit
    {
        public override void Credit(decimal amount)
        {
            //throw new DomainException("This deposit does not support filling up");
        }
    }

    // А) Нарушает Принцип единственной ответственности
    // В) Нарушает Принцип подстановки Лисков
    // С) Нарушает Принцип разделения интерфейса
    // D) Написан корректно

    /*
    
        Класс FrozenDeposit нарушает Принцип подстановки Лисков

        Объекты подкласса должны быть заменяемыми объектами базового класса без изменения желаемых свойств программы
        В данном случае, класс FrozenDeposit переопределяет метод Credit, чтобы выбрасывать исключение, что нарушает ожидания пользователя базового класса Deposit
        Если есть объект типа Deposit, вы ожидаете, что сможете вызвать метод Credit и добавить средства к депозиту
        Однако для FrozenDeposit это не так, и это может привести к неожиданным ошибкам в коде, который использует объекты типа Deposit

        Правильный ответ — В) Нарушает Принцип подстановки Лисков.
     
    */

    #endregion

    #region Какой шаблон проектирования используется для динамического добавления новых функций к объекту без изменения его структуры?

    // А) Абстрактная фабрика
    // В) Шаблонный метод
    // С) Стратегия
    // D) Декоратор

    /*
     
        Правильный ответ — D) Декоратор

        Шаблон проектирования "Декоратор" позволяет динамически добавлять новые функции к объекту, оборачивая его в другой объект-декоратор
        Это позволяет расширять функциональность объектов без изменения их структуры, что делает декоратор очень гибким и удобным инструментом для добавления новых возможностей
     
    */

    // ################## Вопрос 26.

    /*
    
        Даны таблицы:
        Clients                         -- клиенты
        (
            Id bigint,                  -- Id клиента
            ClientName nvarchar(200)    -- Наименование клиента
        );

        ClientContacts                  -- контакты клиентов
        (
            Id bigint,                  -- Id контакта
            ClientId bigint,            -- Id клиента
            ContactType nvarchar(255),  -- тип контакта
            ContactValue nvarchar(255)  -- значение контакта
        );

        Написать запросы, которые возвращают:
        - наименование клиентов и кол-во контактов клиентов
        - список клиентов, у которых есть более 2 контактов
     
    */

    /*

        1. Запрос, который возвращает наименование клиентов и количество контактов клиентов:

            SELECT 
                c.ClientName,
                COUNT(cc.Id) AS ContactCount
            FROM 
                Clients c
            LEFT JOIN 
                ClientContacts cc ON c.Id = cc.ClientId
            GROUP BY 
                c.ClientName;

            Используем LEFT JOIN, чтобы получить всех клиентов, даже если у них нет контактов
            COUNT(cc.Id) считает количество контактов для каждого клиента
            Группируем результат по имени клиента

        2. Запрос, который возвращает список клиентов, у которых есть более 2 контактов:

            SELECT 
                c.ClientName
            FROM 
                Clients c
            JOIN 
                ClientContacts cc ON c.Id = cc.ClientId
            GROUP BY 
                c.ClientName
            HAVING 
                COUNT(cc.Id) > 2;

        Используем JOIN, чтобы получить только тех клиентов, у которых есть хотя бы один контакт
        Затем группируем по имени клиента и используем HAVING для фильтрации клиентов с количеством контактов больше 2
     
    */

    // ################## Вопрос 27.

    /*
    
        Дана таблица:
        Dates - клиенты
        (
                  Id bigint,
                  Dt date
        );

        Написать запрос, который возвращает интервалы для одинаковых Id
        Например, есть такой набор данных:

        Id - первая колонка, Dt - вторая
        1, 01.01.2021
        1, 10.01.2021
        1, 30.01.2021
        2, 15.01.2021
        2, 30.01.2021

        Результатом выполнения запроса должен быть такой набор данных:
        Id - первая колонка, Sd - вторая, Ed - третья

        1, 01.01.2021, 10.01.2021
        1, 10.01.2021, 30.01.2021
        2, 15.01.2021, 30.01.2021
     
    */

    /*
    
        Для получения интервалов дат для одинаковых Id в таблице Dates, можно использовать оконные функции и подзапросы
        Вот SQL-запрос, который решает вашу задачу:

        WITH RankedDates AS (
            SELECT 
                Id,
                Dt,
                LEAD(Dt) OVER (PARTITION BY Id ORDER BY Dt) AS NextDt
            FROM 
                Dates
        )
        SELECT 
            Id,
            Dt AS Sd,
            NextDt AS Ed
        FROM 
            RankedDates
        WHERE 
            NextDt IS NOT NULL;

        1. CTE (Common Table Expression)
           Создаем временную таблицу RankedDates, где для каждого Id и даты Dt добавляем следующую дату NextDt с помощью функции LEAD()
           Эта функция позволяет получить значение следующей строки в пределах одной группы (группы по Id), упорядоченной по дате Dt
  
        2. Основной запрос: Из CTE выбираем Id, текущую дату как Sd и следующую дату как Ed
           Мы фильтруем результаты, чтобы исключить строки, где NextDt равно NULL, т.е. чтобы не включать последнюю дату для каждого Id, у которой нет следующей даты
     
    */

    #endregion

    #region

        static void Main_3()
        {
            // 1. Первый блок
            var i = 0;
            var baselist = new List<int> { 1, 2, 3, 4, 5 };

            var test1 = baselist.Where(t => t > 2);

            var test2 = test1.Select(_ => i++);

            Console.WriteLine($"i = {i}");

            // 2. Второй блок
            var number = test2.First();

            Console.WriteLine($"i = {i}, number = {number}");


            // 3. Третий блок
            var result = test2.ToList();

            Console.WriteLine($"i = {i}");
        }

    #endregion

    #region

        static async Task Main_4(string[] args)
        {
            var items = Enumerable.Range(0, 10);

            // CustomSelect определен ниже
            foreach (var item in items.CustomSelect(p => p * p))
            {
                Console.WriteLine("item = {0}", item);
            }
        }

    #endregion

    #region

        static void Main_5()
        {
            var cancellationToken = CancellationToken.None;
            var first = GetFirstAsync(cancellationToken);     // 
            var second = GetSecondAsync(cancellationToken);   // 
            var third = GetThirdAsync(cancellationToken);     // 

            Console.WriteLine($"first={first.Result}, second={second.Result}, third={third.Result}");
        }

        static async Task<string> GetFirstAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("1");
            var result = await Task.Run(
                () => {
                    Task.Delay(1000).Wait();
                    return "First";
                }, cancellationToken);
            Console.WriteLine("2");
            return result;
        }

        static async Task<string> GetSecondAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("3");
            var result = await Task.Run(
                () => {
                    Task.Delay(2000).Wait();
                    return "Second";
                }, cancellationToken);
            Console.WriteLine("4");
            return result;
        }

        static async Task<string> GetThirdAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("5");
            var result = await Task.Run(
                () => {
                    Task.Delay(3000).Wait();
                    return "Third";
                }, cancellationToken);
            Console.WriteLine("6");
            return result;
        }

    #endregion

    #region Получение данных с веб-сайта через HttpClient

    static async Task Main_4()
    {
        HttpClient httpClient = new HttpClient();

        HttpRequestMessage message = new HttpRequestMessage()
        {
            RequestUri = new Uri("https://skillaz.ru/")
        };

        HttpResponseMessage result = httpClient.Send(message);

        var data = await result.Content.ReadAsStringAsync();
    }

    #endregion

    #region Иерархия

    /*
    
        Есть иерархия
        public class TransportVehicle  { }
        public class Car : TransportVehicle  { }
        public class PublicTransport  :  TransportVehicle { }
        public class Bus : PublicTransport  { }
         public class Tram  :  PublicTransport { }

        Какие из следующих примеров кода при запуске приведут к выбросу исключения?

        // 1
        object transport = new Car();
        var vehicle = (TransportVehicle)transport;

        // 2
        object transport = new Bus();
        var publicTransport = (PublicTransport)transport;

        // 3
        object transport = new Tram();
        var transportVehicle = (TransportVehicle)transport;

        // 4
        object transport = new Car();
        var bus = (Bus)transport;

        // 5
        object transport = new PublicTransport();
        var tram = (Tram)transport;

        // 6
        object transport = new TransportVehicle();
        var car = (Car)transport;
     
    */

    /*
     
        Давайте проанализируем каждый из приведенных примеров кода, чтобы определить, приведут ли они к выбросу исключения при выполнении.

        1. 
        object transport = new Car();
        var vehicle = (TransportVehicle)transport;

        Результат: Не приведет к исключению. Car является подклассом TransportVehicle, и приведение к базовому типу безопасно.

        2. 
        object transport = new Bus();
        var publicTransport = (PublicTransport)transport;

        Результат: Не приведет к исключению. Bus является подклассом PublicTransport, и приведение к базовому типу безопасно.

        3. 
        object transport = new Tram();
        var transportVehicle = (TransportVehicle)transport;

        Результат: Не приведет к исключению. Tram является подклассом PublicTransport, который в свою очередь является подклассом TransportVehicle, так что приведение к базовому типу безопасно.

        4. 
        object transport = new Car();
        var bus = (Bus)transport;

        Результат: Приведет к исключению. Car не является подклассом Bus, поэтому попытка привести объект типа Car к типу Bus вызовет InvalidCastException.

        5. 
        object transport = new PublicTransport();
        var tram = (Tram)transport;

        Результат: Приведет к исключению. PublicTransport не является подклассом Tram, поэтому попытка привести объект типа PublicTransport к типу Tram вызовет InvalidCastException.

        6. 
        object transport = new TransportVehicle();
        var car = (Car)transport;

        Результат: Приведет к исключению. TransportVehicle не является подклассом Car, поэтому попытка привести объект типа TransportVehicle к типу Car вызовет InvalidCastException.

        ▎Вывод:

        Исключения будут выброшены в следующих случаях:

        • Пример 4

        • Пример 5

        • Пример 6

        Таким образом, исключения произойдут в примерах 4, 5 и 6. 
     
    */

    #endregion

    #region Выберите все верные утверждения

    class A2
    {

        public static IList<int> F1(int n, List<int> list = null)
        {
            if (list == null)
                list = new List<int>();

            if (n <= 0)
                return list;

            list.Add(n);

            return F1(n - 2, list);
        }

        public static void Main()
        {
            var result = F1(5);
            Console.WriteLine(string.Join(", ", result));
        }
    }

    //Выберите все верные утверждения

    //1 RecursiveCollect будет вызвана 4 раза
    //2 За время работы приложения будет создан 1 экземпляр класса List<int>
    //3    Программа завершится с ошибкой StackOverflowException
    //4    Программа выведет на экран 5, 3, 1
    //5    Программа выведет на экран 5, 3, 1, -1
    //6    Программа выведет на экран 1, 3, 5
    //7    Программа выведет на экран -1, 1, 3, 5

    /*
     
        Давайте проанализируем представленный код и каждое из утверждений.

        ▎Код:

        class A
        {
            public static IList<int> F1(int n, List<int> list = null)
            {
                if (list == null)
                    list = new List<int>();

                if (n <= 0)
                    return list;

                list.Add(n);

                return F1(n - 2, list);
            }

            public static void Main()
            {
                var result = F1(5);
                Console.WriteLine(string.Join(", ", result));
            }
        }


        ▎Анализ работы функции F1:

        • При первом вызове F1(5):

          • list будет инициализирован новым экземпляром List<int>.

          • 5 добавляется в list.

          • Рекурсивный вызов F1(3, list).

        • При втором вызове F1(3):

          • 3 добавляется в list.

          • Рекурсивный вызов F1(1, list).

        • При третьем вызове F1(1):

          • 1 добавляется в list.

          • Рекурсивный вызов F1(-1, list).

        • При четвертом вызове F1(-1):

          • Поскольку n <= 0, функция вернет текущий list.

        ▎Содержимое списка после выполнения:

        После завершения всех вызовов, содержимое списка будет: [5, 3, 1].

        ▎Проверка утверждений:

        1. RecursiveCollect будет вызвана 4 раза

           • Верно. Функция F1 вызывается 4 раза (для 5, 3, 1 и -1).

        2. За время работы приложения будет создан 1 экземпляр класса List<int>

           • Верно. Новый экземпляр List<int> создается только один раз при первом вызове функции.

        3. Программа завершится с ошибкой StackOverflowException

           • Неверно. Программа корректно завершится, так как рекурсия имеет базовый случай.

        4. Программа выведет на экран 5, 3, 1

           • Верно. Это именно те значения, которые будут в списке, и они будут выведены в указанном порядке.

        5. Программа выведет на экран 5, 3, 1, -1

           • Неверно. Значение -1 не добавляется в список и не будет выведено.

        6. Программа выведет на экран 1, 3, 5

           • Неверно. Порядок вывода будет именно 5, 3, 1.

        7. Программа выведет на экран -1, 1, 3, 5

           • Неверно. Значение -1 не будет выведено, и порядок также неверен.

        ▎Итог:

        Верные утверждения:

        • 1

        • 2

        • 4 
     
    */

    #endregion

    #region Иерархия

        public class Animal
        {
            public virtual void Speak()
            {
                Console.WriteLine("Speak from Animal");
            }
        }

        public class Mammal : Animal
        {
            public override void Speak()
            {
                Console.WriteLine("Speak from Mammal");
            }
        }

        public class Dog : Mammal
        {
            public override void Speak()
            {
                base.Speak();
                Console.WriteLine("Speak from Dog");
            }
        }

        public class Cat : Mammal
        {
        }

        public class Fish : Animal
        {
            public override void Speak()
            {
                base.Speak();
                Console.WriteLine("Speak from Fish");
            }
        }

        public class B
        {
            public static void Main()
            {
                var animals = new Animal[]
                {
                new Dog(),
                new Cat(),
                new Fish()
                };

                foreach (var animal in animals)
                {
                    animal.Speak();
                }
            }
        }

        //Какие утверждения правильные

        //1 На экран будет выведено 3 строки
        //2 Все строковые литералы, определенные в коде, будут выведены на экран
        //3 Одна и та же строка будет выведена на экран несколько раз
        //4 Возможно внести изменение в класс Cat, чтобы вывод на экран изменился
        //5 Возможно внести изменение в класс Animal, чтобы вывод на экран изменился
        //6 Если класс Animal и метод Speak в классе Animal сделать абстрактными, код продолжит компилироваться, а вывод не изменится
        //7 Метод Speak в классе Fish можно пометить sealed, код продолжит компилироваться, а вывод не изменится
        //8 Метод Speak в классе Mammal можно пометить sealed, код продолжит компилироваться, а вывод не изменится

    /*
    
        При выполнении программы, будет создан массив animals, содержащий три объекта: Dog, Cat и Fish. В цикле каждый объект вызовет метод Speak():

        1. Dog:

           • Выведет "Speak from Mammal" (из метода Mammal.Speak(), который переопределен в Dog) и затем "Speak from Dog".

   
        2. Cat:

           • Поскольку метод Speak() не переопределен в классе Cat, будет вызван метод из класса Mammal, который выведет "Speak from Mammal".

        3. Fish:

           • Вызовет метод Speak() из класса Fish, который сначала вызовет метод из класса Animal и выведет "Speak from Animal", а затем "Speak from Fish".

        Таким образом, вывод будет следующим:
        Speak from Mammal
        Speak from Dog
        Speak from Mammal
        Speak from Animal
        Speak from Fish


        ▎Проверка утверждений:

        1. На экран будет выведено 3 строки

           • Неверно. Будет выведено 5 строк.

        2. Все строковые литералы, определенные в коде, будут выведены на экран

           • Верно. Все строки ("Speak from Animal", "Speak from Mammal", "Speak from Dog", "Speak from Fish") будут выведены.

        3. Одна и та же строка будет выведена на экран несколько раз

           • Верно. Строка "Speak from Mammal" будет выведена дважды (для Dog и Cat).

        4. Возможно внести изменение в класс Cat, чтобы вывод на экран изменился

           • Верно. Можно переопределить метод Speak() в классе Cat, чтобы изменить вывод.

        5. Возможно внести изменение в класс Animal, чтобы вывод на экран изменился

           • Верно. Если изменить реализацию метода Speak() в классе Animal, это повлияет на все производные классы, которые не переопределили его.

        6. Если класс Animal и метод Speak в классе Animal сделать абстрактными, код продолжит компилироваться, а вывод не изменится

           • Неверно. Код не будет компилироваться, так как класс Animal станет абстрактным, и его нельзя будет инстанцировать без реализации метода Speak() в производных классах.

        7. Метод Speak в классе Fish можно пометить sealed, код продолжит компилироваться, а вывод не изменится

           • Верно. Пометив метод как sealed, мы запрещаем его переопределение в производных классах, но это не повлияет на текущий вывод программы.

        8. Метод Speak в классе Mammal можно пометить sealed, код продолжит компилироваться, а вывод не изменится

           • Неверно. Если пометить метод Speak() в классе Mammal как sealed, это предотвратит его переопределение в классе Dog, что изменит поведение и вывод программы.

        ▎Итог:

        Верные утверждения:

        • 2

        • 3

        • 4

        • 5

        • 7
     
    */

    #endregion

    #region Задача про магов

    /*
    
        Вам предстоит реализовать функцию нанесения урона для игры
        Есть два типа персонажей с определенными значениями характеристик.
        - Воин: сила 5, магия 0, здоровье: 100
        - Маг: сила 1, магия 2, здоровье: 50

        Есть 3 типа оружия. У оружия есть формула урона, которая может зависеть от характеристик атакующего персонажа.
        - Меч: сила, урон = сила * 3
        - Волшебный посох: урон = магия * 2 + 2
        - Жезл дурака: наносит случайный урон противнику от 0 до 10 (все включительно). Если жезлом владеет маг, то характеристики мага (сила, магия) меняются местами случайным образом.

        Нужно написать функцию InflictDamage, которая будет наносить урон персонажу-противнику в зависимости от оружия и атакующего персонажа.
        Минимальный уровень здоровья = 0. API этой функции можно придумать на свой вкус. Но при написании кода и выборе контракта этой функции нужно учитывать следующие требования:
        - типы персонажей и оружия в будущем будут сильно расширяться
        - над проектом работают несколько разработчиков параллельно
        - API  классов должен быть удобным для использования

        Нужно реализовать описанные выше требования. Для этого вы можете создавать любые дополнительные методы или классы, а так же как угодно изменять существующий код.
     
    */

    public abstract class Character
        {
            public int Strength { get; set; }
            public int Magic { get; set; }
            public int Health { get; set; }

            public Character(int strength, int magic, int health)
            {
                Strength = strength;
                Magic = magic;
                Health = health;
            }

            public void TakeDamage(int damage)
            {
                Health -= damage;
                if (Health < 0) Health = 0; // Минимальное здоровье = 0
            }
        }

        public class Warrior : Character
        {
            public Warrior() : base(5, 0, 100) { }
        }

        public class Mage : Character
        {
            public Mage() : base(1, 2, 50) { }
        }

        public interface IWeapon
        {
            int CalculateDamage(Character attacker);
        }

        public class Sword : IWeapon
        {
            public int CalculateDamage(Character attacker)
            {
                return attacker.Strength * 3;
            }
        }

        public class MagicStaff : IWeapon
        {
            public int CalculateDamage(Character attacker)
            {
                return attacker.Magic * 2 + 2;
            }
        }

        public class FoolStaff : IWeapon
        {
            private Random _random = new Random();

            public int CalculateDamage(Character attacker)
            {
                if (attacker is Mage)
                {
                    // Меняем местами силу и магию случайным образом
                    var tempStrength = attacker.Strength;
                    attacker.Strength = attacker.Magic;
                    attacker.Magic = tempStrength;
                }

                return _random.Next(0, 11); // Случайный урон от 0 до 10
            }
        }

        public class GameEngine
        {
            public void InflictDamage(Character attacker, IWeapon weapon, Character defender)
            {
                int damage = weapon.CalculateDamage(attacker);
                defender.TakeDamage(damage);
                Console.WriteLine($"{attacker.GetType().Name} атакует {defender.GetType().Name} на {damage} урона. Осталось здоровья: {defender.Health}");
            }
        }

        // Пример использования
        class Program2
        {
            static void Main(string[] args)
            {
                Character warrior = new Warrior();
                Character mage = new Mage();
                IWeapon sword = new Sword();
                IWeapon magicStaff = new MagicStaff();
                IWeapon foolStaff = new FoolStaff();

                GameEngine gameEngine = new GameEngine();

                gameEngine.InflictDamage(warrior, sword, mage);
                gameEngine.InflictDamage(mage, magicStaff, warrior);
                gameEngine.InflictDamage(mage, foolStaff, warrior);
            }
        }

    #endregion


    static async Task Main()
    {

    }
}

public static class Extensions
{
    //Реализовать метод-расширение CustomSelect для IEnumerable<T>,
    //который будет выполнять проекцию элементов коллекции с использованием переданной функции-селектора.
    //Метод должен работать аналогично стандартному LINQ-методу Select, но с собственной реализацией.
    public static IEnumerable<TResult> CustomSelect<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
        // Проверка входных параметров
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        // Ленивое выполнение с помощью yield
        foreach (var item in source)
        {
            yield return selector(item);
        }
    }
}

