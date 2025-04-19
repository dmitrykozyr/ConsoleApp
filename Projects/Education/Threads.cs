namespace SharpEdus;

public class Threads
{
    #region Конкурентные коллекции
    /*

        Могут быть безопасно использованы из нескольких потоков одновременно:

        - ConcurrentBag<T> - неупорядоченная коллекция
        - ConcurrentDictionary<TKey, TValue>
        - ConcurrentQueue<T>
        - ConcurrentStack<T>

    */
    #endregion

    #region Неизменяемые коллекции
    /*

        Используются:
        - в многопоточных приложениях, где несколько потоков изменяют одну коллекцию одновременно
        - для сохранения состояния коллекции на определенный момент времени
        - для реализации отмены операции
        - для сохранения состояния приложения

        ImmutableList<T>
        ImmutableArray<T>
        ImmutableDictionary<TKey, TValue>
        ImmutableHashSet<T>
        ImmutableQueue<T>
        ImmutableStack<T>

    */
    #endregion

    class Counter
    {
        public int X;

        public int Y;

        public Counter() { }

        public Counter(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Count()
        {
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("Вторичный поток:");

                Console.WriteLine(i * X * Y);

                Thread.Sleep(400);
            }
        }
    }

    static volatile bool stop;

    static void Main_()
    {
        //General_();
        //Join_();
        //BreakThread_();
        //ThreadPriorities_();
        //ThreadPool_();
        //SecondaryThreads_();
        //Volatile_();
        //AccessStaticVariables_();
        //Lock_();
        //Mutex_();
        //Semaphore_();
        //AutoResetEvent_();
        //ManualResetEvent_();
        //CountdownEvent_();
        //RegisteredWaitHandle_();
        //EventWaitHandle_();
        //SendArgumentToThread_();
        //SendSeveralArgumentsToThreadNotSequre_();
        //SendSeveralArgumentsToThreadSequre_();
    }

    static void General_()
    {
        /*
            Каждому потоку выделяется квант времени

            Статусы потока содержатся в перечислении ThreadState:
            - Unstarted         не запущен
            - Running           запущен и работает
            - Background        выполняется в фоновом режиме
            - StopRequested     получил запрос на остановку
            - Stopped           завершен
            - SuspendRequested  получил запрос на приостановку
            - Suspended         приостановлен
            - WaitSleepJoin     заблокирован методами Sleep или Join
            - AbortRequested    для потока вызван метод Abort, но остановка еще не произошла
            - Aborted           остановлен, но еще окончательно не завершен
        */

        object lockCompleted = new object();

        void F1()
        {
            lock (lockCompleted) 
            { 
                Console.WriteLine("F1"); 
            }
        }

        var thread = new Thread(F1);

        //new Thread(F1).Start(); // Эквивалентно предыдущей записи

        thread.Name = "Thread 1";

        thread.Start();

        // CurrentThread - поток, выполняющий данный метод
        Console.WriteLine("IsAlive " + Thread.CurrentThread.IsAlive);
        Console.WriteLine("IsBackground " + Thread.CurrentThread.IsBackground);
        Console.WriteLine("ThreadState " + Thread.CurrentThread.ThreadState);
    }

    static void Interrupt_()
    {
        void F1()
        {
            for (; ; )
            {
                Console.WriteLine("for");
            }
        }

        Console.WriteLine("Start" + Thread.CurrentThread.ManagedThreadId);

        var thread = new Thread(F1);

        thread.Start();

        Thread.Sleep(2000);

        thread.Interrupt(); // Через 2 секунды метод прервется
                            // Abort устарел

        Console.WriteLine("End" + Thread.CurrentThread.ManagedThreadId);
    }

    static void Priorities_()
    {
        /*
            Приоритеты потоков, располагаются в перечислении ThreadPriority:
            - Lowest
            - BelowNormal
            - Normal - по умолчанию
            - AboveNormal
            - Highest
        */

        static void F1()
        {
            Console.WriteLine("Start " + Thread.CurrentThread.ManagedThreadId + " " + Thread.CurrentThread.Priority);

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("-- Loop " + Thread.CurrentThread.ManagedThreadId + " " + Thread.CurrentThread.Priority);
            }

            Console.WriteLine("End " + Thread.CurrentThread.ManagedThreadId + " " + Thread.CurrentThread.Priority);
        }

