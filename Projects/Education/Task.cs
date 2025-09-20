namespace Education;

class Tasks
{
    #region Разница между Task.Delay и Thread.Sleep

        /*
            Thread.Sleep(5):

                - синхронный метод
                  Блокирует текущий поток на указанный промежуток времени
                  Во время выполнения Thread.Sleep поток не может выполнять никакие другие задачи,
                  что может привести к неэффективному использованию ресурсов, особенно в приложениях с высокой конкурентностью
        
                - блокирует поток и может привести к снижению производительности, особенно в UI-приложениях,
                  т.к. это может вызвать зависание интерфейса пользователя
        
                - не возвращает значение и просто приостанавливает выполнение текущего потока

            Task.Delay(5):

                - асинхронный метод
                  Не блокирует поток, а возвращает Task, который завершится через указанное время
                  Позволяет потоку продолжать выполнение других задач, что делает его более подходящим для асинхронного программирования и работы с UI-приложениями
        
                - не блокирует поток, что позволяет более эффективно использовать ресурсы и поддерживать отзывчивость приложения
        
                - возвращает объект Task, который можно ожидать (await), что позволяет использовать его в асинхронных методах и управлять потоком выполнения
        */

    #endregion

    #region Разница между Task.WaitAll и Thread.WhenAll

        /*
            Task.WaitAll():

                - синхронный - блокирует вызывающий поток, пока все переданные задачи не завершатся

                - если одна или несколько задач завершатся с ошибкой, WaitAll() выбросит AggregateException,
                  содержащий все исключения от завершившихся с ошибкой задач

                - используется, когда необходимо синхронно дождаться завершения задач, например, в консольных приложениях или методах,
                  где асинхронное выполнение не требуется

            Task.WhenAll():

                - асинхронный - возвращает задачу, которая завершится, когда все переданные задачи завершатся,
                  позволяет продолжать выполнение кода в вызывающем потоке, не блокируя его

                - если одна или несколько задач завершаются с ошибкой, WhenAll() также выбросит AggregateException,
                  когда завершится возвращаемая задача

                - лучше использовать в асинхронных методах и контекстах, где важно не блокировать поток,
                  например, в веб-приложениях или в UI-приложениях
        */

        static void Main_1()
        {
            Task task1 = Task.Run(() =>
            {
                // выполнение задачи 1
            });

            Task task2 = Task.Run(() =>
            {
                // выполнение задачи 2
            });

            // Блокирует поток до завершения обеих задач
            Task.WaitAll(task1, task2);
        }

        static async Task Main_2()
        {
            Task task1 = Task.Run(() =>
            {
                // выполнение задачи 1
            });

            Task task2 = Task.Run(() =>
            {
                // выполнение задачи 2
            });

            // Не блокирует поток, ждет завершения задач асинхронно
            await Task.WhenAll(task1, task2);
        }

    #endregion

    static void Main_()
    {
        //General_();
        //Task_();
        //MethodsWithArguments_();
        //Continuation_();
        //ContinuationTask_();
        //ReturnDataFromTask_();
        //ReturnDataFromTask_2_();
        //CallExceptionInTask_();
        //CancelTask_();
        //Cancellation_();
        //CancellationToken_();
        //CancellationTokenSource_();
        //CancelParallelOperations_();
        //Checked_();
        //TaskFactory_();
        //TaskFactory_2_();
        //WaitAll_();
        //Factory_();
        //Lambda_();
        //Parallel_();
        //For_();
        //ForEach_();
        //InsertedTasks_();
        //TasksArray_();
        //ParallelClass_();
        //ParallelFor_();
        //ParallelForEach_();
        //BreakCycle_();
    }

