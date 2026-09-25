using Education.Patterns.Behavioral.ChainOfResponsibility.Abstractions;
using Education.Patterns.Behavioral.ChainOfResponsibility.Constants;

namespace Education.Patterns.Behavioral.ChainOfResponsibility.CustomHandlers;

public class SquirrelHandler : AbstractHandler
{
    public override string HandleRequest(string request) =>
        request == Food.NUT
            ? $"Белка съела {request}"
            : base.HandleRequest(request);
}
