using Domain.Interfaces.Serializer;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Application.Services.Serializer;

public class XmlMessageSerializer : IXmlMessageSerializer
{
    public async Task<TMessage> Deserialize<TMessage>(Memory<byte> messageBody)
        where TMessage : class
    {
        var serializer = new XmlSerializer(typeof(TMessage));
        string xml = Encoding.UTF8.GetString(messageBody.Span);
        var newXml = xml;

        using (var reader = new StringReader(newXml))
        {
            return await Task.Run(() => (TMessage)serializer.Deserialize(reader));
        }
    }

    public async Task<object> Deserialize(Memory<byte> messageBody, Type returnType)
    {
        var serializer  = new XmlSerializer(returnType);
        string xml      = Encoding.UTF8.GetString(messageBody.Span);
        var newXml      = xml;

        using (var reader = new StringReader(newXml))
        {
            return await Task.Run(() => serializer.Deserialize(reader));
        }
    }

    public async Task<Memory<byte>> Serialize<TMessage>(TMessage message)
        where TMessage : class
    {
        var serializer          = new XmlSerializer(typeof(TMessage));
        var stream              = new MemoryStream();
        var xmlWriterSettings   = new XmlWriterSettings()
        {
            Indent = true,
            Encoding = Encoding.UTF8,
        };

        using var xmlWriter = XmlWriter.Create(stream, xmlWriterSettings);
        var emptyNamespaces = new XmlSerializerNamespaces();

        emptyNamespaces.Add(string.Empty, string.Empty);

        serializer.Serialize(xmlWriter, message, emptyNamespaces);

        stream.Position = 0;

        string result = await new StreamReader(stream).ReadToEndAsync();

        return Encoding.UTF8.GetBytes(result);
    }
}
