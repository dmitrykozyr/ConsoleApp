namespace Education.General.Types;

public class Struct_
{
    public struct Structure_
    {
        // Могут наследоваться только от интерфейса
        // Их нельзя наследовать
        // Могут быть readonly, но все поля тоже должны быть readonly

        public int age;
        public string name;

        // Можно инициализировать поля при объявлении
        public string gender = "Male";

        // Можно создать свой конструктор без параметров
        public Structure_()
        {
            name = "Unknown";
            age = 0;
        }

        // Конструктор больше не обязан инициализировать все поля вручную
        // Неуказанные поля получат значения по умолчанию
        public Structure_(string name)
        {
            this.name = name;
            // age и gender инициализируются автоматически (0 и "Male")
        }

        public Structure_(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Name: {name} Age: {age} Gender: {gender}");
        }
    }

    public void Main_()
    {
        Structure_ tom;

        tom.age = 34;
        tom.name = "Tom";
        tom.gender = "Male";

        tom.DisplayInfo();

        var john = new Structure_("John", 37);
        john.DisplayInfo();

        // Теперь вызовется наш кастомный конструктор без параметров
        var bob = new Structure_();
        bob.DisplayInfo();
    }
}
