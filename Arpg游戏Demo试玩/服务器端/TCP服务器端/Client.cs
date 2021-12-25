using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCP服务器端
{
    class Client
    {
        private Socket clientScoket;
        private Thread t;
        private byte[] data = new byte[1024];
        public Client(Socket s)
        {
            clientScoket = s;
            t = new Thread(ReceiveMessage);
            t.Start();
        }
        private void ReceiveMessage()
        {
            //循环接收数据
            while (true)
            {
                //判断socket连接是否断开
                if (clientScoket.Poll(10, SelectMode.SelectRead))
                {
                    if (clientScoket!=null)
                    {
                        clientScoket.Close();
                    }
                    
                    Console.WriteLine("断开连接");
                    break;//连接断开结束线程
                }
                int length = clientScoket.Receive(data);
                string message = Encoding.UTF8.GetString(data, 0, length);
                //接受数据，把数据分发到客户端.....
                Program.NroadCastMessage(message );
                Console.WriteLine("收到信息：" + message);
            }
        }
        public void Sendmessage(string message)
        {
            if (message!=""&& message!=null)
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                clientScoket.Send(data);
            }
        }
        public bool Connected
        {
            get
                { return clientScoket.Connected; }
        }

    }
}
