
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace sever1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Listens for a connection from any IP
            //TcpListener listener = new TcpListener(System.Net.IPAddress.Any, 49152);
            //IPAddress ipAddress = IPAddress.Parse("192.168.254.37");
            TcpListener listener = new TcpListener(System.Net.IPAddress.Any, 49152);



            
            Task z = listenToClient(listener);

            //int i = 10;

            for (int i = 0; i < 100; i++)
            {
                
                Console.WriteLine("looping {0} times", i);
                System.Threading.Thread.Sleep(1000);
            }
            

        }

        private static async Task listenToClient(TcpListener listener)
        {
            listener.Start();
            while (true)
            {
                Console.WriteLine("Waiting for a connection.");
                //TcpClient client = listener.AcceptTcpClient();
                TcpClient client = await listener.AcceptTcpClientAsync();
                Console.WriteLine("Client accepted.");
                
                NetworkStream stream = client.GetStream();
                StreamReader sr = new StreamReader(client.GetStream());
                StreamWriter sw = new StreamWriter(client.GetStream());
                try
                {
                    byte[] buffer = new byte[1024];
                    stream.Read(buffer, 0, buffer.Length);
                    int recv = 0;
                    foreach (byte b in buffer)
                    {
                        if (b != 0)
                        {
                            recv++;
                        }
                    }
                    string request = Encoding.UTF8.GetString(buffer, 0, recv);
                    
                    Console.WriteLine("request received: " + request);
                    sw.WriteLine("You rock!");
                    sw.Flush();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong.");
                    sw.WriteLine(e.ToString());
                }
                
            }
            
        }
    }
}