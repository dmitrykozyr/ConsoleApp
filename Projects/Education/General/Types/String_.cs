namespace Education.General.Types;

public class String_
{
    // Строки - это ссылочный тип

    public void F1()
    {
        // Интерполяция строк
        string name = "Alice";
        int age = 30;
        string result1 = $"Hello, my name is {name} and I am {age} years old";


        // Интернирование строк
        // Позволяет экономить память и улучшать производительность
        // Строки с одинаковыми значениями хранятся в одном и том же месте в памяти
        // Если создаем несколько строк с одинаковым содержимым, они будут ссылаться на один и тот же объект в памяти
        // При интернировании строки с одинаковыми значениями будут равны не только по содержимому, но и по ссылке
        string str1 = "Hello";
        string str2 = "Hello";
        bool result2 = ReferenceEquals(str1, str2); // true
    }
}
