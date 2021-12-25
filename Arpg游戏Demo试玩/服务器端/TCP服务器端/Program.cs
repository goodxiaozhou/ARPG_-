using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCP服务器端
{
    class Program
    {
        static List<Client> clientList = new List<Client>();
        /// <summary>
        /// 广播消息
        /// </summary>
        /// <param name="message"></param>
        public static void NroadCastMessage(string message)
        {
            //创建没有连接的集合
            if (message!=null&& message!="")
            {
                var notConnectedList = new List<Client>();
                for (int i = 0; i < clientList.Count; i++)
                {
                    if (clientList[i].Connected)
                    {

                        clientList[i].Sendmessage(message);


                    }
                    else
                    {
                        notConnectedList.Add(clientList[i]);
                    }
                    for (int j = 0; j < notConnectedList.Count; j++)
                    {
                        clientList.Remove(notConnectedList[j]);
                    }

                }
            }
            
            //foreach (var item in clientList)
            //{
            //    if (item.Connected)
            //    {
            //        item.Sendmessage(message);
            //    }
            //    else
            //    {
            //        notConnectedList.Add(item);
            //    }
            //    foreach (var temp in notConnectedList)
            //    {
            //        clientList.Remove(temp);
            //    }
                
            //}         

        }
        static void Main(string[] args)
        {
             
            Socket tcpServer = new Socket(AddressFamily.InterNetwork, 
                SocketType.Stream, ProtocolType.Tcp);
            //tcpServer.Bind(new IPEndPoint(IPAddress.Parse("192.168.1.6"),5566));
            IPAddress iPAddress = IPAddress.Parse("172.21.68.121");
            EndPoint point = new IPEndPoint(iPAddress, 7788);
            tcpServer.Bind(point);
            tcpServer.Listen(100 );
            Console.WriteLine("连接中");
           
            while (true)
            {
                Socket clientSocket = tcpServer.Accept();
                Console.WriteLine("连接成功");
                Client client = new Client(clientSocket);//客服端通信逻辑放到client类里面进行处理
                clientList.Add(client);
                //Console.ReadKey();
            }
            
           
        }
    }
}
