namespace EmailService.ESB;

public class Message<T>
{
    public Guid MessageId { get; init; }

    public T Data { get; init; }

    public Message(T data)
    {
        MessageId = Guid.NewGuid();
        Data = data;
    }
}