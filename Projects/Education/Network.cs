using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Education
{
    class Network
    {
        class Socket_
        {
            // Сокеты - это механизм обмена данными между компьютерами через сеть
            // Предоставляют высокоуровневый интерфейс для работы с сетевыми соединениями, такими как TCP/IP и UDP
            // Позволяют устанавливать соединения, отправлять и принимать данные через сеть, управлять параметрами сетевого взаимодействия

            // Пример клиентского приложения, использующего сокеты для установления TCP-соединения с сервером, отправки данных и получения ответа            
            // Здесь используется синхронный подход, но в.NET доступны асинхронные методы для работы с сокетами
            static void Main_()
            {
                // Устанавливаем соединение с сервером
                var serverAddress = IPAddress.Parse("192.168.1.100");
                int port = 8080;

                var serverEndPoint = new IPEndPoint(serverAddress, port);
                var clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                clientSocket.Connect(serverEndPoint);

                // Отправляем данные на сервер
                string message = "Hello, server!";
                byte[] data = Encoding.ASCII.GetBytes(message);
                clientSocket.Send(data);

                // Получаем ответ от сервера
                byte[] buffer = new byte[1024];
                int bytesRead = clientSocket.Receive(buffer);
                string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Server response: " + response);

                // Закрываем соединение
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
        }
    }
}
