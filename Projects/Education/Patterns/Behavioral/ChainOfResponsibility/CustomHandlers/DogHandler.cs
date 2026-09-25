using Education.Patterns.Behavioral.ChainOfResponsibility.Abstractions;
using Education.Patterns.Behavioral.ChainOfResponsibility.Constants;

namespace Education.Patterns.Behavioral.ChainOfResponsibility.CustomHandlers;

public class DogHandler : AbstractHandler
{
    public override string HandleRequest(string request) =>
        request == Food.MEAT
            ? $"Собака съела {request}"
            : base.HandleRequest(request);
}
