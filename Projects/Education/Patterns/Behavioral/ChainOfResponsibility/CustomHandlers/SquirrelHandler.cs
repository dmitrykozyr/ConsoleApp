using Education.Patterns.Behavioral.ChainOfResponsibility.Constants;

namespace Education.Patterns.Behavioral.ChainOfResponsibility.CustomHandlers;

public class SquirrelHandler : AbstractHandler
{
    public override string HandleRequest(string request)
    {
        if (request == Food.NUT)
        {
            return $"Белка съела {request}";
        }
        else
        {
            return base.HandleRequest(request);
        }
    }
}
