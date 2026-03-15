namespace Education.General;

public static class ExtensionMethods_
{
    // Имеют слово this перед первым аргументом
    // Могут быть только статическими и только в статических классах
    // Запрещаются параметры по умолчанию
    // Можно перегружать
    // Первый аргумент не может быть помечен словами ref, out, остальные могут
    // Не имеют доступ к private и protected полям расширяемого класса

    static void Extention(this string value)
    {
        Console.WriteLine(value);
    }

    static void Extention(this string value1, string value2)
    {
        Console.WriteLine(value1 + value2);
    }

    public class ProgramExtensionMethods
    {
        public void Main_()
        {
            string text = "1";

            text.Extention();

            text.Extention("2");

            "1".Extention("2");
        }
    }
}
