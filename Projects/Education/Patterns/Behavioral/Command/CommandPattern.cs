namespace Education.Patterns.Behavioral.Command;

// С данным паттерном официант может собрать 10 заказов в список (стек или очередь)
// и отправить на кухню, когда повар освободится
public class CommandPattern
{
    public void Start()
    {
        // Официант передает заказы повару, не зная об их содержимом
        var waiter = new Waiter();

        // Повар реально выполняет работу
        var chief = new Chief();

        // Клиент создает команды
        var order_1 = new CookOrder(chief, "Мясо");
        var order_2 = new CookOrder(chief, "Салат");

        waiter.AddOrderToList(order_1);
        waiter.AddOrderToList(order_2);

        // Официант передает их в работу
        waiter.SendOrdersToKitchen();
    }
}
