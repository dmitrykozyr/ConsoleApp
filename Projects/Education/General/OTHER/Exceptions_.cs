namespace Education.General;

public class Exceptions_
{
    // Исключение не обязательно должно быть обработано в классе, где оно произошло
    // Можно создать класс для обработки определенных исключений

    // throw    возвращает весь стек вызовов
    // throw ex обрезает стек

    class ExceptionHandler
    {
        public static void Handle(Exception e)
        {
            if (e.GetBaseException().GetType() == typeof(ArgumentException))
            {
                Console.WriteLine("You caught ArgumentException");
            }
            else
            {
                throw e;
            }
        }
    }

    static class ExceptionThrower
    {
        public static void TriggerException(bool isTrigger)
        {
            throw new ArgumentException();
        }
    }

    static void Main_()
    {
        // Программа выведет 1 2 3 4 6
        try
        {
            // Сначала выполнятся try catch finally данного уровня, даже если возникнет исключение
            try
            {
                Console.WriteLine("1");
                throw new NullReferenceException();
            }
            catch
            {
                Console.WriteLine("2");

                // Если убрать эту строку, программа выведет 1 2 3 6
                // Здесь throw вверх то-же исключение, что было поймано выше - NullReferenceException
                // throw без аргументов можно вызвать только из блока catch
                throw;
            }
            // Вызовется в любом случае
            finally
            {
                Console.WriteLine("3");
            }
        }
        // Если в try произошло исключение, то вызовется соответствующий блок catch
        catch (NullReferenceException ex)
        {
            Console.WriteLine("4");
        }
        catch (Exception ex)
        {
            Console.WriteLine("5");
        }
        finally // Вызовется в любом случае
        {
            Console.WriteLine("6");
            throw new NullReferenceException(); // Необработанное исключение
        }
    }
}
