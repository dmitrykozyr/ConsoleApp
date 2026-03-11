using Education.Patterns.Behavioral.ChainOfResponsibility.Constants;

namespace Education.Patterns.Behavioral.ChainOfResponsibility.CustomHandlers;

public class MonkeyHandler : AbstractHandler
{
    public override string HandleRequest(string request)
    {
        if (request == Food.BANANA)
        {
            return $"Обезьяна съела {request}";
        }
        else
        {
            return base.HandleRequest(request);
        }
    }
}