    static void General_()
    {
        /*
            Task использует пул потоков
            Task.Run создает новый поток из пула потоков

            Ресурсы, связанные с Task, освобождаются автоматически сборщиком мусора
            В классе Task реализуется интерфейс IDisposable

            Задача получения результата блокирует вызывающий код, пока результат не будет вычислен
        */

        static void F1()
        {
            for (int count = 0; count < 5; count++)
            {
                Thread.Sleep(500);
            }
        }

        // Создание Task
        var task1 = new Task(F1);
        var task2 = new Task(F1);

        // Запуск Task
        task1.Start();

        // Ожидание завершения Task
        task1.Wait();

        // Ожидание завершения нескольких Task
        Task.WaitAll(task1, task2);

        // Ожидание завршения одной из Task
        Task.WaitAny(task1, task2);

        // Фабрика задач 1
        var taskFactory1 = new TaskFactory();
        Task task3 = taskFactory1.StartNew(F1);

        // Фабрика задач 2
        var taskFactory2 = Task.Factory.StartNew(F1);

        // Лямбда-выражение
        Task task4 = Task.Factory.StartNew(() => { F1(); });
        task4.Wait();
        task4.Dispose();

        // Еще способ с лямбдой
        var task5 = new Task(() => Console.WriteLine("111"));
        task5.Start();
    }

    static void CancellationToken_()
    {
        // Для отмены операции нужно:
        // - создать объект CancellationTokenSource
        // - из него получить сам токен
        // - вызвать метод Cancel() у объекта CancellationTokenSource
        // - в самой операции отловить выставление токена через token.IsCancellationRequested

        static void F1(int x, CancellationToken token)
        {
            int result = 1;

            for (int i = 1; i <= x; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Операция прервана токеном");
                    return;
                }

                result *= i;
                Console.WriteLine($"Факториал числа {x} равен {result}");
                Thread.Sleep(5000);
            }
        }

        var cancelTokenSource = new CancellationTokenSource();

        CancellationToken token = cancelTokenSource.Token;

        var task = new Task(() => F1(5, token));

        task.Start();


        string? s = Console.ReadLine();

        if (s == "Y")
        { 
            cancelTokenSource.Cancel();
        }
    }

    static void ParallelInvoke_()
    {
        // Класс Parallel берет потоки из пула потоков и управляет конкуренцией

        // Для указания метода нужно использовать делегаты:
        // - System.Action<T>
        // - System.Func<T>

        // Метод Invoke сначала инициирует выполнение методов, а затем ожидает их завершения
        // Нельзя указать порядок выполнения
        // Выполнение метода Main приостанавливается, пока не произойдет возврат из метода Invoke

        static void F1()
        {
            Console.WriteLine("F1 начало");

            for (int count = 0; count < 5; count++)
            {
                Thread.Sleep(500);
            }

            Console.WriteLine("F1 конец");
        }

        static void F2()
        {
            Console.WriteLine("F2 начало");

            for (int count = 0; count < 5; count++)
            {
                Thread.Sleep(500);
            }

            Console.WriteLine("F2 конец");
        }

        Console.WriteLine("Начало");
        Parallel.Invoke(F1, F2);
        Console.WriteLine("Конец");
    }

    static int[] data;
    static void ParallelFor_()
    {
        static void F1(int i)
        {
            data[i] = data[i] / 10;

            if (data[i] < 10000)
            {
                data[i] = 0;
            }

            if (data[i] >= 10000)
            {
                data[i] = 100;
            }

            if (data[i] > 20000)
            {
                data[i] = 200;
            }
        }

        static void F2(int x)
        {
            int result = 1;

            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }

            Thread.Sleep(3000);
        }


        Console.WriteLine("Начало");
        data = new int[100000000];

        for (int i = 0; i < data.Length; i++)
        {
            data[i] = i;
        }

        Parallel.For(0, data.Length, F1);
        Parallel.For(1, 10, F2);

        Console.WriteLine("Конец");
    }

    static void ForEach_()
    {
        // Итерация по коллекции, реализующей интерфейс IEnumerable
        // Цикл можно остановить, вызвав ParallelLoopState.Break

        static void F1(int x)
        {
            int result = 1;

            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }

            Console.WriteLine($"Executing task for {x}");
            Console.WriteLine($"Factorial of {x} equals {result}");
            Thread.Sleep(3000);
        }

        var myList = new List<int>() { 1, 3, 5, 8 };

        ParallelLoopResult result = Parallel.ForEach<int>(myList, F1);
    }

}
