namespace Domain.Interfaces.Serializer;

public interface IXmlMessageSerializer
{
    Task<object> Deserialize(Memory<byte> messageBody, Type returnType);

    Task<TMessage> Deserialize<TMessage>(Memory<byte> messageBody)
        where TMessage : class;

    Task<Memory<byte>> Serialize<TMessage>(TMessage message)
        where TMessage : class;
}
