using Education.Patterns.Behavioral.Visitor.Animals;
using Education.Patterns.Behavioral.Visitor.Interfaces;

namespace Education.Patterns.Behavioral.Visitor.Visitors;

public class SoundVisitor : IVisitor
{
    public void VisitLion(Lion lion)
    {
        Console.WriteLine($"Лев рычит: {lion.Roar()}");
    }

    public void VisitMonkey(Monkey monkey)
    {
        Console.WriteLine($"Обезьяна кричит: {monkey.Shout()}");
    }
}
