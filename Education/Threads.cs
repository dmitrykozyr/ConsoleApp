using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharpEdus
{
    public class Threads
    {
        #region==== Общее ===========================================================
        // Каждому потоку выделяется определенный квант времени
        // Если у нас графическое приложение, которое посылает запрос серверу или считывает и обрабатывает
        // огромный файл, то без многопоточности у нас бы блокировался графический интерфейс на время выполнения
        // Благодаря потокам можем выделить задачу, которая может долго обрабатываться, в отдельный поток
        // Клиент-серверные приложения практически не мыслимы без многопоточности

        // Статусы потока содержатся в перечислении ThreadState:
        // Unstarted - еще не был запущен
        // Running - запущен и работает
        // Background - выполняется в фоновом режиме
        // StopRequested - получил запрос на остановку
        // Stopped - завершен
        // SuspendRequested - получил запрос на приостановку
        // Suspended - приостановлен
        // WaitSleepJoin - заблокирован методами Sleep или Join
        // AbortRequested - для потока вызван метод Abort, но остановка еще не произошла
        // Aborted - остановлен, но еще окончательно не завершен

        // Приоритеты потоков, располагаются в перечислении ThreadPriority
        // Приоритет влияет на время, выделяемое потоку процессором
        // Lowest, BelowNormal, Normal - по умолчанию, AboveNormal, Highest
        static object lockCompleted = new object();
        public static void F20()
        {
            static void F20_1()
            {
                lock (lockCompleted) { Console.WriteLine("F20_1"); }
            }

            Thread thr = new Thread(F20_1);
            new Thread(F20_1).Start(); // Эквивалентно предыдущей записи
            thr.Name = "My Thread";
            thr.Start();

            Console.WriteLine("IsAlive " + Thread.CurrentThread.IsAlive);
            Console.WriteLine("IsBackground " + Thread.CurrentThread.IsBackground);
            Console.WriteLine("ThreadState " + Thread.CurrentThread.ThreadState);
        }
        #endregion

        #region==== Join ============================================================
        // Join заставляет первичный поток ждать завершения вторичного
        // С Join будет: 1 start
        //               5 start
        //               1 end
        // Без Join будет: 1 start
        //                 1 end
        //                 5 start
        public static void F1()
        {
            static void F1_2() { Console.WriteLine("Thread " + Thread.CurrentThread.ManagedThreadId + " start"); }
            Console.WriteLine("Thread " + Thread.CurrentThread.ManagedThreadId + " start");
            var thr = new Thread(F1_2);
            thr.Start();
            thr.Join();
            Console.WriteLine("Thread " + Thread.CurrentThread.ManagedThreadId + " end");
        }
        #endregion

        #region==== Прерывание работы потока ========================================
        public static void F3()
        {
            static void F3_1()
            {
                for (;;)
                    Console.WriteLine("for");
            }

            Console.WriteLine("Start" + Thread.CurrentThread.ManagedThreadId);
            var thr = new Thread(F3_1);
            thr.Start();
            Thread.Sleep(2000);
            thr.Abort(); // Через 2 секунды метод F3_1 прервется
            Console.WriteLine("End" + Thread.CurrentThread.ManagedThreadId);
        }
        #endregion

        #region==== Приоритеты потоков ==============================================
        public static void F4()
        {
            static void F4_1()
            {
                Console.WriteLine("Start " + Thread.CurrentThread.ManagedThreadId + " " +
                                   Thread.CurrentThread.Priority);
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("Loop " + Thread.CurrentThread.ManagedThreadId + " " +
                                       Thread.CurrentThread.Priority);
                }
                Console.WriteLine("End " + Thread.CurrentThread.ManagedThreadId + " " +
                    Thread.CurrentThread.Priority);
            }

            Thread[] thr = new Thread[5];

            for (int i = 0; i < 5; i++)
                thr[i] = new Thread(F4_1);

            thr[0].Priority = ThreadPriority.Lowest;
            thr[1].Priority = ThreadPriority.Normal;
            thr[2].Priority = ThreadPriority.Highest;
            thr[3].Priority = ThreadPriority.BelowNormal;
            thr[4].Priority = ThreadPriority.AboveNormal;

            for (int i = 0; i < 5; i++)
                thr[i].Start();
        }
        #endregion

        #region==== Пул потоков =====================================================
        public static void F5()
        {            
            static void F5_1() // Метод выводит, сколько доступно потоков в пуле
            {
                Thread.Sleep(1000);
                int availableThreads;
                int availableIOThreads;
                int maxThreads;
                int maxIOThreads;

                // Сколько потоков доступно в начале программы
                ThreadPool.GetAvailableThreads(out availableThreads, out availableIOThreads);

                // Сколько потоков максимально доступно во время выполнения
                ThreadPool.GetMaxThreads(out maxThreads, out maxIOThreads);

                Console.WriteLine("Number of threads in the pool {0}, max threads {1}",
                    availableThreads, maxThreads);
                Console.WriteLine("Number of free IO threads in the pool {0}, max threads {1}",
                    availableIOThreads, maxIOThreads);
            }

            static void F5_2(object state)
            {
                Thread.CurrentThread.Name = "1";
                Console.WriteLine("Start thread " + Thread.CurrentThread.Name);
                Thread.Sleep(1000);
                Console.WriteLine("End thread " + Thread.CurrentThread.Name);
            }

            static void F5_3(object state)
            {
                Thread.CurrentThread.Name = "2";
                Console.WriteLine("Start thread " + Thread.CurrentThread.Name);
                Thread.Sleep(1000);
                Console.WriteLine("EndND thread " + Thread.CurrentThread.Name);
            }

            Console.WriteLine("Start");
            ThreadPool.QueueUserWorkItem(new WaitCallback(F5_2));
            F5_1();
            ThreadPool.QueueUserWorkItem(F5_3); // Помещаем F5_3 в очередь пула потоков
            F5_1();
            Thread.Sleep(1000);
            Console.WriteLine("End");
            F5_1();
        }
        #endregion

        #region==== Вторичные потоки ================================================
        // Есть два варианта работы вторичных потоков
        // Foreground - будет работать после завршения первичного потока (по умолчанию)
        // Background - завершает работу вместе с первичным потоком
        public static void F13()
        {
            var thr = new Thread(F1);
            thr.IsBackground = true;
            thr.Start();
        }
        #endregion

        #region==== volatile ========================================================
        // Компилятор увидит, что stop не изменяется, поэтому для переменной
        // с модификатором volatile сгенерируется условие выхода из цицкла
        static volatile bool stop;
        public static void F14()
        {
            static void F14_1()
            {
                int x = 0;
                while (!stop)
                {
                    Console.WriteLine("Loop");
                    x++;
                }
            }

            Thread thr = new Thread(F14_1);
            thr.Start();
            Thread.Sleep(1000);
            stop = true;
            thr.Join();
        }
        #endregion

        // Синхронизация

        #region==== Доступ к static - переменной из нескольких потоков ==============
        // Атрибут означает, что каждый поток работает со своей статической переменной, а не с общей для всех
        [ThreadStatic]
        public static int counter = 0;
        public static void F2()
        {
            static void F2_1()
            {
                if (counter < 10)
                {
                    counter++;
                    Console.WriteLine("Start " + Thread.CurrentThread.ManagedThreadId + ", counter " + counter);
                    var thr = new Thread(F2); // Плохо - каждый раз создаем новый поток
                    thr.Start();
                    thr.Join();
                }
            }

            Console.WriteLine("Start " + Thread.CurrentThread.ManagedThreadId);
            var thr = new Thread(F2_1);
            thr.Start();
            thr.Join();
            Console.WriteLine("End " + Thread.CurrentThread.ManagedThreadId);
        }
        #endregion

        #region==== Lock ============================================================
        // Внутри lock может одновременно работать только один поток
        // locker - Это пустой объект-заглушка типа object
        static object locker = new object();
        static string value = string.Empty;
        public static void F25()
        {
            static void F25_1()
            {
                Thread.Sleep(1000);
                lock (locker) { value = "Updating value"; }
            }

            Task.Factory.StartNew(F25_1);
            Console.WriteLine("Main thread is waiting");
            lock (locker)
            {
                value = "Updating value in main thread";
                Console.WriteLine("Value: " + value);
            }
            Thread.Sleep(1000);
            Console.WriteLine("Released worker thread");
        }
        #endregion

        #region==== Mutex ===========================================================
        // Когда выполнение дойдет до mutex.WaitOne(), поток будет ожидать, пока не освободится мьютекс
        // После освобождения продолжит работу
        // Нет межпроцессорной синхронизации
        static Mutex mutex = new Mutex(false, "Mutex_1");
        public static void F6()
        {
            static void F6_1()
            {
                mutex.WaitOne();
                Console.WriteLine("1");
                F6_2(); // Запускаем метод, который тоже использует данный конкретный mutex
                        // Пока F6_2() не отработает, F6_2 будет ждать
                mutex.ReleaseMutex();
            }

            static void F6_2()
            {
                mutex.WaitOne();        // Приостанавливаем выполнение потока, пока не будет получен mutex
                Console.WriteLine("2");
                mutex.ReleaseMutex();   // После выполнения всех действий, когда мьютекс не нужен,
                                        // поток освобождает
            }

            Thread[] thr = new Thread[5];
            for (int i = 0; i < 5; i++)
            {
                thr[i] = new Thread(F6_1);
                thr[i].Name = i.ToString();
                thr[i].Start();
            }
        }
        #endregion

        #region==== Semaphore =======================================================
        // Похож на mutex, но дает одновременный доступ к общему ресурсу не одному, а нескольким потокам
        static Semaphore semaphore;
        public static void F7()
        {
            static void F7_1(object number)
            {
                semaphore.WaitOne(); // Ожидание получения семафора
                Console.WriteLine("Begin " + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(2000);
                Console.WriteLine("End " + Thread.CurrentThread.ManagedThreadId);
                semaphore.Release(); // Освобождаем семафор
            }

            // 1й аргумент - начальное количество слотов
            // 2й аргумент - максимальное количество слотов
            // 3й аргумент - имя семафора
            semaphore = new Semaphore(2, 4, "Semaphore_1");
            semaphore.Release(2); // Убираем 2 из предыдущей строки, теперь семафор могут
                                  // использовать максимальное число потоков - 4
            for (int i = 0; i < 5; i++)
                new Thread(F7_1).Start(i);
        }
        #endregion

        #region==== SemaphoreSlim ===================================================        
        static SemaphoreSlim semaphoreSlim; // Меньше нагружает процессор и работает в рамках одного процесса
        public static void F8()
        {
            static void F8_1(object number)
            {
                semaphoreSlim.Wait();
                Console.WriteLine("Begin " + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(2000);
                Console.WriteLine("End " + Thread.CurrentThread.ManagedThreadId);
                semaphoreSlim.Release();
            }

            semaphoreSlim = new SemaphoreSlim(2, 4);
            semaphoreSlim.Release(2);   // Убираем 2 из предыдущей строки, теперь семафор одновременно
                                        // могут использовать максимальное число потоков - 4
            for (int i = 0; i < 5; i++)
                new Thread(F8_1).Start(i);
        }
        #endregion

        // Обработка событий

        #region==== AutoResetEvent ==================================================
        // Класс AutoResetEvent нужен для синхронизации потоков
        // Является оберткой над объектом "событие"
        // Позволяет переключить данный объект из сигнального в несигнальное состояние
        // Уведомляет ожидающий поток, что произошло событие

        // false - установка в несигнальное состояние, тогда объект изначально в несигнальном состоянии,
        // а поскольку все потоки блокируются методом waitHandler.WaitOne() до ожидания сигнала,
        // то произойдет блокировка программы, и программа не будет выполнять никаких действий
        static AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        public static void F9()
        {
            static void F9_1()
            {
                Console.WriteLine("1");
                // Метод WaitOne указывает, что текущий поток переводится в состояние ожидания,
                // пока объект waitHandler не будет переведен в сигнальное состояние
                // Так все потоки переводятся в состояние ожидания
                autoResetEvent.WaitOne(); // Остановка вторичного потока
                Console.WriteLine("2");
                autoResetEvent.WaitOne(); // Остановка вторичного потока
                Console.WriteLine("3");
            }

            static void F9_2()
            {
                Console.WriteLine("1 Begin");
                autoResetEvent.WaitOne();
                Console.WriteLine("1 End");
            }

            static void F9_3()
            {
                Console.WriteLine("2 Begin");
                autoResetEvent.WaitOne();
                Console.WriteLine("2 End");
            }

            Thread thr = new Thread(F9_1);
            thr.Start();
            Console.WriteLine("Press any button to continue");
            Console.ReadKey();
            autoResetEvent.Set();   // Метод уведомляет все ожидающие потоки, что объект waitHandler
                                    // снова находится в сигнальном состоянии, и один из потоков захватывает
                                    // данный объект, переводит в несигнальное состояние,
                                    // а остальные потоки снова ожидают
            Console.WriteLine("Press any button to continue");
            Console.ReadKey();
            autoResetEvent.Set();   // Продолжение выполнения вторичного потока

            new Thread(F9_2).Start();
            new Thread(F9_3).Start();
            Console.WriteLine("Press any button to continue");
            Console.ReadKey();
            autoResetEvent.Set();   // Сигнал одному потоку
            autoResetEvent.Set();   // Сигнал другому потоку

            // Если в программе несколько объектов AutoResetEvent, можем использовать для отслеживания
            // состояния этих объектов методы WaitAll и WaitAny, которые в качестве параметра принимают
            // массив объектов класса WaitHandle - базового класса для AutoResetEvent
            // AutoResetEvent.WaitAll(new WaitHandle[] { waitHandler });
        }
        #endregion

        #region==== ManualResetEvent ================================================
        // false - установка в несигнальное состояние
        static ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        // slim работает на уровне потоков, а не процессов
        static ManualResetEventSlim manualResetEventSlim = new ManualResetEventSlim(false);
        public static void F10()
        {
            static void F10_1()
            {
                Console.WriteLine("1 Begin");
                manualResetEvent.WaitOne();
                //resetEvent.WaitOne();
                Console.WriteLine("1 End");
            }

            static void F10_2()
            {
                Console.WriteLine("2 BEGIN");
                manualResetEvent.WaitOne();
                Console.WriteLine("2 END");
            }

            new Thread(F10_1).Start();
            new Thread(F10_2).Start();
            Console.WriteLine("Press any button to continue");
            Console.ReadKey();
            manualResetEvent.Set(); // Сигнал всем потокам

            //Task.Factory.StartNew(F10_1);
            //Task.Factory.StartNew(F10_2);
            //resetEvent.Set();
        }
        #endregion

        #region==== CountdownEvent ==================================================
        static CountdownEvent countdown = new CountdownEvent(5);
        public static void F26()
        {
            static void F26_1() { Console.WriteLine("F26_1"); }

            Task.Factory.StartNew(F26_1);
            Task.Factory.StartNew(F26_1);
            Task.Factory.StartNew(F26_1);

            countdown.Wait();
        }
        #endregion

        #region==== RegisteredWaitHandle ============================================
        public static void F11()
        {
            static void F11_1(object state, bool istTmeOut) { Console.WriteLine("Signal"); }

            AutoResetEvent auto = new AutoResetEvent(false);
            WaitOrTimerCallback callback = new WaitOrTimerCallback(F11_1);
            RegisteredWaitHandle registeredWaitHandle = ThreadPool.RegisterWaitForSingleObject(
                auto,       // от кого ждать сигнал
                callback,   // что выполнять
                null,       // 1й аргумент callback метода
                2000,       // интервал между вызовами callback метода
                true);      // true - вызвать callback метод 1 раз,
                            // false - вызвать с интервалом
            auto.Set();
            registeredWaitHandle.Unregister(auto);
        }
        #endregion

        #region==== EventWaitHandle =================================================
        static EventWaitHandle handle = null;
        public static void F12()
        {
            static void F12_1()
            {
                handle.WaitOne(); // Приостановка потока
                while (true)
                {
                    Console.WriteLine("1");
                    Thread.Sleep(300);
                }
            }

            handle = new EventWaitHandle(
                false,                      // несигнальное состояние
                EventResetMode.ManualReset, // тип события
                "GlobalEvent::GUID");       // имя объекта синхронизации в ОС. Если объект с таким
                                            // именем существует, будет получена ссылка на него

            Thread thr = new Thread(F12_1) { IsBackground = true };
            thr.Start();
        }
        #endregion

        // Передача аргументов

        #region==== Передача агрумента в поток ======================================
        public static void F16()
        {
            static void F16_1(object x)
            {
                for (int i = 1; i < 9; i++)
                {
                    int n = (int)x;

                    Console.WriteLine("Второй поток:");
                    Console.WriteLine(i * n);
                    Thread.Sleep(400);
                }
            }

            // Передача в метод F16_1 аргумента типа int
            Thread thr = new Thread(new ParameterizedThreadStart(F16_1));
            thr.Start(5);
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("Main thread: ");
                Console.WriteLine(i * i);
                Thread.Sleep(300);
            }

            // Альтернативный способ запуска
            //new Thread(() =>
            //{
            //    Thread.Sleep(1000);
            //    Console.WriteLine("thr1");
            //}).Start();
        }
        #endregion

        #region==== Передача нескольких агрументов в поток (небезопасная) ===========
        // Для передачи в поток нескльких аргументов нужен класс
        // Метод Thread.Start не является типобезопасным и мы можем передать в него любой тип,
        // а потом придется приводить переданный объект к нужному типу
        public class Counter
        {
            public int x;
            public int y;
        }
        public static void F17()
        {
            static void F17_1(object obj)
            {
                for (int i = 1; i < 9; i++)
                {
                    Counter c = (Counter)obj;

                    Console.WriteLine("Второй поток:");
                    Console.WriteLine(i * c.x * c.y);
                }
            }

            Counter counter = new Counter();
            counter.x = 4;
            counter.y = 5;

            Thread thr = new Thread(new ParameterizedThreadStart(F17_1));
            thr.Start(counter);
        }
        #endregion

        #region==== Передача нескольких агрументов в поток (безопасная) =============
        // Рекомендуется объявлять все используемые методы и переменные в специальном классе,
        // а в основной программе запускать поток через ThreadStart
        public class Counter_2
        {
            private int x;
            private int y;

            public Counter_2(int _x, int _y)
            {
                this.x = _x;
                this.y = _y;
            }

            public void Count()
            {
                for (int i = 1; i < 9; i++)
                {
                    Console.WriteLine("Второй поток:");
                    Console.WriteLine(i * x * y);
                    Thread.Sleep(400);
                }
            }
        }

        public static void F18()
        {
            Counter_2 counter = new Counter_2(5, 4);
            Thread thr = new Thread(new ThreadStart(counter.Count));
            thr.Start();
        }
        #endregion

        static void Main_()
        {
            Console.ReadKey();
        }
    }
}
