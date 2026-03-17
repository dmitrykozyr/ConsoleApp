namespace Education.General.Types;

public class Struct_
{
    struct Structure_
    {
        // Могут наследоваться только от интерфейса
        // Их нельзя наследовать
        // Могут быть readonly, но все поля тоже должны быть readonly

        public string name;
        public int age;

        // Ошибка - нельзя инициализировать поля при объявлении
        //public string gender = "Male";

        // Если определяем конструктор - он должен инициализировать все поля
        public Structure_(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Name: {name} Age: {age}");
        }
    }

    static void Main_()
    {
        Structure_ tom;     // Необязательно вызывать конструктор для создания объекта
                            // Надо проинициализировать все поля перед получением их значений 
        tom.name = "Tom";
        tom.age = 34;
        tom.DisplayInfo();

        var john = new Structure_("John", 37);
        john.DisplayInfo();

        var bob = new Structure_();  // Можем использовать конструктор без параметров, при вызове
                                     // которого полям будет присвоено значение по умолчанию
        bob.DisplayInfo();
    }
}
