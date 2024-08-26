namespace SharpEdu
{
    class Tasks
    {
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

        struct Structure { public int a; public int b; }

        static int[] data;

        static void ThreadSleep()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(500);
                Console.WriteLine("Thread Id " + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("TaskId " + Task.CurrentId);
            }
        }

        static void General_()
        {
            // Task - использует пул потоков

            // Thread.CurrentThread.IsBackground = true;
            // Если false (по умолчанию), то после завершения Main ожидается завершение недовыполненных task, иначе они прерываются

            // Исполнением задач управляет планировщик задач, который работает с пулом потоков
            
            // В отличие от класса Thread, в классе Task нет свойства Name для хранения имени задачи,
            // но есть свойство Id
            
            // В классе Task реализуется интерфейс IDisposable
            // Обычно ресурсы, связанные с классом Task, освобождаются автоматически сборщиком мусора
            // Этот метод можно вызывать для отдельной задачи только после ее завершения,
            // которое можно отследить методом Wait
            // Если вызвать Dispose для активной задачи, возникнет исключение InvalidOperationException

            // Задача получения результата блокирует вызывающий код, пока результат не будет вычислен
            
            static void F1()
            {
                Console.WriteLine("MyTask() was launched, task id " + Task.CurrentId);

                for (int count = 0; count < 5; count++)
                {
                    Thread.Sleep(500);
                    Console.WriteLine("In method MyTask calculation is equal " + count + ", task id " + Task.CurrentId);
                }

                Console.WriteLine("MyTask() was completed, task id " + Task.CurrentId);
            }

            Console.WriteLine("Main thread was launched, task id " + Task.CurrentId);
            var task1 = new Task(F1);       // Создание Task
            var task2 = new Task(F1);
            task1.Start();                  // Запуск Task
            task2.Start();
            task1.Wait();                   // Ожидание завершения Task
            task2.Wait();
            
            //Task.WaitAll(task1, task2);   // Ожидание завершения нескольких Task
            // Обе эти задачи не должны ждать завершения друг друга, иначе метод никогда не завершится
            //Task.WaitAny(task1, task2);   // Ожидание завршения одной из Task

            // Другие способы запуска Task, где она создается и запускается в одно действие:
            // Фабрика задач
            TaskFactory tf = new TaskFactory();
            Task t1 = tf.StartNew(F1);

            // Фабрика задач через задачу
            var t2 = Task.Factory.StartNew(F1);

            // Конструктор Task
            var t3 = new Task(F1);
            t3.Start();

            // Лямбда-выражение
            Task t4 = Task.Factory.StartNew(() => 
            {
                for (int count = 0; count < 10; count++)
                {
                    Thread.Sleep(500);
                }
            });
            t4.Wait();
            t4.Dispose();

            // Еще способ с лямбдой
            var task = new Task(() => Console.WriteLine("Hello Task!"));
            task.Start();

            Console.WriteLine("Main thread was finished, task id " + Task.CurrentId); Console.ReadLine();
        }

        static void Task_()
        {
            var action = new Action(ThreadSleep);
            var task = new Task(action);
            var task_2 = new Task(action);
            task.Start(); // Асинхронный запуск
            task_2.Start();
            Console.WriteLine("Status " + task.Status);

            //task1.RunSynchronously(); // Cинхронный запуск
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(500);
                Console.WriteLine("Main, TaskStatus " + task.Status);
            }
        }

        static void MethodsWithArguments_()
        {
            // object нужен для работы с методами с аргументам
            static void F2_2(object obj) 
            { 
                Console.WriteLine("F2 " + obj.ToString()); 
            }

            Action<object> action = new Action<object>(F2_2);
            Task task = new Task(action, "second argument");
            task.Start();
            Thread.Sleep(500);
            Console.WriteLine(task.AsyncState as string);
            // Аналог Join() - останавливает первичный поток, пока не завершится вторичный
            task.Wait();

            // Организация задержки
            while (!task.IsCompleted)
            {
                Thread.Sleep(200);
            }

            // Аналог Join
            IAsyncResult asyncResult = task as IAsyncResult;
            ManualResetEvent waitHandle = asyncResult.AsyncWaitHandle as ManualResetEvent;
            waitHandle.WaitOne();
        }

        static void Continuation_()
        {
            // Автоматический запуск новой задачи после завершения предыдушей
            // Такой метод должен принимать аргумент типа Task
            static void F1(Task task)
            {
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(500);
                    Console.WriteLine("--");
                }
            }

            var action = new Action(ThreadSleep);       // Создание 1й задачи
            var task = new Task(action);
            var action_1 = new Action<Task>(F1);        // Создание продолжения задачи
            Task task_2 = task.ContinueWith(action_1);
            task.Start();                               // Последовательный запуск
        }

        static void ContinuationTask_()
        {
            // Задачи продолжения позволяют определить задачи, которые выполняются после завершения других задач
            // Можем вызвать после выполнения одной задачи несколько других, определить условия их вызова,
            // передать из предыдущей задачи в следующую данные
            
            static void F8()
            {
                static void F8_1(Task t)
                {
                    Console.WriteLine($"Id задачи: {Task.CurrentId}");
                    Console.WriteLine($"Id предыдущей задачи: {t.Id}");
                    Thread.Sleep(3000);
                }

                Task task1 = new Task(() => { Console.WriteLine($"Id задачи: {Task.CurrentId}"); });

                Task task2 = task1.ContinueWith(F8_1); // Задача продолжения
                task1.Start();

                task2.Wait(); // Ждем окончания второй задачи
                Console.WriteLine("Выполняется работа метода Main");
            }

            // Первая задача задается с помощью лямбда-выражения, которое выводит id задачи
            // Вторая - задача продолжения задается с помощью метода ContinueWith,
            // который принимает делегат Action<Task>
            // То есть метод Display, который передается в данный метод в качестве значения параметра,
            // должен принимать параметр типа Task
            // Благодаря передаче в метод параметра Task можем получить различные свойства предыдущей задачи,
            // как например, в данном случае получаем ее Id
            // После завершения task1 сразу вызовется task2
            
            static void F8_1()
            {
                static int Sum(int a, int b) => a + b;

                static void F8_2(int sum)
                { 
                    Console.WriteLine($"Sum: {sum}");
                }

                Task<int> task1 = new Task<int>(() => Sum(4, 5));
                Task task2 = task1.ContinueWith(sum => F8_2(sum.Result)); // задача продолжения
                task1.Start();
                task2.Wait(); // ждем окончания второй задачи
                Console.WriteLine("End of Main");
            }

            // Подобным образом можно построить целую цепочку последовательно выполняющихся задач:
            static void F8_2()
            {
                static void F9_1(Task t) 
                { 
                    Console.WriteLine($"Id задачи: {Task.CurrentId}");
                }

                Task task1 = new Task(() => 
                { 
                    Console.WriteLine($"Id задачи: {Task.CurrentId}"); 
                });

                // задача продолжения
                Task task2 = task1.ContinueWith(F9_1);

                Task task3 = task1.ContinueWith((Task t) => 
                { 
                    Console.WriteLine($"Id задачи: {Task.CurrentId}"); 
                });

                Task task4 = task2.ContinueWith((Task t) => 
                { 
                    Console.WriteLine($"Id задачи: {Task.CurrentId}"); 
                });

                task1.Start();
            }
        }

        static void ReturnDataFromTask_()
        {
            // Для передачи аргументов упаковываем их в структуру и передаем через object
            static int F17_1(object value)
            {
                int a = ((Structure)value).a;
                int b = ((Structure)value).b;
                Thread.Sleep(500);
                return a + b;
            }

            Structure struct_1;
            struct_1.a = 5;
            struct_1.b = 7;
            Task<int> task;

            // 1й способ
            task = new Task<int>(F17_1, struct_1);
            task.Start();

            // 2й способ
            //TaskFactory<int> taskFactory = new TaskFactory<int>();
            //task = taskFactory.StartNew(F17_1, struct_1);

            // 3й способ
            //task = Task<int>.Factory.StartNew(F17_1, struct_1);

            Console.WriteLine(task.Result);
        }

        static void ReturnDataFromTask_2_()
        {
            // Чтобы задать возвращаемый из задачи тип объекта, нужно типизировать Task
            // Например, Task<int> - задача будет возвращать объект int
            // В качестве задачи должен выполняться метод, возвращающий данный тип объекта
            // Например, в первом случае в качестве задачи выполняется функция Factorial, которая принимает и возвращает число
            // Возвращаемое число будет храниться в свойстве Result: task1.Result
            // Нам не надо его приводить к int, оно уже само по себе будет представлять число
            // То же самое и со второй задачей task2
            // В этом случае в лямбда-выражении возвращается объект Book и мы его получаем с помощью task2.Result
            // При этом при обращении к свойству Result программа текущий поток останавливает выполнение и ждет,
            // когда будет получен результат из выполняемой задачи

            //static void F7_1()
            //{
            //    Task<int> task1 = new Task<int>(() => Factorial(5));
            //    task1.Start();
            //    Console.WriteLine($"Факториал числа 5 равен {task1.Result}");
            //    Task<Book> task2 = new Task<Book>(() =>
            //    {
            //        return new Book { Title = "Война и мир", Author = "Л. Толстой" };
            //    });
            //    task2.Start();
            //    Book b = task2.Result; // ожидаем получение результата
            //    Console.WriteLine($"Название книги: {b.Title}, автор: {b.Author}");
            //    Console.ReadLine();
            //}

            //public static class Book
            //{
            //    public string Title { get; set; }
            //    public string Author { get; set; }
            //}

            static int Factorial(int x)
            {
                int result = 1;

                for (int i = 1; i <= x; i++)
                {
                    result *= i;
                }

                return result;
            }
        }

        static void CallExceptionInTask_()
        {
            static void F1()
            {
                Console.WriteLine("Begin");
                throw new Exception();
                Console.WriteLine("End");
            }

            var task = new Task(F1);
            try
            {
                task.Start();
                task.Wait(); // Wait необходим для обработки исключения
            }
            catch (Exception ex)
            {
                Console.WriteLine("1: " + ex.GetType() + " " + ex.Message);

                if (ex.InnerException is not null)
                {
                    Console.WriteLine("2: " + ex.InnerException);
                }
            }
            finally 
            { 
                Console.WriteLine("3: " + task.Status); 
            }
        }

        static void CancelTask_()
        {
            // Сначала получается признак отмены ПО из источника ПО, затем он передается задаче,
            // затем она контролирует его на предмет получения запроса на отмену
            // Если получен запрос на отмену, задача должна завершиться
            // В одних случаях этого достаточно для прекращения задачи, а в других - из задачи должен быть
            // вызван метод ThrowIfCancellationRequested() для ПО
            // ПО является экземпляром объекта типа CancellationToken
            // В структуре CancellationToken есть свойство IsCancellationRequested - возвращает true, если была запрошена отмена задачи
            // Еще есть метод ThrowIfCancellationRequested()
            // Если ПО, для которого вызывается этот метод, получил запрос на отмену,
            // в нем генерируется исключение OperationCanceledException
            // С целью опроса в задаче проверяется свойство IsCancellationRequested
            // Если оно содержит true - отмена была запрошена, и задача должна быть завершена
            // Для создания задачи, из которой вызывается метод ThrowIfCancellationRequested(),
            // когда она отменяется, требуется передать ПО задаче и конструктору класса Task через метод StartNew()
            // ПО будет передан делегату, реализующему задачу, и экземпляру объекта типа Task
            // Факт отмены отслеживается проверкой значения свойства IsCanceled для экземпляра объекта типа Task
            // В программе ниже применяется опрос для контроля состояния ПО
            // Метод ThrowIfCancellationRequested() вызывается после входа в метод MyTask()
            // Это позволяет завершить задачу, если она была отменена еще до ее запуска
            // Внутри цикла проверяется свойство IsCancellationRequested - если true,
            // то выводится сообщение об отмене и вызывается метод ThrowIfCancellationRequested() для отмены задачи
            // Выполнение метода MyTask() отменяется в методе Main() две секунды спустя
            // В методе MyTask() выполняются четыре шага цикла
            // Когда перехватывается исключение AggregateException, проверяется состояние задачи
            // Если задача отменена - выводится соответствующее сообщение
            // Когда сообщение AggregateException генерируется в ответ на отмену задачи - это еще не свидетельствует об ошибке,
            // а означает отмену задачи
            static void F1(Object ct)
            {
                CancellationToken cancelTok = (CancellationToken)ct;
                cancelTok.ThrowIfCancellationRequested();
                Console.WriteLine("MyTask() №{0} запущен", Task.CurrentId);

                for (int count = 0; count < 10; count++)
                {
                    // Используем опрос
                    if (!cancelTok.IsCancellationRequested)
                    {
                        Thread.Sleep(500);
                        Console.WriteLine("В методе MyTask №{0} подсчет равен {1}", Task.CurrentId, count);
                    }
                }

                Console.WriteLine("MyTask() #{0} завершен", Task.CurrentId);
            }
            Console.WriteLine("Основной поток запущен");

            // Объект источника признаков отмены
            CancellationTokenSource cancelTokSSrc = new CancellationTokenSource();

            // Запустить задачу, передав ей признак отмены
            Task tsk = Task.Factory.StartNew(F1, cancelTokSSrc.Token, cancelTokSSrc.Token);

            Thread.Sleep(2000);

            try
            {
                cancelTokSSrc.Cancel(); // отменить задачу
                tsk.Wait();
            }
            catch (AggregateException exc)
            {
                if (tsk.IsCanceled)
                {
                    Console.WriteLine("Задача tsk отменена");
                }
            }
            finally
            {
                tsk.Dispose();
                cancelTokSSrc.Dispose();
            }
            Console.WriteLine("Основной поток завершен");
            Console.ReadLine();
        }

        static void Cancellation_()
        {
            // Отмена задачи с использованием опроса
            // Если задача сразу после старта отменена, возбудить OperationCanceledException
            static void F1(object value)
            {
                CancellationToken token = (CancellationToken)value;
                token.ThrowIfCancellationRequested();

                for (int i = 0; i < 10; i++)
                {
                    if (token.IsCancellationRequested) //Проверка, что задача отменена
                    {
                        Console.WriteLine("Stop task");
                        token.ThrowIfCancellationRequested();
                    }

                    Thread.Sleep(500);
                    Console.WriteLine(".");
                }
            }

            var cancellation = new CancellationTokenSource();
            CancellationToken token = cancellation.Token;
            Task task = new Task(F1, token);
            task.Start();
            Thread.Sleep(3000);
            cancellation.Cancel();
        }

        static void CancellationToken_()
        {
            // Параллельное выполнение задач может занимать много времени и может возникнуть необходимость прервать задачу
            // Для отмены операции надо создать и использовать токен
            // Вначале создается объект CancellationTokenSource, затем из него получаем сам токен
            // Чтобы отменить операцию, необходимо вызвать метод Cancel() у объекта CancellationTokenSource
            // В самой операции мы можем отловить выставление токена с помощью условной конструкции if (token.IsCancellationRequested)
            // Если был вызван метод cancelTokenSource.Cancel(), то выражение token.IsCancellationRequested возвращает true
            var cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            int number = 6;

            var task1 = new Task(() =>
            {
                int result = 1;

                for (int i = 1; i <= number; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Операция прервана");
                        return;
                    }

                    result *= i;
                    Console.WriteLine($"Факториал числа {number} равен {result}");
                    Thread.Sleep(5000);
                }
            });
            task1.Start();

            Console.WriteLine("Введите Y для отмены операции или другой символ для ее продолжения:");
            string s = Console.ReadLine();

            if (s == "Y")
            {
                cancelTokenSource.Cancel();
            }
        }

        static void CancellationTokenSource_()
        {
            // Если операция представляет внешний метод, то ему надо передавать в качестве одного из параметров токен
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
            var task1 = new Task(() => F1(5, token));
            task1.Start();
            Console.WriteLine("Введите Y для отмены операции или любой другой символ для ее продолжения:");
            string s = Console.ReadLine();

            if (s == "Y")
            { 
                cancelTokenSource.Cancel();
            }
        }

        static async Task CancellationTokenTimeout_(TimeSpan timeout)
        {
            // Входящий аргумент TimeSpan.FromSeconds(5)

            // Создаем CancellationTokenSource с заданным таймаутом
            using (var cancellationTokenSource = new CancellationTokenSource(timeout))
            {
                CancellationToken token = cancellationTokenSource.Token;

                try
                {
                    // Запускаем задачу, которая может быть отменена
                    await Task.Run(() => LongRunningOperation(token), token);
                    Console.WriteLine("Операция завершена успешно.");
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Операция была отменена по таймауту.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла ошибка: {ex.Message}");
                }
            }

            static void LongRunningOperation(CancellationToken token)
            {
                for (int i = 0; i < 10; i++)
                {
                    // Проверяем, отменен ли токен
                    token.ThrowIfCancellationRequested();

                    // Имитация длительной работы
                    Console.WriteLine($"Работа {i + 1}/10...");
                    Thread.Sleep(1000);
                }
            }
        }

        static void CancelParallelOperations_()
        {
            // Для отмены выполнения параллельных операций, запущенных с методами Parallel.For() и Parallel.ForEach(),
            // можно использовать перегруженные версии данных методов,
            // которые принимают объект ParallelOptions
            // Данный объект позволяет установить токен
            // В параллельной запущеной задаче через 400 миллисекунд происходит вызов cancelTokenSource.Cancel(),
            // в результате программа выбрасывает исключение OperationCanceledException
            // и выполнение параллельных операций прекращается
            static void F1(int x)
            {
                int result = 1;

                for (int i = 1; i <= x; i++) 
                { 
                    result *= i; 
                }

                Console.WriteLine($"Факториал числа {x} равен {result}");

                Thread.Sleep(3000);
            }

            var cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            new Task(() =>
            {
                Thread.Sleep(400);
                cancelTokenSource.Cancel();
            }).Start();

            try
            {
                Parallel.ForEach<int>(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 },
                                      new ParallelOptions { CancellationToken = token }, F1);
                // или так
                //Parallel.For(1, 8, new ParallelOptions { CancellationToken = token }, Factorial);
            }
            catch (OperationCanceledException ex) 
            { 
                Console.WriteLine("Операция прервана"); 
            }
            finally 
            { 
                cancelTokenSource.Dispose(); 
            }
        }

        static void Checked_()
        {
            static int F1()
            {
                byte result = 255;
                checked { result += 1; }
                return result;
            }

            var task = new Task<int>(F1);
            Action<Task<int>> continuation;
            continuation = t => Console.WriteLine(task.Result);

            // Если предыдущая задача выполнилась с ошибкой, то продолжение НЕ выполняем (без checked в F10)
            task.ContinueWith(continuation, TaskContinuationOptions.OnlyOnRanToCompletion);
            continuation = t => Console.WriteLine(task.Result);

            // Если предыдущая задача выполнилась с ошибкой, то продолжение выполняем (с checked в F10)
            task.ContinueWith(continuation, TaskContinuationOptions.OnlyOnFaulted);

            task.Start();
        }

        static void TaskFactory_()
        {
            static void F1(object o)
            {
                int i = 1000;
                while (i-- > 0) { Console.Write(o); }
            }

            Task.Factory.StartNew(() => F1('q'));    // 1й поток
            var task = new Task(() => F1('w'));      // 2й поток, эквивалентно предыдущей записи
            task.Start();
        }

        static void TaskFactory_2_()
        {
            static int F1(object o)
            {
                Console.WriteLine(Task.CurrentId + " " + o);
                return o.ToString().Length;
            }

            var task = new Task<int>(F1, "text1");
            task.Start();
            Task<int> task_2 = Task.Factory.StartNew(F1, "text2");
            Console.WriteLine(task + " " + task.Result);
            Console.WriteLine(task_2 + " " + task_2.Result);
        }

        static void WaitAll_()
        {
            var task = new Task(ThreadSleep);
            var task_1 = new Task(ThreadSleep);
            task.Start();
            task_1.Start();
            Task.WaitAll(task, task_1); // Ждем завершения всех указанных Task
            //Task.WaitAny(task, task_1); // Ждем завершения любой из указанных Task
        }

        static void Factory_()
        {
            // Запуск task через Start не требуется
            var factory = new TaskFactory();
            Task task = factory.StartNew(ThreadSleep);
        }

        static void Lambda_()
        {
            Task task = Task.Factory.StartNew(new Action(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(200);
                    Console.WriteLine("Lambda");
                }
            }));
            task.Wait();    // Ожидание завершения
            task.Dispose(); // Освобождение памяти
        }

        static void ParallelInvoke_()
        {
            // Класс Parallel сам берет потоки из пула потоков и управляет конкуренцией
            // Нужно использовать делегаты System.Func<T> и System.Action<T> для указания целевого метода,
            // который будет вызываться для обработки данных
            // Action<T> ничего не принимает и ничего не возвращает
            // Func<T> возвращает значение и принимает различное количество аргументов

            // Распараллеливание задач методом Invoke
            // Метод Invoke() выполняет один или несколько методов, указываемых в виде его аргументов
            // Выполняемые методы должны быть совместимы с делегатом Action -
            // не должны принимать параметры и не должны возвращать значение
            // Метод Invoke() сначала инициирует выполнение, а затем ожидает завершения всех методов
            // Нельзя указать порядок выполнения методов

            // В приведенном ниже примере демонстрируется применение метода Invoke()
            // Методы F1() и F2() выполняются параллельно посредством вызова метода Invoke()
            // Выполнение метода Main приостанавливается, пока не произойдет возврат из метода Invoke

            static void F1()
            {
                Console.WriteLine("F1 begin");

                for (int count = 0; count < 5; count++)
                {
                    Thread.Sleep(500);
                    Console.WriteLine("--> F1 Count=" + count);
                }

                Console.WriteLine("F1 end");
            }

            static void F2()
            {
                Console.WriteLine("F2 begin");

                for (int count = 0; count < 5; count++)
                {
                    Thread.Sleep(500);
                    Console.WriteLine("--> F2 Count=" + count);
                }

                Console.WriteLine("F2 end");
            }

            Console.WriteLine("Main thread begin");
            Parallel.Invoke(F1, F2); // Выполнить параллельно оба метода
            Console.WriteLine("Main thread end");
        }

        static void ParallelFor_()
        {
            // В программе ниже создается массив data, состоящий из 1.000.000.000 целых значений
            // Затем вызывается метод For(), которому передается метод MyTransform()
            // Метод состоит из ряда операторов, выполняющих произвольные преобразования в массиве data

            // Программа состоит из двух циклов
            // В первом, стандартном, цикле for инициализируется массив data
            // Во втором, выполняемом параллельно методом For(),
            // над каждым элементом массива data производится преобразование
            // Метод For автоматически разбивает вызовы метода MyTransform на части для параллельной обработки

            // Не все циклы могут выполняться эффективно, когда они распараллеливаются
            // Мелкие циклы и циклы, состоящие из простых операций, выполняются быстрее последовательным способом

            // Метод For() возвращает экземпляр объекта типа ParallelLoopResult
            // Это структура, в которой определяются два свойства IsCompleted и LowestBreakIteration
            // IsCompleted будет true, если выполнены все шаги цикла
            // LowestBreakIteration содержит наименьшее значение переменной управления циклом,
            // если цикл прервется раньше времени через ParallelLoopState.Break

            static void F1(int i)
            {
                data[i] = data[i] / 10;
                if (data[i] <  10000) data[i] = 0;
                if (data[i] >= 10000) data[i] = 100;
                if (data[i] >  20000) data[i] = 200;
                if (data[i] >  30000) data[i] = 300;
            }

            Console.WriteLine("Main thread begin");
            data = new int[100000000];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = i;
            }

            Parallel.For(0, data.Length, F1);
            Console.WriteLine("Main thread end");

            // Первый параметр задает начальный индекс элемента в цикле
            // Второй - конечный индекс
            // Третий - делегат Action, указывает на метод, который будет выполняться раз за итерацию
            // В данном случае в качестве первого параметра в метод Parallel.For передается число 1
            // В качестве второго - число 10
            // Метод будет вести итерацию с 1 до 9 включительно
            // Третий параметр представляет метод, подсчитывающий факториал числа
            // Так как этот параметр представляет тип Action<int>, то этот метод в качестве параметра должен принимать объект int
            // В качестве значения параметра в этот метод передается счетчик, который проходит в цикле от 1 до 9 включительно
            // Метод Factorial вызывается 9 раз
            
            static void F2(int x)
            {
                int result = 1;

                for (int i = 1; i <= x; i++)
                {
                    result *= i;
                }

                Console.WriteLine($"Выполняется задача {Task.CurrentId}");
                Console.WriteLine($"Факториал числа {x} равен {result}");
                Thread.Sleep(3000);
            }

            Parallel.For(1, 10, F2);
        }

        static void ForEach_()
        {
            // Метод Parallel.ForEach осуществляет итерацию по коллекции, реализующей интерфейс IEnumerable
            // Цикл можно остановить, вызвав ParallelLoopState.Break

            static ParallelLoopResult F1<TSource>(IEnumerable<TSource> source,
                                                  Action<TSource, ParallelLoopState> body)
            {
                return new ParallelLoopResult();
            }

            // Метод возвращает структуру ParallelLoopResult, которая содержит информацию о выполнении цикла
            // Поскольку используем коллекцию объектов int, то метод, передаваемый в качестве второго параметра,
            // должен принимать int

            // Здесь myList - это коллекция аргументов, которые передаем методу F1 на каждой итерации цикла
            // Они не обязательно будут передаваться в том порядке, в котором указаны в списке

            static void F2(int x)
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
            ParallelLoopResult result = Parallel.ForEach<int>(myList, F2);
        }

        static void InsertedTasks_()
        {
            // Одна задача может запускать другую - вложенную задачу, эти задачи выполняются независимо
            // друг от друга и вложенная задача может завершить выполнение после завершения метода Main
            static void F1()
            {
                var outer = Task.Factory.StartNew(() =>      // внешняя задача
                {
                    Console.WriteLine("Outer task starting...");

                    var inner = Task.Factory.StartNew(() =>  // вложенная задача
                    {
                        Console.WriteLine("Inner task starting...");
                        Thread.Sleep(2000);
                        Console.WriteLine("Inner task finished.");
                    });
                });

                outer.Wait(); // ожидаем выполнения внешней задачи
                Console.WriteLine("End of Main");
            }

            // Если необходимо, чтобы вложенная задача выполнялась вместе с внешней, необходимо
            // использовать значение TaskCreationOptions.AttachedToParent:
            static void F2()
            {
                var outer = Task.Factory.StartNew(() =>      // внешняя задача
                {
                    Console.WriteLine("Outer task starting...");

                    var inner = Task.Factory.StartNew(() =>  // вложенная задача
                    {
                        Console.WriteLine("Inner task starting...");
                        Thread.Sleep(2000);
                        Console.WriteLine("Inner task finished.");
                    }, TaskCreationOptions.AttachedToParent);
                });

                outer.Wait(); // ожидаем выполнения внешней задачи
                Console.WriteLine("End of Main");
            }
        }

        static void TasksArray_()
        {
            // Как и с потоками, можно создать и запустить массив задач
            // Можно определить все задачи в массиве через объект Task
            static void F1()
            {
                var tasks = new Task[]
                {
                    new Task(() => Console.WriteLine("First Task")),
                    new Task(() => Console.WriteLine("Second Task")),
                    new Task(() => Console.WriteLine("Third Task"))
                };

                foreach (var t in tasks) // Запуск задач в массиве
                {
                    t.Start();
                }
            }

            // Либо также можно использовать методы Task.Factory.StartNew или Task.Run и сразу запускать все задачи
            static void F2()
            {
                var tasks = new Task[3];
                int j = 1;
                for (int i = 0; i < tasks.Length; i++)
                    tasks[i] = Task.Factory.StartNew(() => Console.WriteLine($"Task {j++}"));
            }

            // Можно столкнуться с тем, что все задачи из массива могут завершиться после того,
            // как отработает метод Main, в котором запускаются эти задачи
            static void F3()
            {
                var tasks = new Task[3]
                {
                    new Task(() => Console.WriteLine("First Task")),
                    new Task(() => Console.WriteLine("Second Task")),
                    new Task(() => Console.WriteLine("Third Task"))
                };

                foreach (var t in tasks)
                {
                    t.Start();
                }

                var tasks2 = new Task[3];
                int j = 1;

                for (int i = 0; i < tasks2.Length; i++)
                {
                    tasks2[i] = Task.Factory.StartNew(() => Console.WriteLine($"Task {j++}"));
                }

                Console.WriteLine("Завершение метода Main");
            }

            // Если необходимо выполнять некоторый код после того как все задачи массива завершатся,
            // то применяется метод Task.WaitAll(tasks)
            // В этом случае сначала завершатся все задачи, и потом будет выполняться следующий код из метода Main
            // Можно применить метод Task.WaitAny(tasks) - он ждет завершения хотя бы одной задачи из массива
            var tasks = new Task[3]
            {
                new Task(() => Console.WriteLine("First Task")),
                new Task(() => Console.WriteLine("Second Task")),
                new Task(() => Console.WriteLine("Third Task"))
            };

            foreach (var t in tasks)
            {
                t.Start();
            }

            Task.WaitAll(tasks); // Ожидаем завершения задач 
            Console.WriteLine("Завершение метода Main");
        }

        static void ParallelClass_()
        {
            // Класс Parallel также является частью TPL и предназначен для упрощения параллельного выполнения кода
            // Parallel имеет ряд методов, которые позволяют распараллелить задачу
            // Одним из методов, позволяющих параллельное выполнение задач, является метод Invoke
            // Метод Parallel.Invoke в качестве параметра принимает массив объектов Action
            // то есть можем передать в данный метод набор методов, которые будут вызываться при его выполнении
            // Количество методов может быть различным, но в данном случае мы определяем выполнение трех методов
            // Опять же как и в случае с классом Task мы можем передать либо название метода, либо лямбда-выражение
            // При наличии нескольких ядер данные методы будут выполняться параллельно на различных ядрах
            static void F1()
            {
                Console.WriteLine($"Выполняется задача {Task.CurrentId}");
                Thread.Sleep(3000);
            }

            static void F2(int x)
            {
                int result = 1;

                for (int i = 1; i <= x; i++) 
                { 
                    result *= i; 
                }

                Console.WriteLine($"Выполняется задача {Task.CurrentId}");
                Thread.Sleep(3000);
                Console.WriteLine($"Результат {result}");
            }

            Parallel.Invoke(F1, () =>
            {
                Console.WriteLine($"Выполняется задача {Task.CurrentId}");
                Thread.Sleep(3000);
            }, () => F2(5));
        }

        static void BreakCycle_()
        {
            // В стандартных циклах for и foreach предусмотрен преждевременный выход из цикла с помощью оператора break
            // В методах Parallel.ForEach и Parallel.For мы также можем, не дожидаясь окончания цикла, выйти из него
            // Здесь метод Factorial, обрабатывающий каждую итерацию, принимает дополнительный параметр - объект ParallelLoopState
            // Если счетчик в цикле достигнет значения 5, вызывается метод Break
            // Благодаря чему система осуществит выход и прекратит выполнение метода Parallel.For при первом удобном случае
            // Методы Parallel.ForEach и Parallel.For возвращают объект ParallelLoopResult, наиболее значимыми свойствами которого являются
            // IsCompleted - определяет, завершилось ли полное выполнение параллельного цикла
            // LowestBreakIteration - возвращает индекс, на котором произошло прерывание работы цикла
            // Так как на 5 индексе происходит прерывание, то свойство IsCompleted будет иметь значение false,
            // а LowestBreakIteration будет равно 5
            static void F1(int x, ParallelLoopState pls)
            {
                int result = 1;

                for (int i = 1; i <= x; i++)
                {
                    result *= i;

                    if (i == 5)
                    {
                        pls.Break();
                    }
                }

                Console.WriteLine($"Выполняется задача {Task.CurrentId}");
                Console.WriteLine($"Факториал числа {x} равен {result}");
            }
            ParallelLoopResult result = Parallel.For(1, 10, F1);

            if (!result.IsCompleted)
            {
                Console.WriteLine($"Выполнение цикла завершено на итерации {result.LowestBreakIteration}");
            }
        }

        static void TaskCompletionSource_()
        {
            // Используется для создания и управления асинхронными операциями

            // Может быть использован, когда:
            // - необходимо выполнить асинхронную операцию
            // - необходимо преобразовать существующую синхронную операцию в асинхронную
            // - необходимо объединить результаты нескольких асинхронных операций в одну

            // Создаем объект TaskCompletionSource и запускаем асинхронную операцию в отдельном потоке
            // После завершения операции устанавливаем результат в "Hello, world!"
            // Возвращаем объект Task

            async Task<string> F1()
            {
                var tcs = new TaskCompletionSource<string>();

                Task.Run(async () =>
                {
                    await Task.Delay(5000);
                    
                    tcs.SetResult("Hello, world!");
                });

                return await tcs.Task;
            }
        }

        static void AsyncCombinators_()
        {
            // Асинхронные комбинаторы позволяют комбинировать асинхронные операции и
            // совместно использовать результаты нескольких асинхронных операций,
            // что упрощает код и повышает производительность

            static void F1() { }
            static void F2() { }
            static void F3() { }

            static async Task WhenAll()
            {
                // Запускаем три асинхронные операции и ждем, пока все они завершатся

                var task1 = Task.Run(() => F1());
                var task2 = Task.Run(() => F2());
                var task3 = Task.Run(() => F3());

                await Task.WhenAll(task1, task2, task3);
            }

            static async Task WhenAny()
            {
                // Запускаем три асинхронные операции и ждем, пока хотя бы одна завершится
                // Затем определяем, какая из операций завершилась, и выполняем соответствующие действия

                var task1 = Task.Run(() => F1());
                var task2 = Task.Run(() => F2());
                var task3 = Task.Run(() => F3());

                var completedTask = await Task.WhenAny(task1, task2, task3);

                if (completedTask == task1) { /* .. */ }
                else if (completedTask == task2) { /* .. */ }
                else if (completedTask == task3) { /* .. */ }
            }

            static async Task ContinueWith()
            {
                // Запускаем асинхронную операцию и задаем продолжение, которое будет выполнено после ее завершения
                // Продолжение может выполнять любые действия, включая обработку результатов операции

                var task1 = Task.Run(() => F1());

                var continuationTask = task1.ContinueWith((t) =>
                {
                    // ..
                });

                await continuationTask;
            }

        }

        static void AsyncEnumerable_()
        {
            // Асинхронный итератор

            // Метод возвращает асинхронный итератор, который использует оператор yield return
            // для возврата элементов коллекции по мере их готовности
            async IAsyncEnumerable<int> GetNumbersAsync()
            {
                for (int i = 0; i < 10; i++)
                {
                    await Task.Delay(100);
                    yield return i;
                }
            }

            // Метод использует оператор await foreach для эффективного перебора
            // элементов коллекции в асинхронном коде
            async Task PrintNumbersAsync()
            {
                await foreach (var number in GetNumbersAsync())
                {
                    Console.WriteLine(number);
                }
            }
        }
    }
}
