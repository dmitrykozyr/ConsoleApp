using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SharpEdu
{
    public class Tasks
    {
        #region==== Общее ===========================================================
        // TPL упрощает создание и применение потоков
        // Позволяет автоматически использовать несколько процессоров
        // Позволяет автоматически распределять нагрузку приложений между доступными процессорами, используя пул потоков
        // Класс Task отличается от класса Thread тем, что является абстракцией, представляющей асинхронную операцию,
        // а в классе Thread инкапсулируется поток исполнения
        // Исполнением задач управляет планировщик задач, который работает с пулом потоков,
        // поэтому несколько задач могут разделять один поток
        // В отличие от класса Thread, в классе Task нет свойства Name для хранения имени задачи, но есть свойство Id
        // Каждая задача получает идентификатор при создании, значения уникальны, но не упорядочены,
        // поэтому один идентификатор задачи может появиться перед другим и может и не иметь меньшее значение
        
        // Возвращает исполняемую задачу или пустое значение, если вызывающий код не является задачей
        // В классе Task реализуется интерфейс IDisposable
        // Обычно ресурсы, связанные с классом Task, освобождаются автоматически сборщиком мусора
        // Этот метод можно вызывать для отдельной задачи только после ее завершения, которое можно отследить методом Wait
        // Если вызвать Dispose для активной задачи, возникнет исключение InvalidOperationException

        // Задача получения результата блокирует вызывающий код, пока результат не будет вычислен
        public static void F1()
        {
            static void F1_1()
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
            Task task1 = new Task(F1_1);    // Создание Task
            Task task2 = new Task(F1_1);
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
            Task t1 = tf.StartNew(F1_1);

            // Фабрика задач через задачу
            Task t2 = Task.Factory.StartNew(F1_1);

            // Конструктор Task
            Task t3 = new Task(F1_1);
            t3.Start();

            // Лямбда-выражение
            Task t4 = Task.Factory.StartNew(() => {
                for (int count = 0; count < 10; count++)
                {
                    Thread.Sleep(500);
                }
            });
            t4.Wait();
            t4.Dispose();

            // Еще способ с лямбдой
            Task task = new Task(() => Console.WriteLine("Hello Task!"));
            task.Start();

            Console.WriteLine("Main thread was finished, task id " + Task.CurrentId); Console.ReadLine();
        }
        #endregion

        #region==== Task ============================================================
        // Task - использует пул потоков
        // Если false (по умолчанию), то после завершения main ожидается завершение недовыполненных task, иначе они прерываются
        // Thread.CurrentThread.IsBackground = true;
        public static void F26()
        {
            static void F1_1()
            {
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(500);
                    Console.WriteLine("Thread Id " + Thread.CurrentThread.ManagedThreadId);
                    Console.WriteLine("TaskId " + Task.CurrentId);
                }
            }
            
            Action action = new Action(F1_1);
            Task task = new Task(action);
            Task task_2 = new Task(action);
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
        #endregion

        #region==== Методы с аргументами ============================================        
        public static void F2()
        {
            // object нужен для работы с методами с аргументам
            static void F2_2(object obj) { Console.WriteLine("F2 " + obj.ToString()); }
            Action<object> action = new Action<object>(F2_2);
            Task task = new Task(action, "second argument");
            task.Start();
            Thread.Sleep(500);
            Console.WriteLine(task.AsyncState as string);
            // Аналог Join() - останавливает первичный поток, пока не завершится вторичный
            task.Wait();

            // Организация задержки
            while (!task.IsCompleted)
                Thread.Sleep(200);

            // Аналог Join
            IAsyncResult asyncResult = task as IAsyncResult;
            ManualResetEvent waitHandle = asyncResult.AsyncWaitHandle as ManualResetEvent;
            waitHandle.WaitOne();
        }
        #endregion

        #region==== Continuation ====================================================
        // Автоматический запуск новой задачи после завершения предыдушей
        // Такой метод должен принимать аргумент типа Task
        public static void F6()
        {
            static void F6_1(Task task)
            {
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(500);
                    Console.WriteLine("--");
                }
            }

            Action action = new Action(F1);                 // Создание 1й задачи
            Task task = new Task(action);
            Action<Task> action_1 = new Action<Task>(F6_1); // Создание продолжения задачи
            Task task_2 = task.ContinueWith(action_1);
            task.Start();                                   // Последовательный запуск
            Console.ReadKey();
        }
        #endregion

        #region==== Задачи продолжения ==============================================
        // Задачи продолжения или continuation task позволяют определить задачи, которые выполняются после завершения других задач
        // Можем вызвать после выполнения одной задачи несколько других, определить условия их вызова,
        // передать из предыдущей задачи в следующую некоторые данные
        // Задачи продолжения похожи на методы обратного вызова, но являются обычными задачами Task
        public static void F8()
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

        // Первая задача задается с помощью лямбда-выражения, которое просто выводит id этой задачи
        // Вторая задача - задача продолжения задается с помощью метода ContinueWith, который в качестве параметра принимает делегат Action<Task>
        // То есть метод Display, который передается в данный метод в качестве значения параметра, должен принимать параметр типа Task
        // Благодаря передачи в метод параметра Task, мы можем получить различные свойства предыдущей задачи, как например, в данном случае получает ее Id
        // И после завершения задачи task1 сразу будет вызываться задача task2
        // Также мы можем передавать конкретный результат работы предыдущей задачи
        public static void F8_1()
        {
            static int Sum(int a, int b) => a + b;
            static void F8_2(int sum)
            {
                Console.WriteLine($"Sum: {sum}");
            }

            Task<int> task1 = new Task<int>(() => Sum(4, 5));

            // задача продолжения
            Task task2 = task1.ContinueWith(sum => F8_2(sum.Result));
            task1.Start();

            // ждем окончания второй задачи
            task2.Wait();
            Console.WriteLine("End of Main");
        }

        // Подобным образом можно построить целую цепочку последовательно выполняющихся задач:
        public static void F8_2()
        {
            static void F9_1(Task t)
            {
                Console.WriteLine($"Id задачи: {Task.CurrentId}");
            }

            Task task1 = new Task(() => {
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
        #endregion

        #region==== Возвращение значений из Task ====================================
        // Для передачи аргументов упаковываем их в структуру и чередаем через object
        public struct Struct_1
        {
            public int a;
            public int b;
        }

        public static void F17()
        {
            static int F17_1(object value)
            {
                int a = ((Struct_1)value).a;
                int b = ((Struct_1)value).b;
                Thread.Sleep(500);
                return a + b;
            }

            Struct_1 struct_1;
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
        #endregion

        #region==== Возвращение результатов из задач ================================
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
        public static void F7()
        {
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
                    result *= i;
                return result;
            }
        }
        #endregion

        #region==== Вызов исключений в задачах ======================================
        public static void F9()
        {
            static void F9_1()
            {
                Console.WriteLine("Begin");
                throw new Exception();
                Console.WriteLine("End");
            }

            Task task = new Task(F9_1);
            try
            {
                task.Start();
                task.Wait(); // Wait необходим для обработки исключения
            }
            catch (Exception ex)
            {
                Console.WriteLine("1: " + ex.GetType() + " " + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("2: " + ex.InnerException);
            }
            finally { Console.WriteLine("3: " + task.Status); }
        }
        #endregion

        #region==== Отмена задачи ===================================================
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
        public static void F10()
        {
            static void F2_2(Object ct)
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
            Task tsk = Task.Factory.StartNew(F2_2, cancelTokSSrc.Token, cancelTokSSrc.Token);

            Thread.Sleep(2000);
            try
            {                
                cancelTokSSrc.Cancel(); // отменить задачу
                tsk.Wait();
            }
            catch (AggregateException exc)
            {
                if (tsk.IsCanceled)
                    Console.WriteLine("Задача tsk отменена");
            }
            finally
            {
                tsk.Dispose();
                cancelTokSSrc.Dispose();
            }
            Console.WriteLine("Основной поток завершен");
            Console.ReadLine();
        }
        #endregion

        #region==== Cancellation ====================================================
        // Отмена задачи с использованием опроса
        // Если задача сразу после старта отменена, возбудить OperationCanceledException
        public static void F11()
        {
            static void F11_1(object value)
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

            CancellationTokenSource cancellation = new CancellationTokenSource();
            CancellationToken token = cancellation.Token;
            Task task = new Task(F11_1, token);
            task.Start();
            Thread.Sleep(3000);
            cancellation.Cancel();
        }
        #endregion

        #region==== Отмена задач и параллельных операций. CancellationToken =========
        // Параллельное выполнение задач может занимать много времени и может возникнуть необходимость прервать задачу
        // Для отмены операции надо создать и использовать токен
        // Вначале создается объект CancellationTokenSource, затем из него получаем сам токен
        // Чтобы отменить операцию, необходимо вызвать метод Cancel() у объекта CancellationTokenSource
        // В самой операции мы можем отловить выставление токена с помощью условной конструкции if (token.IsCancellationRequested)
        // Если был вызван метод cancelTokenSource.Cancel(), то выражение token.IsCancellationRequested возвращает true
        public static void F13()
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            int number = 6;

            Task task1 = new Task(() =>
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
                cancelTokenSource.Cancel();

            Console.Read();
        }

        // Если операция представляет внешний метод, то ему надо передавать в качестве одного из параметров токен:
        public static void F13_1()
        {
            static void F13_2(int x, CancellationToken token)
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
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            Task task1 = new Task(() => F13_2(5, token));
            task1.Start();
            Console.WriteLine("Введите Y для отмены операции или любой другой символ для ее продолжения:");
            string s = Console.ReadLine();
            if (s == "Y")
                cancelTokenSource.Cancel();
        }
        #endregion

        #region==== Отмена параллельных операций Parallel ===========================
        // Для отмены выполнения параллельных операций, запущенных с методами Parallel.For() и Parallel.ForEach(),
        // можно использовать перегруженные версии данных методов,
        // которые принимают объект ParallelOptions
        // Данный объект позволяет установить токен
        // В параллельной запущеной задаче через 400 миллисекунд происходит вызов cancelTokenSource.Cancel(),
        // в результате программа выбрасывает исключение OperationCanceledException
        // и выполнение параллельных операций прекращается
        public static void F14()
        {
            static void F14_1(int x)
            {
                int result = 1;
                for (int i = 1; i <= x; i++) { result *= i; }
                Console.WriteLine($"Факториал числа {x} равен {result}");
                Thread.Sleep(3000);
            }

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            new Task(() =>
            {
                Thread.Sleep(400);
                cancelTokenSource.Cancel();
            }).Start();

            try
            {
                Parallel.ForEach<int>(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 },
                                      new ParallelOptions { CancellationToken = token }, F14_1);
                // или так
                //Parallel.For(1, 8, new ParallelOptions { CancellationToken = token }, Factorial);
            }
            catch (OperationCanceledException ex) { Console.WriteLine("Операция прервана"); }
            finally { cancelTokenSource.Dispose(); }
            Console.ReadLine();
        }
        #endregion

        #region==== Checked =========================================================
        public static void F12()
        {
            static int F12_1()
            {
                byte result = 255;
                checked { result += 1; }
                return result;
            }

            Task<int> task = new Task<int>(F12_1);
            Action<Task<int>> continuation;
            continuation = t => Console.WriteLine(task.Result);

            // Если предыдущая задача выполнилась с ошибкой, то продолжение НЕ выполняем (без checked в F10)
            task.ContinueWith(continuation, TaskContinuationOptions.OnlyOnRanToCompletion);
            continuation = t => Console.WriteLine(task.Result);

            // Если предыдущая задача выполнилась с ошибкой, то продолжение выполняем (с checked в F10)
            task.ContinueWith(continuation, TaskContinuationOptions.OnlyOnFaulted);

            task.Start();
            Console.ReadKey();
        }
        #endregion

        #region==== TaskFactory =====================================================
        public static void F15()
        {
            static void F15_1(char c)
            {
                int i = 1000;
                while (i-- > 0) { Console.Write(c); }
            }

            Task.Factory.StartNew(() => F15_1('q'));    // 1й поток
            var task = new Task(() => F15_1('w'));      // 2й поток, эквивалентно предыдущей записи
            task.Start();
        }
        #endregion

        #region==== TaskFactory =====================================================
        public static void F16()
        {
            static void F16_1(object o)
            {
                int i = 1000;
                while (i-- > 0) { Console.Write(o); }
            }

            Task task = new Task(F16_1, "F16");
            task.Start();
            Task.Factory.StartNew(F16_1, 123);
        }
        #endregion

        #region==== TaskFactory =====================================================
        public static void F18()
        {
            static int F18_1(object o)
            {
                Console.WriteLine(Task.CurrentId + " " + o);
                return o.ToString().Length;
            }

            string text1 = "text1";
            string text2 = "text2";
            var task = new Task<int>(F18_1, text1);
            task.Start();
            Task<int> task_2 = Task.Factory.StartNew<int>(F18_1, text2);
            Console.WriteLine(task + " " + task.Result);
            Console.WriteLine(task_2 + " " + task_2.Result);
        }
        #endregion

        #region==== Wait All ========================================================
        public static void F19()
        {
            Task task = new Task(F1);
            Task task_1 = new Task(F1);
            task.Start();
            task_1.Start();
            Task.WaitAll(task, task_1); // Ждем завершения всех указанных Task
            //Task.WaitAny(task, task_1); // Ждем завершения любой из указанных Task
        }
        #endregion

        #region==== Factory =========================================================        
        public static void F20() // Запуск task через Start не требуется
        {
            TaskFactory factory = new TaskFactory();
            Task task = factory.StartNew(F1);
        }
        #endregion

        #region==== Lambda ==========================================================
        public static void F21()
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
        #endregion

        #region==== Класс Parallel ==================================================
        // Одним из главных классов в TPL является System.Threading.Tasks.Parallel
        // Содержит методы, которые позволяют выполнять итерации по объектам, реализующим IEnumerable<T> в параллельном режиме
        // Поддерживает методы Parallel.For() и Parallel.ForEach()
        // Позволяют создавать тело операторов кода, которое может выполнятся в параллельном режиме
        // Представляют собой логику того же рода, которая была бы написана с использованием for и foreach
        // Класс Parallel сам берет потоки из пула потоков и управляет конкуренцией
        // Оба метода требуют указания совместимого с IEnumerable или IEnumerable<T> контейнера, хранящего данные,
        // которые нужно обработать в параллельном режиме
        // Контейнер может быть массивом, необобщенной коллекцией (вроде ArrayList), обобщенной коллекцией (наподобие List<T>) или результатом запроса LINQ
        // Нужно будет использовать делегаты System.Func<T> и System.Action<T> для указания целевого метода, который будет вызываться для обработки данных
        // Func<T> представляет метод, который возвращает значение и принимает различное количество аргументов
        // Делегат Action<T> похож на Func<T> в том, что позволяет указывать метод, принимающий несколько параметров
        // Однако Action<T> указывает метод, который может возвращать только void
        // Распараллеливание задач методом Invoke
        // Метод Invoke(), определенный в классе Parallel, позволяет выполнять один или несколько методов, указываемых в виде его аргументов
        // Также масштабирует исполнение кода, ьиспользуя доступные процессоры, если есть возможность
        // Выполняемые методы должны быть совместимы с описанным ранее делегатом Action.Следовательно, каждый метод, передаваемый методу Invoke() в качестве аргумента,
        // не должен ни принимать параметров, ни возвращать значение
        // Благодаря тому что параметр actions данного метода относится к типу params, выполняемые методы могут быть указаны в виде переменного списка аргументов
        // Для этой цели можно также воспользоваться массивом объектов типа Action, но зачастую оказывается проще указать список аргументов
        // Метод Invoke() сначала инициирует выполнение, а затем ожидает завершения всех передаваемых ему методов
        // Это, в частности, избавляет от необходимости (да и не позволяет) вызывать метод Wait()
        // Все функции параллельного выполнения метод Invoke() берет на себя
        // И хотя это не гарантирует, что методы будут действительно выполняться параллельно, тем не менее
        // именно такое их выполнение предполагается, если система поддерживает несколько процессоров
        // Кроме того, отсутствует возможность указать порядок выполнения методов от первого и до последнего,
        // и этот порядок не может быть таким же, как и в списке аргументов
        // В приведенном ниже примере программы демонстрируется применение метода Invoke() на практике
        // В этой программе два метода MyMeth() и MyMeth2() выполняются параллельно посредством вызова метода Invoke()
        // Выполнение метода Main приостанавливается, пока не произойдет возврат из метода Invoke
        // Метод Main(), в отличие от методов MyMeth и MyMeth2, не выполняется параллельно
        // Поэтому применять метод Invoke() показанным здесь способом нельзя, если требуется,
        // чтобы исполнение вызывающего потока продолжалось
        public static void F3()
        {
            static void F3_1() // Методы, исполняемые как задача
            {
                Console.WriteLine("MyMeth запущен");
                for (int count = 0; count < 5; count++)
                {
                    Thread.Sleep(500);
                    Console.WriteLine("--> MyMeth Count=" + count);
                }
                Console.WriteLine("MyMeth завершен");
            }

            static void F3_2()
            {
                Console.WriteLine("MyMeth2 запущен");
                for (int count = 0; count < 5; count++)
                {
                    Thread.Sleep(500);
                    Console.WriteLine("--> MyMeth2 Count=" + count);
                }
                Console.WriteLine("MyMeth2 завершен");
            }

            Console.WriteLine("Основной поток запущен");
            Parallel.Invoke(F3_1, F3_2); // Выполнить параллельно оба именованных метода
            Console.WriteLine("Основной поток завершен");
        }
        #endregion

        #region==== Метод For =======================================================
        // public static ParallelLoopResult
        // For(int fromInclusive, int toExclusive, Action<int> body)
        // fromInclusive обозначает начальное значение того, что соответствует переменной управления циклом
        // называется также итерационным, или индексным, значением;
        // a toExclusive - значение, на единицу больше конечного
        // На каждом шаге цикла переменная управления циклом увеличивается на единицу

        // Циклически выполняемый код указывается методом, передаваемым через параметр body
        // Метод должен быть совместим с делегатом Action<int>

        // Для метода For() обобщенный параметр T должен быть типа int
        // Значение, передаваемое через параметр obj, будет следующим значением переменной управления циклом
        // А метод, передаваемый через параметр body, может быть именованным или анонимным
        // Метод For() возвращает экземпляр объекта типа ParallelLoopResult, описывающий состояние завершения цикла.
        // Для простых циклов этим значением можно пренебречь
        // Главная особенность метода For - он позволяет, когда такая возможность имеется, распараллелить исполнение кода в цикле

        // В программе ниже создается массив data, состоящий из 1.000.000.000 целых значений
        // Затем вызывается метод For(), которому в качестве "тела" цикла передается метод MyTransform()
        // Этот метод состоит из ряда операторов, выполняющих произвольные преобразования в массиве data
        // Его назначение - сымитировать конкретную операцию
        // Выполняемая операция должна быть нетривиальной, чтобы параллелизм данных принес положительный эффект

        // Программа состоит из двух циклов
        // В первом, стандартном, цикле for инициализируется массив data
        // Во втором, выполняемом параллельно методом For(), над каждым элементом массива data производится преобразование
        // Это преобразование носит произвольный характер и выбрано для демонстрации
        // Метод For автоматически разбивает вызовы метода MyTransform на части для параллельной обработки отдельных порций данных, хранящихся в массиве
        // Если запустить программу на компьютере с двумя доступными процессорами или больше, то цикл преобразования данных в массиве может быть выполнен методом For параллельно

        // Не все циклы могут выполняться эффективно, когда они распараллеливаются
        // Мелкие циклы и циклы, состоящие из простых операций, выполняются быстрее последовательным способом
        // Поэтому цикл for инициализации массива данных не распараллеливается методом For() в рассматриваемой программе

        // Метод For() возвращает экземпляр объекта типа ParallelLoopResult.Это структура, в которой определяются два следующих свойства:

        // public bool IsCompleted { get; }
        // public Nullable<long> LowestBreakIteration { get; }
        // Свойство IsCompleted будет иметь логическое значение true, если выполнены все шаги цикла
        // При нормальном завершении цикла это свойство будет содержать true
        // Если выполнение цикла прервется раньше времени, то данное свойство будет содержать false
        // Свойство LowestBreakIteration будет содержать наименьшее значение переменной управления циклом, если цикл прервется раньше времени вызовом метода ParallelLoopState.Break

        // Для доступа к объекту типа ParallelLoopState следует использовать форму метода For(), делегат которого принимает в качестве второго параметра текущее состояние цикла

        // public static ParallelLoopResult For(int fromInclusive, int toExclusive,
        // Action<int, ParallelLoopState> body)
        // В данной форме делегат Action, описывающий тело цикла, определяется следующим образом:

        // public delegate void Action<in T1, in T2>(T1 arg1, T2 arg2)
        // Для метода For() обобщенный параметр T1 должен быть типа int, а обобщенный параметр Т2 — типа ParallelLoopState
        // Всякий раз, когда делегат Action вызывается, текущее состояние цикла передается в качестве аргумента arg2

        // Для преждевременного завершения цикла следует воспользоваться методом Break(), вызываемым для экземпляра объекта типа ParallelLoopState внутри тела цикла, определяемого параметром body

        // public void Break()
        // Вызов метода Break() формирует запрос на как можно более раннее прекращение параллельно выполняемого цикла, что может произойти через несколько шагов цикла после вызова метода Break()
        // Все шаги цикла до вызова метода Break() все же выполняются
        // Отдельные части цикла могут и не выполняться параллельно
        // Так, если выполнено 10 шагов цикла, то это еще не означает, что все эти 10 шагов представляют 10 первых значений переменной управления циклом
        // Прерывание цикла, параллельно выполняемого методом For(), нередко оказывается полезным при поиске данных
        // Если искомое значение найдено, то продолжать выполнение цикла нет никакой надобности
        // Прерывание цикла может оказаться полезным и в том случае, если во время очередной операции встретились недостоверные данные
        static int[] data;
        public static void F4()
        {
            static void F4_1(int i)
            {
                data[i] = data[i] / 10;
                if (data[i] < 10000) data[i] = 0;
                if (data[i] >= 10000) data[i] = 100;
                if (data[i] > 20000) data[i] = 200;
                if (data[i] > 30000) data[i] = 300;
            }
            Console.WriteLine("Основной поток запущен");
            data = new int[100000000];
            for (int i = 0; i < data.Length; i++)
                data[i] = i;

            // Распараллелить цикл методом For()
            Parallel.For(0, data.Length, F4_1);
            Console.WriteLine("Основной поток завершен");
            Console.ReadLine();
        }
        #endregion

        #region==== Метод ForEach ===================================================
        // source обозначает коллекцию данных, обрабатываемых в цикле
        // body - метод, который будет выполняться на каждом шаге цикла
        // Метод, передаваемый через параметр body, принимает значение или ссылку на каждый обрабатываемый в цикле элемент массива, но не его индекс
        // Возвращаются сведения о состоянии цикла
        // Параллельное выполнение этого цикла можно остановить, вызвав метод Break для экземпляра объекта типа ParallelLoopState,
        // передаваемого через параметр body, если используется приведенная ниже форма метода ForEach():
        public static ParallelLoopResult ForEach<TSource>(
                        IEnumerable<TSource> source, 
                        Action<TSource, ParallelLoopState> body) 
        { 
            return new ParallelLoopResult(); 
        }
        #endregion

        #region==== Вложенные задачи ================================================
        // Одна задача может запускать другую - вложенную задачу, эти задачи выполняются независимо
        // друг от друга и вложенная задача может завершить выполнение после завершения метода Main
        public static void F5()
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
        public static void F5_1()
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
        #endregion

        #region==== Массив задач ====================================================
        // Как и с потоками, можно создать и запустить массив задач
        // Можно определить все задачи в массиве через объект Task
        public static void F22()
        {
            Task[] tasks1 = new Task[]
            {
                new Task(() => Console.WriteLine("First Task")),
                new Task(() => Console.WriteLine("Second Task")),
                new Task(() => Console.WriteLine("Third Task"))
            };
            
            foreach (var t in tasks1) // Запуск задач в массиве
                t.Start();
        }

        // Либо также можно использовать методы Task.Factory.StartNew или Task.Run и сразу запускать все задачи
        public static void F22_1()
        {
            Task[] tasks2 = new Task[3];
            int j = 1;
            for (int i = 0; i < tasks2.Length; i++)
                tasks2[i] = Task.Factory.StartNew(() => Console.WriteLine($"Task {j++}"));
        }

        // Можно столкнуться с тем, что все задачи из массива могут завершиться после того,
        // как отработает метод Main, в котором запускаются эти задачи
        public static void F22_2()
        {
            Task[] tasks1 = new Task[3]
            {
                new Task(() => Console.WriteLine("First Task")),
                new Task(() => Console.WriteLine("Second Task")),
                new Task(() => Console.WriteLine("Third Task"))
            };
            foreach (var t in tasks1)
                t.Start();

            Task[] tasks2 = new Task[3];
            int j = 1;
            for (int i = 0; i < tasks2.Length; i++)
                tasks2[i] = Task.Factory.StartNew(() => Console.WriteLine($"Task {j++}"));

            Console.WriteLine("Завершение метода Main");
        }

        // Если необходимо выполнять некоторый код после того как все задачи массива завершатся,
        // то применяется метод Task.WaitAll(tasks)
        // В этом случае сначала завершатся все задачи, и потом будет выполняться следующий код из метода Main
        // Можно применить метод Task.WaitAny(tasks) - он ждет завершения хотя бы одной задачи из массива
        public static void F22_3()
        {
            Task[] tasks1 = new Task[3]
            {
                new Task(() => Console.WriteLine("First Task")),
                new Task(() => Console.WriteLine("Second Task")),
                new Task(() => Console.WriteLine("Third Task"))
            };
            foreach (var t in tasks1)
                t.Start();
            Task.WaitAll(tasks1); // Ожидаем завершения задач 
            Console.WriteLine("Завершение метода Main");
            Console.ReadLine();
        }
        #endregion

        #region==== Класс Parallel ==================================================
        // Класс Parallel также является частью TPL и предназначен для упрощения параллельного выполнения кода
        // Parallel имеет ряд методов, которые позволяют распараллелить задачу
        // Одним из методов, позволяющих параллельное выполнение задач, является метод Invoke
        // Метод Parallel.Invoke в качестве параметра принимает массив объектов Action
        // то есть можем передать в данный метод набор методов, которые будут вызываться при его выполнении
        // Количество методов может быть различным, но в данном случае мы определяем выполнение трех методов
        // Опять же как и в случае с классом Task мы можем передать либо название метода, либо лямбда-выражение
        // При наличии нескольких ядер данные методы будут выполняться параллельно на различных ядрах
        public static void F_9()
        {
            static void F9_1()
            {
                Console.WriteLine($"Выполняется задача {Task.CurrentId}");
                Thread.Sleep(3000);
            }

            static void F9_2(int x)
            {
                int result = 1;
                for (int i = 1; i <= x; i++) { result *= i; }
                Console.WriteLine($"Выполняется задача {Task.CurrentId}");
                Thread.Sleep(3000);
                Console.WriteLine($"Результат {result}");
            }

            Parallel.Invoke(F9_1,
                () => {
                    Console.WriteLine($"Выполняется задача {Task.CurrentId}");
                    Thread.Sleep(3000);
                },
                () => F9_2(5));
        }
        #endregion

        #region==== Parallel.For ====================================================
        // Метод Parallel.For позволяет выполнять итерации цикла параллельно
        // Первый параметр задает начальный индекс элемента в цикле
        // Второй - конечный индекс
        // Третий - делегат Action, указывает на метод, который будет выполняться раз за итерацию
        // В данном случае в качестве первого параметра в метод Parallel.For передается число 1, а в качестве второго - число 10
        // Метод будет вести итерацию с 1 до 9 включительно
        // Третий параметр представляет метод, подсчитывающий факториал числа
        // Так как этот параметр представляет тип Action<int>, то этот метод в качестве параметра должен принимать объект int
        // В качестве значения параметра в этот метод передается счетчик, который проходит в цикле от 1 до 9 включительно
        // Метод Factorial вызывается 9 раз
        public static void F23()
        {
            static void F23_1(int x)
            {
                int result = 1;
                for (int i = 1; i <= x; i++) { result *= i; }
                Console.WriteLine($"Выполняется задача {Task.CurrentId}");
                Console.WriteLine($"Факториал числа {x} равен {result}");
                Thread.Sleep(3000);
            }
            Parallel.For(1, 10, F23_1);
        }
        #endregion

        #region==== Parallel.ForEach ================================================
        // Метод Parallel.ForEach осуществляет итерацию по коллекции, реализующей интерфейс IEnumerable, подобно циклу foreach, только осуществляет параллельное выполнение перебора
        // Первый параметр представляет перебираемую коллекцию,
        // Второй - делегат, выполняющийся один раз за итерацию для каждого перебираемого элемента коллекции
        // На выходе метод возвращает структуру ParallelLoopResult, которая содержит информацию о выполнении цикла
        // Поскольку мы используем коллекцию объектов int, то и метод, который мы передаем в качестве второго параметра,
        // должен принимать int
        public static void F24()
        {
            static void F24_1(int x)
            {
                int result = 1;
                for (int i = 1; i <= x; i++) { result *= i; }
                Console.WriteLine($"Выполняется задача {Task.CurrentId}");
                Console.WriteLine($"Факториал числа {x} равен {result}");
                Thread.Sleep(3000);
            }
            ParallelLoopResult result = Parallel.ForEach<int>(new List<int>() { 1, 3, 5, 8 }, F24_1);
        }
        #endregion

        #region==== Выход из цикла ==================================================
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
        public static void F25()
        {
            static void F25_1(int x, ParallelLoopState pls)
            {
                int result = 1;

                for (int i = 1; i <= x; i++)
                {
                    result *= i;
                    if (i == 5)
                        pls.Break();
                }
                Console.WriteLine($"Выполняется задача {Task.CurrentId}");
                Console.WriteLine($"Факториал числа {x} равен {result}");
            }
            ParallelLoopResult result = Parallel.For(1, 10, F25_1);

            if (!result.IsCompleted)
                Console.WriteLine($"Выполнение цикла завершено на итерации {result.LowestBreakIteration}");
        }
        #endregion

        static void Main_()
        {
            Console.ReadKey();
        }
    }
}
