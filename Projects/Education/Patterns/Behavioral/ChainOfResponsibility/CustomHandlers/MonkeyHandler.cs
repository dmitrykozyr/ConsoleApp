using Education.Patterns.Behavioral.ChainOfResponsibility.Abstractions;
using Education.Patterns.Behavioral.ChainOfResponsibility.Constants;

namespace Education.Patterns.Behavioral.ChainOfResponsibility.CustomHandlers;

public class MonkeyHandler : AbstractHandler
{
    public override string HandleRequest(string request) =>
        request == Food.BANANA
            ? $"Обезьяна съела {request}"
            : base.HandleRequest(request);
}
