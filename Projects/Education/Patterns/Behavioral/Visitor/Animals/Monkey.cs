using Education.Patterns.Behavioral.Visitor.Interfaces;

namespace Education.Patterns.Behavioral.Visitor.Animals;

public class Monkey : IAnimal
{
    public void Accept(IVisitor visitor)
    {
        visitor.VisitMonkey(this);
    }

    public string Shout()
    {
        return "Уа-уа!";
    }
}
