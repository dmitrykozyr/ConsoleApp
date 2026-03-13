namespace Education.Patterns.Behavioral.Iterator;

public class IteratorPattern
{
    public void Start()
    {
        var studentGroup = new StudentGroup();

        foreach (var student in studentGroup)
        {
            Console.WriteLine(student);
        }
    }
}
