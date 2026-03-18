namespace Education.General;

public static class ExtensionMethods_
{
    public static void ShowString(this string value) => Console.WriteLine(value);

    public static void CombineStrings(this string value_1, string value_2 = "defaultValue") => Console.WriteLine($"{value_1} {value_2}");

    public class ProgramExtensionMethods
    {
        public static void Main_()
        {
            string text = "Hello";

            text.ShowString();

            text.CombineStrings("World");

            "Hello".CombineStrings("World");
        }
    }
}
