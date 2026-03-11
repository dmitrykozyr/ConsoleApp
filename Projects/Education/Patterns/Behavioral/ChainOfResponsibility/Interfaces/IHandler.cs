namespace Education.Patterns.Behavioral.ChainOfResponsibility.Interfaces;

public interface IHandler
{
    IHandler SetNext(IHandler handler);

    string HandleRequest(string request);
}
