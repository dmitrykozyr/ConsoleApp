using Education.Patterns.Behavioral.Visitor.Animals;

namespace Education.Patterns.Behavioral.Visitor.Interfaces;

public interface IVisitor
{
    void VisitLion(Lion lion);

    void VisitMonkey(Monkey monkey);
}
