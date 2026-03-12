using Education.Patterns.Behavioral.Visitor.Animals;
using Education.Patterns.Behavioral.Visitor.Interfaces;

namespace Education.Patterns.Behavioral.Visitor.Visitors;

public class FeedVisitor : IVisitor
{
    public void VisitLion(Lion lion)
    {
        Console.WriteLine("Льву дали мясо");
    }

    public void VisitMonkey(Monkey monkey)
    {
        Console.WriteLine("Обезьяне дали бананы");
    }
}
