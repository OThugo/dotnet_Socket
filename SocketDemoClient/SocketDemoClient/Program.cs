using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketDemoClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                int port = 4444;
                string host = "127.0.0.1";
                //创建终结点EndPoint
                IPAddress ip = IPAddress.Parse(host);
                IPEndPoint ipe = new IPEndPoint(ip, port);   //把ip和端口转化为IPEndPoint的实例

                //创建Socket并连接到服务器
                Socket c = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);   //  创建Socket
                Console.WriteLine("Connecting...");
                c.Connect(ipe); //连接到服务器

                //向服务器发送信息
                string sendStr = "messages from the client:of course I'mm right here！";
                byte[] bs = Encoding.ASCII.GetBytes(sendStr);   //把字符串编码为字节

                Console.WriteLine("Send message");
                c.Send(bs, bs.Length, 0); //发送信息

                //接受从服务器返回的信息
                string recvStr = "";
                byte[] recvBytes = new byte[1024];
                int bytes;
                bytes = c.Receive(recvBytes, recvBytes.Length, 0);    //从服务器端接受返回信息
                recvStr += Encoding.ASCII.GetString(recvBytes, 0, bytes);
                Console.WriteLine("client get message:{0}", recvStr);    //回显服务器的返回信息

                Console.ReadLine();
                //一定记着用完Socket后要关闭
                c.Close();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("argumentNullException:{0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException:{0}", e);
            }
        }
    }
}