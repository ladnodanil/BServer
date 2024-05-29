using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BServer
{
    public class BattleShipServer
    {
        private TcpListener listener;
        private List<Socket> clients = new List<Socket>();
        private bool isRunning = true;

        public BattleShipServer()
        {
            listener = new TcpListener(IPAddress.Any, 11000);
        }

        public void Start()
        {
            listener.Start();
            Console.WriteLine("Сервер запущен...");
            AcceptClients();
        }

        private async void AcceptClients()
        {
            while (isRunning)
            {
                var client = await listener.AcceptSocketAsync();
                Console.WriteLine("Клиент подключен...");
                clients.Add(client);

                if (clients.Count == 2)
                {
                    Console.WriteLine("Оба клиента подключены. Начало игры...");
                    Task.Run(() => HandleClient(clients[0], clients[1]));
                    Task.Run(() => HandleClient(clients[1], clients[0]));
                }
            }
        }

        private async Task HandleClient(Socket client, Socket otherClient)
        {
            byte[] buffer = new byte[4096];
            while (isRunning)
            {
                int bytesRead = await Task.Run(() => client.Receive(buffer));
                if (bytesRead > 0)
                {
                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Получено от клиента: {data}");
                    await Task.Run(() => otherClient.Send(buffer, bytesRead, SocketFlags.None));
                }
            }
        }
    }

}
