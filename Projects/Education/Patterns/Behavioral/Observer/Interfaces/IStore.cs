namespace Education.Patterns.Behavioral.Observer.Interfaces;

public interface IStore
{
    void Suscribe(ISubscriber subscriber);

    void Unsubscribe(ISubscriber subscriber);

    void Notify(string productName);
}
