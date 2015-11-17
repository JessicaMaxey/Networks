using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Compression_Server_v2
{
    class Program
    {
        static StreamReader sreader = null;
        static StreamWriter swriter = null;

        public static void Main(string[] args)
        {
            while (true)
            {
                //HttpListener listener = new HttpListener();

                //listener.Prefixes.Add("http://127.0.0.1:8080/");
                //listener.Start();
                //textBox1.Text = "listening.../n/r";

                //HttpListenerContext context = listener.GetContext();

                //HttpListenerRequest request = context.Request;
                //textBox1.Text = request.ToString();

                //HttpListenerResponse response = context.Response;
                //textBox1.Text = response.ToString();

                //string responsestring = "received";
                //byte[] buffer = Encoding.UTF8.GetBytes(responsestring);
                //response.ContentLength64 = buffer.Length;

                //Stream output = response.OutputStream;
                //output.Write(buffer, 0, buffer.Length);
                //output.Flush();
                //output.Close();


                byte[] byte_ipaddress = { 127, 0, 0, 1 };

                IPAddress ip_address = new IPAddress(byte_ipaddress);
                int port = 8080;

                TcpListener server = new TcpListener(ip_address, port);

                server.Start();
                

                //loops listening for clients
                for (;;)
                {
                    TcpClient client = server.AcceptTcpClient();

                    Messaging(client);
                }
            }
        }

        public static void Messaging(TcpClient client)
        {
            WebRequest response;
            Console.WriteLine("Connected... Getting screen name... ");



            String input = "";

            sreader = new StreamReader(client.GetStream());
            
            while(input != "</soapenv:Envelope>")
            {
                input = sreader.ReadLine();
                Console.WriteLine(input);

            }

            swriter = new StreamWriter(client.GetStream());
            swriter.WriteLine("got it.");
        }


        //private void Reader()
        //{
        //    String returndata = sreader.ReadLine();
        //    returndata += "\n";
        //    //using this method makes writing to the 
        //    //message box thread safe
        //    this.SetText(returndata);


        //}

        //delegate void SetTextCallback(string text);

        ////this makes writing to the textbox thread safe
        //private void SetText(string text)
        //{
        //    if (this.textBox1.InvokeRequired)
        //    {
        //        SetTextCallback d = new SetTextCallback(SetText);
        //        this.Invoke(d, new object[] { text });
        //    }
        //    else
        //    {
        //        this.textBox1.Text += (text + "\r\n");
        //    }
        //}
    }
}
