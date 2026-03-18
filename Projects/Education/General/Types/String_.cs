namespace Education.General.Types;

public class String_
{
    // Строки - это ссылочный тип
    public void F1()
    {
        // Интерполяция
        string name = "Алиса";
        string result = $"Я {name}";



        // Интернирование
        // Строки с одинаковыми значениями хранятся в одном месте в памяти
        // Если создаем несколько строк с одинаковым содержимым - они будут ссылаться на один объект в памяти
        string str_1 = "111";
        string str_2 = "111";
        bool isEquals;

        isEquals = ReferenceEquals(str_1, str_2);
        Console.WriteLine($"{str_1} {str_2} {isEquals}");
        // 111 111 True

        str_2 = "222";
        isEquals = ReferenceEquals(str_1, str_2);
        Console.WriteLine($"{str_1} {str_2} {isEquals}");
        // 111 222 False
    }
}
