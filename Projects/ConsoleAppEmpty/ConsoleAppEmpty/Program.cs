public class Program
{
    static void Main()
    {
        var userClass1 = new UserClass("Tom");
        var userClass2 = new UserClass("Tom");
        Console.WriteLine(userClass1.Equals(userClass2)); // false

        var userRecord1 = new UserRecord("Tom");
        var userRecord2 = new UserRecord("Tom");
        Console.WriteLine(userRecord1.Equals(userRecord2)); // true
    }





    public class UserClass
    {
        public string Name { get; init; }
        public UserClass(string name) => Name = name;
    }

    public record UserRecord
    {
        public string Name { get; init; }

        public UserRecord(string name) => Name = name;
    }
}
