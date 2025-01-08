using WebApi.Services.Interfaces;

namespace WebApi.Services;

public class EmailMessageSender : IMessageSender
{
    public string Send() { return "Sent by Email"; }
}

public class SmsMessageSender
{
    public string Send() { return "Sent by SMS"; }
}

// Инжектим сервис через конструктор
public class UseService
{
    IMessageSender _messageSender;
    public UseService(IMessageSender messageSender) { _messageSender = messageSender; }

    void F1()
    {
        _messageSender.Send();
    }
}
