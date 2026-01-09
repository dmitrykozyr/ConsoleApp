using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory             = new ConnectionFactory { HostName = "localhost" };
using var connection    = await factory.CreateConnectionAsync();
using var channel       = await connection.CreateChannelAsync();

await channel.QueueDeclareAsync(
    queue:      "queue_1",
    durable:    true,   // Сообщения будут сохранены, если перезапустить Producer, когда не все сообщения еще вычитаны из очереди
    exclusive:  false,
    autoDelete: false,
    arguments:  null);

Console.WriteLine("Waiting for messages");

var consumer = new AsyncEventingBasicConsumer(channel);

consumer.ReceivedAsync += async (sender, eventArgs) =>
{
    byte[] body     = eventArgs.Body.ToArray();
    string message  = Encoding.UTF8.GetString(body);

    Console.WriteLine($"Received: {message}");

    ((AsyncEventingBasicConsumer)sender).Channel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
};

await channel.BasicConsumeAsync(
    queue: "queue_1",
    autoAck: false,
    consumer);

Console.ReadLine();
