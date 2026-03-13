using Education.Patterns.Behavioral.Observer.CustomClients;

namespace Education.Patterns.Behavioral.Observer;

// Когда товар появляется на складе,
// магазин рассылает сообщение всем подписчикам
public class ObserverPattern
{
    public void Start()
    {
        var store = new OnlineStore();

        var ivanEmail = new EmailClient();
        var petrSms = new SMSClient();

        store.Suscribe(ivanEmail);
        store.Suscribe(petrSms);

        // Оба получат уведомление
        store.Restock("iPhone 15");

        store.Unsubscribe(ivanEmail);

        // Получит только Петр
        store.Restock("PlayStation 5");
    }
}
