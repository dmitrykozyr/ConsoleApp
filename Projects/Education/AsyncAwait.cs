using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SharpEdu
{
    public class AsyncAwait
    {
        #region Общее
        // Асинхронный метод не может определять параметры с модификаторами ref и out
        // Запускается метод F1, в котором вызывается асинхронный метод F1_2Async
        // Метод F1_2Async начинает выполняться синхронно вплоть до выражения await
        // Выражение await запускает асинхронную задачу Task.Run(()=>F1_1())
        // Пока выполняется асинхронная задача Task.Run(()=>F1_1()), выполнение кода возвращается в вызывающий метод F1
        // Когда асинхронная задача завершила выполнение, продолжает работу
        // асинхронный метод F1_2Async, который вызвал асинхронную задачу
        public static void F1()
        {
            static void F1_1()
            {
                int result = 1;

                for (int i = 1; i <= 6; i++)
                {
                    result *= i;
                }
                
                Thread.Sleep(8000);
                Console.WriteLine($"Factorial equals {result}");
            }

            static async void F1_2Async()
            {
                Console.WriteLine("Begin F1_2");
                await Task.Run(() => F1_1());
                Console.WriteLine("End F1_2");
            }

            F1_2Async();
        }
        #endregion

        #region Вызов через лямбду
        public static async void F2()
        {
            await Task.Run(() =>
            {
                int result = 1;

                for (int i = 1; i <= 6; i++)
                {
                    result *= i;
                }

                Thread.Sleep(8000);
                Console.WriteLine($"Factorial equals {result}");
            });
        }
        #endregion

        #region Передача параметров в асинхронную операцию
        public static void F3()
        {
            static void F3_1(int n)
            {
                int result = 1;

                for (int i = 1; i <= n; i++)
                {
                    result *= i;
                }
                
                Thread.Sleep(5000);
                Console.WriteLine($"Факториал равен {result}");
            }

            static async void F3_2Async(int n)
            {
                await Task.Run(() => F3_1(n));
            }

            F3_2Async(5);
            F3_2Async(6);
            Console.WriteLine("...");
        }
        #endregion

        #region Получение результата из асинхронной операции
        public static void F4()
        {
            static int F4_1(int n)
            {
                int result = 1;

                for (int i = 1; i <= n; i++)
                {
                    result *= i;
                }

                return result;
            }

            static async void F4_2Async(int n)
            {
                int x = await Task.Run(() => F4_1(n));
                Console.WriteLine($"Факториал равен {x}");
            }

            F4_2Async(5);
            F4_2Async(6);
            Console.Read();
        }
        #endregion

        #region Отмена асинхронных операций
        // Для отмены асинхронных операций используются классы CancellationToken и CancellationTokenSource
        // CancellationToken содержит информацию о том, надо ли отменять асинхронную задачу
        // Асинхронная задача, в которую передается объект CancellationToken, периодически проверяет
        // состояние этого объекта
        // Если его свойство IsCancellationRequested равно true, то задача должна остановиться

        // Для создания объекта CancellationToken применяется объект CancellationTokenSource
        // При вызове у CancellationTokenSource метода Cancel(), у объекта CancellationToken свойство
        // IsCancellationRequested будет установлено в true

        // Для создания токена определяется объект CancellationTokenSource
        // Метод FactorialAsync принимает токен, и если во внешнем коде произойдет отмена операции через cts.Cancel,
        // то в методе Factorial свойство token.IsCancellationRequested будет true
        // и при очередной итерации цикла в методе Factorial произойдет выход из метода,
        // а асинхронная операция завершится
        public static void F15()
        {
            static void F15_1(int n, CancellationToken token)
            {
                int result = 1;

                for (int i = 1; i <= n; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Operation was canceled");
                        return;
                    }

                    result *= i;
                    Console.WriteLine($"Factorial of number {i} equals {result}");
                    Thread.Sleep(1000);
                }
            }

            static async void F15_2Async(int n, CancellationToken token)
            {
                if (token.IsCancellationRequested)
                    return;

                await Task.Run(() => F15_1(n, token));
            }

            var cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            F15_2Async(6, token);
            Thread.Sleep(3000);
            cts.Cancel();
            Console.Read();
        }
        #endregion

        #region Асинхронные стримы
        // Асинхронные методы до сих пор позволяли получать один объект, когда асинхронная операция
        // была готова предоставить результат
        // Для возвращения нескольких значений применяются итераторы, но они имеют синхронную природу,
        // блокируют вызывающий поток и не могут использоваться в асинхронном контексте
        // Асинхронные стримы обходят эту проблему, позволяя получать множество значений и возвращать
        // их по мере готовности в асинхронном режиме
        // Асинхронный стрим представляет метод, который обладает характеристиками:
        // - имеет модификатор async
        // - возращает объект IAsyncEnumerable<T>
        // - интерфейс IAsyncEnumerable определяет метод GetAsyncEnumerator, который возвращает IAsyncEnumerator
        // - содержит выражения yield return для последовательного получения элементов из асинхронного стрима

        // Метод GetNumbersAsync() представляет асинхронный стрим, он является асинхронным
        // Его возвращаемый тип - IAsyncEnumerable<int>
        // Он возвращает с помощью yield return каждые 100 миллисекунд некоторое число
        // То есть метод должен вернуть 10 чисел от 0 до 10 с промежутком в 100 миллисекунд
        // Для получения данных из стрима используется foreach, он предваряется оператором await
        // В этом случае метод F16 должен быть определен с оператором async
        public static async void F16()
        {
            static async IAsyncEnumerable<int> F16_1()
            {
                for (int i = 0; i < 10; i++)
                {
                    await Task.Delay(100);
                    yield return i;
                }
            }

            await foreach (var number in F16_1())
            {
                Console.WriteLine(number);
            }
        }
        #endregion

        #region Применение асинхронных стримов для получения данных из внешнего хранилища
        class Repository
        {
            string[] data = { "Tom", "Sam", "Kate", "Alice", "Bob" };
            public async IAsyncEnumerable<string> F1()
            {
                for (int i = 0; i < data.Length; i++)
                {
                    Console.WriteLine($"Getting {i + 1} element");
                    await Task.Delay(500);
                    yield return data[i];
                }
            }
        }

        public static async void F17()
        {
            Repository repo = new Repository();
            IAsyncEnumerable<string> data = repo.F1();

            await foreach (var name in data) 
            { 
                Console.WriteLine(name); 
            }
        }
        #endregion

        #region Возвращение void из асинхронного метода
        // Использовать такие методы необходимо осторожно - могут вызвать непредсказуемое поведение
        // Чтобы не было проблем, async void заменить на async Task
        // Внутри async void метода должна быть обработка ошибок через try catch и в вызывающем коде тоже
        // Если библиотека не ожидает асинхронный callback, нужно предоставить callback с синхронной сигнатурой,
        // которая для async метода может только возвращать void

        // Асинхронный метод может возвращать void если:
        // - метод Main сам по себе async
        // - метод является event handler
        // - метод - это команда, которая является имплементацией интерфейса ICommand.Execute
        // - метод - это callback, то есть вызывается после завершения асинхронной операции
        public static async void F5()
        {
            static void Factorial(int n)
            {
                int result = 1;

                for (int i = 1; i <= n; i++) 
                { 
                    result *= i; 
                }

                Console.WriteLine($"Факториал равен {result}");
            }

            await Task.Run(() => Factorial(5));
        }
        #endregion

        #region Возвращение Task из асинхронного метода
        // Метод FactorialAsync не использует return для возвращения результата
        // Но если в асинхронном методе выполняется в выражении await асинхронная операция,
        // то можем возвращать из метода объект Task
        public static void F6()
        {
            static void F6_1(int n)
            {
                int result = 1;

                for (int i = 1; i <= n; i++) 
                { 
                    result *= i; 
                }

                Console.WriteLine($"Factorial equals {result}");
            }

            static async Task F6_2Async(int n) 
            { 
                await Task.Run(() => F6_1(n)); 
            }

            F6_2Async(5);
            F6_2Async(6);
            Console.WriteLine("...");
        }
        #endregion

        #region Возвращение Task<T> из асинхронного метода
        // Метод может возвращать значение, тогда возвращаемое значение оборачивается в объект Task,
        // а возвращаемым типом является Task<T>
        // F7_1 возвращает int, в асинхронном методе F7_2Async мы получаем и возвращаем это число
        // Поэтому возвращаемым типом является тип Task<int>
        // Если бы метод F7_1 возвращал строку, то возвращаемым типом был бы Task<string>        
        public static async void F7()
        {
            static int F7_1(int n)
            {
                int result = 1;

                for (int i = 1; i <= n; i++) 
                { 
                    result *= i; 
                }

                return result;
            }

            static async Task<int> F7_2Async(int n) 
            { 
                return await Task.Run(() => F7_1(n)); 
            }

            int n1 = await F7_2Async(5);    // Чтобы получить результат асинхронного метода,                                         
            int n2 = await F7_2Async(6);    // применяем оператор await при вызове F7_2Async
            Console.WriteLine($"n1={n1} n2={n2}");
        }
        #endregion

        #region Возвращение ValueTask<T> из асинхронного метода
        // Использование типа ValueTask<T> во многом аналогично применению Task<T>
        // ValueTask - структура, а Task - класс
        // По умолчанию тип ValueTask недоступен, надо установить через NuGet пакет
        // System.Threading.Tasks.Extensions
        public static async void F8()
        {
            static int F8_1(int n)
            {
                int result = 1;

                for (int i = 1; i <= n; i++) 
                { 
                    result *= i; 
                }

                return result;
            }

            static async ValueTask<int> F8_2Async(int n) 
            { 
                return await Task.Run(() => F8_1(n)); 
            }

            int n1 = await F8_2Async(5);
            int n2 = await F8_2Async(6);
            Console.WriteLine($"n1={n1} n2={n2}");
        }
        #endregion

        #region Последовательный вызов     
        // Асинхронный метод может содержать множество выражений await
        // Когда система встречает await, то выполнение в асинхронном методе останавливается,
        // пока не завершится асинхронная задача
        // После завершения задачи управление переходит к следующему оператору await и так далее
        // Это позволяет вызывать асинхронные задачи последовательно в определенном порядке
        // Здесь факториалы вычислятся последовательно и вывод будет строго детерминирован
        // Применяется, если одна задача зависит от результатов другой
        public static void F9()
        {
            static void F9_1(int n)
            {
                int result = 1;

                for (int i = 1; i <= n; i++) 
                { 
                    result *= i;
                }

                Console.WriteLine($"Factorial of number {n} equals {result}");
            }
            // определение асинхронного метода
            static async void F9_2Async()
            {
                await Task.Run(() => F9_1(4));
                await Task.Run(() => F9_1(3));
                await Task.Run(() => F9_1(5));
            }

            F9_2Async();
        }
        #endregion

        #region Параллельный вызов
        // Можем запустить все задачи параллельно и через метод Task.WhenAll отследить их завершение
        // Запускаются задачи, затем Task.WhenAll создает новую задачу, которая будет выполнена
        // после выполнения всех предоставленных задач, то есть t1, t2, t3
        // С помощью оператора await ожидаем ее завершения
        // В итоге три задачи запустятся параллельно, но вызывающий метод FactorialAsync
        // благодаря оператору await все равно будет ожидать, пока они все не завершатся
        public static void F10()
        {
            static void F10_1(int n)
            {
                int result = 1;

                for (int i = 1; i <= n; i++) 
                { 
                    result *= i; 
                }

                Console.WriteLine($"Factorial of number {n} equals {result}");
            }

            static async void F10_2Async()
            {
                Task t1 = Task.Run(() => F10_1(4));
                Task t2 = Task.Run(() => F10_1(3));
                Task t3 = Task.Run(() => F10_1(5));
                await Task.WhenAll(new[] { t1, t2, t3 });
            }

            F10_2Async();
        }
        #endregion

        #region try catch
        // Для обработки ошибок выражение await помещается в блок try
        // Метод F11_1 генерирует исключение, если передается число меньше 1
        // Для обработки исключения в методе F11_2Async выражение await помещено в блок try
        // Вызывается асинхронный метод с передачей ему отрицательного числа, что привет к исключению
        // Но программа не остановит аварийно свою работу, а обработает исключение и продолжит работу
        public static void F11()
        {
            static void F11_1(int n)
            {
                if (n < 1)
                {
                    throw new Exception($"{n} : number can't be less than 1");
                }

                int result = 1;

                for (int i = 1; i <= n; i++) 
                { 
                    result *= i; 
                }

                Console.WriteLine($"Factorial of number {n} equals {result}");
            }

            static async void F11_2Async(int n)
            {
                try 
                { 
                    await Task.Run(() => F11_1(n)); 
                }
                catch (Exception ex) 
                { 
                    Console.WriteLine(ex.Message); 
                }
            }

            F11_2Async(-4);
            F11_2Async(6);

            Console.Read();
        }
        #endregion

        #region Исследование исключения
        // При возникновении ошибки у объекта Task, представляющего асинхронную задачу,
        // в которой произошла ошибка, свойство IsFaulted имеет значение true,
        // а свойство Exception объекта Task содержит информацию об ошибке
        // Если передадим в метод -1, то task.IsFaulted будет true
        public static void F12()
        {
            static void F12_1(int n)
            {
                if (n < 1)
                {
                    throw new Exception($"{n} : number can't be less than 1");
                }

                int result = 1;

                for (int i = 1; i <= n; i++) 
                { 
                    result *= i; 
                }

                Console.WriteLine($"Factorial of number {n} equals {result}");
            }

            static async void F12_2Async(int n)
            {
                Task task = null;

                try
                {
                    task = Task.Run(() => F12_1(n));
                    await task;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(task.Exception.InnerException.Message);
                    Console.WriteLine($"IsFaulted: {task.IsFaulted}");
                }
            }

            F12_2Async(-4);
            F12_2Async(6);

            Console.Read();
        }
        #endregion

        #region Обработка нескольких исключений. WhenAll
        // Если ожидаем выполнения сразу нескольких задач, например, с помощью Task.WhenAll,
        // то можем получить сразу несколько исключений для каждой выполняемой задачи
        // Тогда можем получить все исключения из свойства Exception.InnerExceptions
        // Здесь в три вызова метода факториала передаются заведомо некорректные числа: -3, -5, -10
        // При всех вызовах будет сгенерирована ошибка
        // Хотя блок catch через переменную Exception ex будет получать одно перехваченное исключение,
        // но с Exception.InnerExceptions сможем получить инфрмацию обо всех исключениях
        public static void F13()
        {
            static void F13_1(int n)
            {
                if (n < 1)
                {
                    throw new Exception($"{n} : number can't be less than 1");
                }

                int result = 1;

                for (int i = 1; i <= n; i++) 
                { 
                    result *= i; 
                }

                Console.WriteLine($"Factorial of number {n} equals {result}");
            }

            static async void F13_2Async(int n)
            {
                Task allTasks = null;

                try
                {
                    Task t1 = Task.Run(() => F13_1(-3));
                    Task t2 = Task.Run(() => F13_1(-5));
                    Task t3 = Task.Run(() => F13_1(-10));

                    allTasks = Task.WhenAll(t1, t2, t3);
                    await allTasks;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    Console.WriteLine("IsFaulted: " + allTasks.IsFaulted);

                    foreach (var inx in allTasks.Exception.InnerExceptions)
                    {
                        Console.WriteLine("Inner exception: " + inx.Message);
                    }
                }
            }

            F13_2Async(-4);
            F13_2Async(6);

            Console.Read();
        }
        #endregion

        #region await в блоках catch и finally      
        public static void F14() // В catch и finally может быть асинхронный код
        {
            static void F14_1(int n)
            {
                if (n < 1)
                {
                    throw new Exception($"{n} : number can't be less than 1");
                }

                int result = 1;

                for (int i = 1; i <= n; i++) 
                { 
                    result *= i; 
                }

                Console.WriteLine($"Factorial of number {n} equals {result}");
            }

            static async void F14_2Async(int n)
            {
                try 
                { 
                    await Task.Run(() => F14_1(n)); 
                }
                catch (Exception ex) 
                { 
                    await Task.Run(() => Console.WriteLine(ex.Message)); 
                }
                finally 
                { 
                    await Task.Run(() => Console.WriteLine("await в блоке finally")); 
                }
            }

            F14_2Async(-4);
            F14_2Async(6);

            Console.Read();
        }
        #endregion
    }

    #region Возвращение void
    // Использовать такие методы необходимо осторожно - могут вызвать непредсказуемое поведение
    // Чтобы не было проблем, async void заменить на async Task
    // Внутри async void метода должна быть обработка ошибок через try catch и в вызывающем коде тоже
    // При использовании async void метода нельзя написать await перед его вызовом

    // Асинхронный метод может возвращать void в таких случаях:
    // - метод main сам по себе async
    // - метод является event handler
    // - метод - это команда, которая является имплементацией интерфейса ICommand.Execute
    // - метод - это callback, то есть вызывается после завершения асинхронной операции
    // Если библиотека не ожидает асинхронный callback, нужно предоставить callback с синхронной сигнатурой,
    // которая для async метода может только возвращать void handle exceptions in async code
    public static class TaskExtensions
    {
        public async static void Await(this Task task)
        {
            try 
            { 
                await task; 
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message); 
            }
        }
    }

    class Program_
    {
        public static async Task F1()
        {
            Console.WriteLine("2, thread " + Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(2000);
            throw new Exception("throw new Exception, thread " + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("3, thread " + Thread.CurrentThread.ManagedThreadId);
        }

        static void Main_(string[] args)
        {
            try
            {
                Console.WriteLine("1, thread " + Thread.CurrentThread.ManagedThreadId);
                F1().Await(); // Вызываем расширяющий метод, который перехватывает исключение,
                              // без него код отработает без ошибок и исключение так и останется не перехваченным
                Console.WriteLine("4, thread " + Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception ex) 
            { 
                Console.WriteLine("catch_2 " + ex); 
            }
            Console.WriteLine("5, thread " + Thread.CurrentThread.ManagedThreadId);
        }
    }
    #endregion

    #region Примеры
    class Exapmple
    {
        async static Task Main_()
        {
            await WriteA();
            var b = WriteB();
            var c = await WriteC();
            var d = await WriteD();
            var a2 = WriteA().Result;
            //var b2 = await WriteB().Result;
        }

        public async static Task<int> WriteA()
        {
            return await Task<int>.Run(() =>
            {
                Console.WriteLine("A");
                return 1;
            });
        }

        public async static Task<int> WriteB()
        {
            return await Task<int>.Run(() =>
            {
                Console.WriteLine("B");
                return 2;
            });
        }

        public async static Task<int> WriteC()
        {
            return await Task<int>.Run(() =>
            {
                Console.WriteLine("C");
                return 3;
            });
        }

        public async static Task<int> WriteD()
        {
            Console.WriteLine("D");
            return 4;
        }
    }
    #endregion
}