        var thread = new Thread[5];

        for (int i = 0; i < 5; i++)
        {
            thread[i] = new Thread(F1);
        }

        thread[0].Priority = ThreadPriority.Lowest;
        thread[1].Priority = ThreadPriority.Normal;
        thread[2].Priority = ThreadPriority.Highest;
        thread[3].Priority = ThreadPriority.BelowNormal;
        thread[4].Priority = ThreadPriority.AboveNormal;

        for (int i = 0; i < 5; i++)
        {
            thread[i].Start();
        }
    }

    static void Pool_()
    {
        void F1()
        {
            int availableThreads;
            int availableIOThreads;

            ThreadPool.GetAvailableThreads(out availableThreads, out availableIOThreads);

            Console.WriteLine("Потоков в пуле " + availableThreads);

            Console.WriteLine("Свободных потоков в пуле " + availableIOThreads);
        }

        void F2(object state)
        {
            Thread.CurrentThread.Name = "1";

            Console.WriteLine("Начало потока " + Thread.CurrentThread.Name);

            Thread.Sleep(1000);

            Console.WriteLine("Конец потока " + Thread.CurrentThread.Name);
        }

        void F3(object state)
        {
            Thread.CurrentThread.Name = "2";

            Console.WriteLine("Начало потока " + Thread.CurrentThread.Name);

            Thread.Sleep(1000);

            Console.WriteLine("Конец потока " + Thread.CurrentThread.Name);
        }



        Console.WriteLine("Начало");

        ThreadPool.QueueUserWorkItem(new WaitCallback(F2));

        F1();

        ThreadPool.QueueUserWorkItem(F3); // Помещаем F3 в очередь пула потоков

        Thread.Sleep(1000);

        Console.WriteLine("Конец");
    }

    static void ForegroundBackground_()
    {
        // Есть два варианта работы вторичных потоков:
        // - Foreground - будет работать после завершения первичного потока (по умолчанию)
        // - Background - завершает работу вместе с первичным потоком

        object lockCompleted = new object();

        void F1()
        {
            lock (lockCompleted)
            {
                Console.WriteLine("F1"); 
            }
        }

        var thread = new Thread(F1);

        thread.IsBackground = true;

        thread.Start();
    }

    static void Join_()
    {
        // Заставляет первичный поток ждать завершения вторичного
        // С Join будет: 1 2 3
        // Без Join:     1 3 2
        
        void F1()
        {
            Thread.Sleep(500);

            Console.WriteLine("F1: " + Thread.CurrentThread.ManagedThreadId);
        }

        Console.WriteLine("Start: " + Thread.CurrentThread.ManagedThreadId);

        var thread = new Thread(F1);

        thread.Start();

        thread.Join();

        Console.WriteLine("End: " + Thread.CurrentThread.ManagedThreadId);
    }

    // Каждый поток работает со своей статической переменной, а не общей для всех
    [ThreadStatic]
    static int counter = 0;

    static void AccessStaticVariables_()
    {
        static void F1()
        {
            if (counter < 10)
            {
                counter++;

                Console.WriteLine("Начало " + Thread.CurrentThread.ManagedThreadId + ", счетчик " + counter);

                var thread = new Thread(F1); // Плохо - каждый раз создаем новый поток

                thread.Start();

                thread.Join();
            }
        }

        Console.WriteLine("Начало " + Thread.CurrentThread.ManagedThreadId);

        var thread = new Thread(F1);

        thread.Start();

        thread.Join();

        Console.WriteLine("Конец " + Thread.CurrentThread.ManagedThreadId);
    }

    static void Interlocked_()
    {
        var count_1 = 0;
        var count_2 = 0;

        // Результат каждый раз будет разный
        Task.WaitAll
        (
            Task.Run(() => { for (int i = 0; i < 10000; i++) count_1++; }),
            Task.Run(() => { for (int i = 0; i < 10000; i++) count_1++; }),
            Task.Run(() => { for (int i = 0; i < 10000; i++) count_1++; }),
            Task.Run(() => { for (int i = 0; i < 10000; i++) count_1++; })
        );

        // Результат каждый раз будет одинаковый - делать так при работе с потоками
        Task.WaitAll
        (
            Task.Run(() => { for (int i = 0; i < 10000; i++) Interlocked.Increment(ref count_2); }),
            Task.Run(() => { for (int i = 0; i < 10000; i++) Interlocked.Increment(ref count_2); }),
            Task.Run(() => { for (int i = 0; i < 10000; i++) Interlocked.Increment(ref count_2); }),
            Task.Run(() => { for (int i = 0; i < 10000; i++) Interlocked.Increment(ref count_2); })
        );

        Console.WriteLine(count_1);
        Console.WriteLine(count_2);
    }

    static void Lock_()
    {
        // Внутри lock может одновременно работать один поток
        // locker - это пустой объект-заглушка типа object        
        // Внутри lock нельзя вызывать await, т.к. возможна ситуация, что объект синхронизации берется одним потоком, а отпускается другим
        // Может привести к deadlock

        object locker = new object();

        string value = string.Empty;

        void F1()
        {
            Thread.Sleep(1000);

            lock (locker)
            {
                value = "Обновляем значение";
            }
        }

        Task.Factory.StartNew(F1);

        Console.WriteLine("Первичный потом ждет");

        lock (locker)
        {
            value = "Обновляем значение в первичном потоке";

            Console.WriteLine("Значение: " + value);
        }

        Thread.Sleep(1000);

        Console.WriteLine("Отпускаем рабочий поток");
    }

    static void Deadlock_()
    {
        // Дедлок произойдет, если
        // - сначала вызовем Method1()
        // - затем в другом потоке Method3()
        // - оба метода используют блокировку lock1, а Method2() использует блокировку lock2, что может привести к блокировке обоих потоков

        object lock1 = new object();
        object lock2 = new object();

        void Method1()
        {
            lock (lock1)
            {
                Method2();
            }
        }

        void Method2()
        {
            lock (lock2)
            {
                Method3();
            }
        }

        void Method3()
        {
            lock (lock1)
            {
                // ...
            }
        }
    }

    static void Semaphore_()
    {
        // Позволяет ограничить количество потоков, имеющих доступ к ресурсу путем создания счетчика, который:
        // - уменьшается, когда поток получает доступ к ресурсу
        // - увеличивается, когда поток освобождает ресурс

        // Если счетчик равен нулю - другие потоки должны ждать, пока ресурс освободится
        // Может быть полезным для ограничения количества запросов к БД
        // SemaphoreSlim меньше нагружает процессор и работает в рамках одного процесса

        Semaphore semaphore;

        void F1(object number)
        {
            // Ждем получения семафора
            semaphore.WaitOne();

            Console.WriteLine("Начало " + Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(2000);

            Console.WriteLine("Конец " + Thread.CurrentThread.ManagedThreadId);

            // Освобождаем семафор
            semaphore.Release();
        }

        // 1й аргумент - начальное количество слотов
        // 2й аргумент - максимальное количество слотов
        // 3й аргумент - имя семафора
        semaphore = new Semaphore(2, 4, "Семафор");

        // Убираем 2 из предыдущей строки, теперь семафор могут использовать максимальное число потоков - 4
        semaphore.Release(2);
        
        for (int i = 0; i < 5; i++)
        {
            new Thread(F1).Start(i);
        }
    }
    
    static void Mutex_()
    {
        // Позволяет блокировать доступ к ресурсу не только в пределах одного процесса, но и между процессами
        // Создает объект блокировки, который одновременно может быть захвачен только одним потоком
        // Нет межпроцессорной синхронизации

        var mutex = new Mutex(false, "Mutex");

        void F1()
        {
            mutex.WaitOne();

            Console.WriteLine("F1");

            // Запускаем метод, который тоже использует данный конкретный mutex
            // Пока F2() не отработает, F1 будет ждать
            F2();

            mutex.ReleaseMutex();
        }

        void F2()
        {
            // Приостанавливаем выполнение потока, пока не будет получен mutex
            mutex.WaitOne();

            Console.WriteLine("F2");

            // После выполнения всех действий поток освобождает
            mutex.ReleaseMutex();
        }



        var thread = new Thread[5];

        for (int i = 0; i < 5; i++)
        {
            thread[i] = new Thread(F1)
            {
                Name = i.ToString()
            };

            thread[i].Start();
        }
    }
}
