namespace Education;

public class AsyncAwait
{
    // Метод, помеченный словом async, может выполняться синхронно, если в нем нет операторов await

    #region Отмена асинхронных операций

    public static void F1()
    {
        static void F2(int n, CancellationToken token)
        {
            int result = 1;

            for (int i = 1; i <= n; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Отмена операции");
                    return;
                }

                result *= i;

                Console.WriteLine(result);

                Thread.Sleep(1000);
            }
        }

        static async void F3(int n, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }

            await Task.Run(() => F2(n, token));
        }

        var cts = new CancellationTokenSource();

        CancellationToken token = cts.Token;

        F3(5, token);

        Thread.Sleep(3000);

        cts.Cancel();
    }

    #endregion

    #region IAsyncEnumerable

    /*
        Для возвращения нескольких значений применяются итераторы, но они:
        - синхронные
        - блокируют вызывающий поток
        - не могут использоваться в асинхронном контексте

        Асинхронные стримы обходят эту проблему, позволяя получать множество значений
        и возвращать их по мере готовности асинхронно

        Полезно при работе с асинхронными источниками данных, такими как БД или веб-API
    */

    class A
    {
        readonly string[] data = { "Tom", "Sam", "Kate", "Alice", "Bob" };

        public async IAsyncEnumerable<string> F1()
        {
            for (int i = 0; i < data.Length; i++)
            {
                await Task.Delay(500);

                yield return data[i];
            }
        }
    }

    public static async Task F2()
    {
        var objA = new A();

        IAsyncEnumerable<string> data = objA.F1();

        await foreach (var name in data)
        {
            Console.WriteLine(name);
        }
    }

    #endregion

    #region Возвращение Task<T> из асинхронного метода

    public static async Task F3()
    {
        static int F4(int n)
        {
            int result = 1;

            for (int i = 1; i <= n; i++) 
            { 
                result *= i; 
            }

            return result;
        }

        static async Task<int> F5(int n) 
        { 
            return await Task.Run(() => F4(n)); 
        }

        // Чтобы получить результат асинхронного метода, применяем оператор await
        int n1 = await F5(5);
        int n2 = await F5(6);
    }

    #endregion

    #region Последовательный вызов     

    /*
        Асинхронный метод может содержать множество выражений await
        Когда система встречает await, выполнение в асинхронном методе останавливается, пока не завершится асинхронная задача
        После завершения задачи управление переходит к следующему оператору await и так далее
        Это позволяет вызывать асинхронные задачи последовательно в определенном порядке
        Здесь факториалы вычислятся последовательно и вывод будет строго детерминирован
        Применяется, если одна задача зависит от результатов другой
    */

    public static void F4()
    {
        static void F5(int n)
        {
            int result = 1;

            for (int i = 1; i <= n; i++) 
            { 
                result *= i;
            }

            Console.WriteLine($"Factorial of number {n} equals {result}");
        }

        static async void F6()
        {
            await Task.Run(() => F5(4));
            await Task.Run(() => F5(3));
            await Task.Run(() => F5(5));
        }
    }

    #endregion

    #region Параллельный вызов, WhenAll

    /*
        Можем запустить все задачи параллельно и через метод Task.WhenAll отследить их завершение

        Запускаются задачи, затем Task.WhenAll создает новую задачу, которая будет выполнена
        после выполнения всех предоставленных задач, то есть t1, t2, t3

        С помощью await ожидаем ее завершения

        В итоге три задачи запустятся параллельно,
        но вызывающий метод, благодаря оператору await, будет ожидать, пока они все завершатся
    */

    public static void F5()
    {
        static void F6(int n)
        {
            int result = 1;

            for (int i = 1; i <= n; i++) 
            { 
                result *= i; 
            }

            Console.WriteLine(result);
        }

        static async void F7()
        {
            Task t1 = Task.Run(() => F6(4));
            Task t2 = Task.Run(() => F6(3));
            Task t3 = Task.Run(() => F6(5));

            await Task.WhenAll(t1, t2, t3);
        }
    }

    #endregion

    #region ConfigureAwait

    public static void F18()
    {
        /*            
            Используется в асинхронном программировании для управления контекстом выполнения после завершения асинхронной операции

            ConfigureAwait(true) по умолчанию:
                - после завершения асинхронной операции продолжает выполнение в том-же контексте синхронизации, в котором была вызвана операция
                - при вызове метода из UI-потока, продолжение так-же выполнится в UI-потоке
                - может быть важно для обновления UI, но может привести к блокировке потока, если вызывающий код ожидает завершения других операций

            ConfigureAwait(false):
                - продолжение не будет выполняться в исходном контексте, что позволяет избежать блокировок
                - позволяет приложению выполняться в любом доступном потоке
                - полезно при выполнении фоновых операций, которые не требуют доступа к UI
                - используется, если не будем взаимодействовать с UI или контекстом синхронизации после завершения асинхронной операции
        */

        static async Task F1()
        {
            using (var client = new HttpClient())
            {
                string result = await client.GetStringAsync("https://api.github.com")
                                            .ConfigureAwait(false);

                Console.WriteLine($"Fetched {result.Length} characters.");
            }
        }

        static async Task Main_()
        {
            Console.WriteLine("Start");

            await F1().ConfigureAwait(true);

            // Выполнение после завершения асинхронной операции может продолжиться в любом доступном потоке,
            // что полезно для библиотек или фоновых задач, где контекст выполнения не важен
            await F1().ConfigureAwait(false);

            Console.WriteLine("End");
        }
    }

    #endregion

    #region Контекст синхронизации

    /*
        Это механизм управления тем, на каком потоке или в каком контексте выполняется код после завершения асинхронной операции
        Важен в асинхронном программировании, т.к. позволяет контролировать, как и где будет продолжаться выполнение кода после ожидания завершения задачи

        Аспекты контекста синхронизации (КС):
        - при вызове асинхронного метода, КС сохраняет информацию о текущем потоке или контексте, в котором метод был вызван
            Это позволяет при необходимости вернуться к тому же потоку после завершения асинхронной операции
        - в приложениях с UI (например, WPF или WinForms) контекст синхронизации обычно связан с основным потоком UI
            Любые изменения пользовательского интерфейса должны выполняться в этом потоке,
            иначе попытка обновления UI может привести к исключению
        - при вызове асинхронного метода, он может быть выполнен в другом потоке или в другом контексте
            После завершения метода, если не указано иное (например, с помощью ConfigureAwait(false)),
            выполнение может вернуться в исходный контекст синхронизации
        - ConfigureAwait позволяет управлять тем, возвращаться ли к исходному контексту синхронизации после завершения асинхронной операции
            Использование ConfigureAwait(false) позволяет избежать возвращения к UI-контексту, что может повысить производительность и избежать блокировок
    */

    async Task UpdateUIAsync_1()
    {
        // Код выполняется в UI-контексте
        // После завершения задержки код вернется в тот же UI-контекст
        await Task.Delay(1000); 

        // Обновление UI
        //myLabel.Text = "Updated";
    }

    async Task UpdateUIAsync_2()
    {
        // Код выполняется в UI-контексте
        // После завершения задержки код может не вернуться в UI-контекст
        await Task.Delay(1000).ConfigureAwait(false);

        // Это может вызвать исключение, если выполняется не в UI-потоке
        //myLabel.Text = "Updated";
    }

    #endregion
}
