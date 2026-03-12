using Education.Patterns.Behavioral.Visitor.Interfaces;

namespace Education.Patterns.Behavioral.Visitor.Animals;

public class Lion : IAnimal
{
    public void Accept(IVisitor visitor)
    {
        visitor.VisitLion(this);
    }

    public string Roar()
    {
        return "Рррр!";
    }
}
