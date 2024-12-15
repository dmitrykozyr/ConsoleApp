// Ссылка на локальный RabbitMQ http://localhost:15672/#/

// 1. Сделать этот проект старотвым, после чего в указанную очередь будут отправлены сообщения

using RabbitMQ.Client;
using System.Text;

// Инстанс для соединения с сервером
var factory = new ConnectionFactory() { HostName = "localhost" };

// Очередь с таким именем должна существовать в http://localhost:15672/#/queues
// Иначе будет создана новая очередь
string queueName = "B";
int messageNumber = 0;

using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    do
    {
        // Каждую секунду в очередь будут добавляться сообщения
        Thread.Sleep(1000);

        // Создаем очередь, куда будем отправлять сообщения
        channel.QueueDeclare(queue: queueName,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
        
        string message = "Some message " + messageNumber;
        var messageBytes = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "",
                             routingKey: queueName,
                             basicProperties: null,
                             body: messageBytes);

        Console.WriteLine("Message {0} was sent", messageNumber++);
    }
    while (true);
}