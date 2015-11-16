using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;


namespace Compression_server
{
    class Program
    {
        TcpClient client = null;

        public Program (TcpClient tcpc) { client = tcpc; }
        static void Main(string[] args)
        {

            //need http server port 8080
            TcpListener listener = new TcpListener(8080);
            listener.Start();

            while (true)
            {
                Program p = new Program(listener.AcceptTcpClient());

                GetMessage(p);
            }
            //new thread for server to list
            //start thread

            //in listen
            //create tcplistener
        }

        public static void GetMessage(Program p)
        {
            //p.client.Connect("127.0.0.1", 8080);
            StreamReader reader = new StreamReader(p.client.GetStream());
            StreamWriter writer = new StreamWriter(p.client.GetStream());

            string input = "";
            string whole_input = "";

            while(input != "</soapenv:Envelope>")
            {
                input = reader.ReadLine();
                whole_input += input;
                Console.WriteLine(input);
            }

            writer.WriteLine("Got it");
            writer.Flush();
        }

    }
}
