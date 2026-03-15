namespace Education.General;

public class Exceptions_
{
    // Исключение не обязательно должно быть обработано в классе, где оно произошло
    // Можно создать класс для обработки определенных исключений
    // throw возвращает весь стек вызовов
    public class ExceptionHandler
    {
        public static void F1()
        {
            try
            {
                var x = 0;

                // Ошибка
                var y = 10 / x;
            }
            catch (Exception ex)
            {
                // Нет StackTrace - плохо
                throw new Exception(ex.Message);

                // Есть StackTrace - хорошо
                throw;

                // Есть StackTrace - хорошо
                throw new Exception("Сообщение об ошибке ", ex);
            }
        }

        public void Main_()
        {
            try
            {
                // Сначала выполнятся try catch finally данного уровня,
                // даже если возникнет исключение
                try
                {
                    throw new NullReferenceException();
                }
                catch
                {
                    // Здесь throw пробросит вверх исключение, пойманное выше
                    // throw без аргументов можно вызвать только из блока catch
                    throw;
                }
                // Вызовется в любом случае
                finally
                {
                }
            }
            // Если в try произошло исключение - вызовется соответствующий блок catch
            catch (NullReferenceException)
            {
            }
            catch (Exception)
            {
            }
            // Вызовется в любом случае
            finally
            {
                // Необработанное исключение
                throw new NullReferenceException();
            }
        }
    }
}
