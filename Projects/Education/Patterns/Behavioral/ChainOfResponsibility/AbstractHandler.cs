using Education.Patterns.Behavioral.ChainOfResponsibility.Interfaces;

namespace Education.Patterns.Behavioral.ChainOfResponsibility;

public abstract class AbstractHandler : IHandler
{
    private IHandler? _nextHandler;

    public IHandler SetNext(IHandler handler)
    {
        _nextHandler = handler;

        return handler;
    }

    public virtual string HandleRequest(string request)
    {
        if (_nextHandler is not null)
        {
            return _nextHandler.HandleRequest(request);
        }

        return null;
    }
}
