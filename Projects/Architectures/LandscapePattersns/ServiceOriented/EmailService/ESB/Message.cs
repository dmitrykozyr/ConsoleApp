namespace EmailService.ESB;

public class Message<T>
{
    public Message(T data)
    {
        MessageId = Guid.NewGuid();
        Data = data;
    }

    public Guid MessageId { get; set; }

    public T Data { get; set; }
}