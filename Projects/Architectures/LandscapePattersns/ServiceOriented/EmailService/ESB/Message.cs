﻿namespace EmailService.ESB;

public class Message<T>
{
    public Guid MessageId { get; set; }

    public T Data { get; set; }

    public Message(T data)
    {
        MessageId = Guid.NewGuid();
        Data = data;
    }
}