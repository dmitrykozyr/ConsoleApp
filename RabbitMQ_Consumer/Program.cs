// 2. Сделать этот проект старотвым, после чего из указанной очереди будет получено сообщение
// и больше этого сообщения в очереди не будет

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };

string queueName = "B";

using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: queueName,
                         durable: true,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    var consumer = new EventingBasicConsumer(channel);

    consumer.Received += (sender, e) =>
    {
        var body = e.Body;
        var message = Encoding.UTF8.GetString(body.ToArray());
        Console.WriteLine("Received message: {0}", message);
    };

    channel.BasicConsume(queue: queueName,
                         autoAck: true,
                         consumer: consumer);

    Console.WriteLine("Subscribed to the queue {0}", queueName);
    Console.ReadLine();
}
