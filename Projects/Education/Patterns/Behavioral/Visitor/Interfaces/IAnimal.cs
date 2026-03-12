namespace Education.Patterns.Behavioral.Visitor.Interfaces;

public interface IAnimal
{
    void Accept(IVisitor visitor);
}
